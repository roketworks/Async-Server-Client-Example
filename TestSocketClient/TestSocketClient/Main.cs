using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Shared;
using SocketLib;
using Timer = System.Windows.Forms.Timer;

namespace TestSocketClient {
  public partial class Main : Form {

    private readonly AsynchronousClient asynchronousClient;
    private static Timer connectionTimer;
    private bool connectionSucessfull = false;

      public Main() {
      InitializeComponent();
      PopulateEndpoints();
      UpdateConnectionLabel(false);
      
      this.textBoxClientId.Text = Guid.NewGuid().ToString();
      AsynchronousClient.Connected += client => {
        this.Invoke(new Action(() => { connectionTimer.Start(); }));
        connectionSucessfull = true;
        UpdateConnectionLabel(true);
        client.Receive();
        client.receiveDone.WaitOne();
      };

      AsynchronousClient.MessageSubmitted += (client1, close) => { MessageBox.Show("Sent message"); };
      AsynchronousClient.MessageReceived += (client, msg) => {
        var action = (ClientAction) ConversionHelper.StringToObject(msg);
        if (action.Action == ActionType.Stop) {
          var shutdownThread = new Thread(() => {
            Thread.Sleep(5000);
            this.Invoke(new Action(() => this.Close()));
          });
          shutdownThread.Start();
        }
        SetOutputText(action.Message);
      };
      asynchronousClient = new AsynchronousClient();
    
      connectionTimer = new Timer();
      connectionTimer.Interval = 5000;
      connectionTimer.Tick += (sender, args) => UpdateConnectionLabel(asynchronousClient.IsConnected());
    }

    private void btnSend_Click(object sender, EventArgs e) {
      if (!connectionSucessfull) {
        MessageBox.Show("No message will be sent. Connection has not been made to server", "Connection Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);  
        return;
      }

      String s = textMessage.Text;
      var o = new StreamObject();
      o.ClientId = textBoxClientId.Text;
      o.Message = textMessage.Text;

      asynchronousClient.Send(ConversionHelper.ObjectToString(o), false);
      asynchronousClient.sendDone.WaitOne();
    }

    private void btnConnect_Click(object sender, EventArgs e) {
      // TODO: VALIDATE SETTINGS
      var ip = this.comboEndpoint.Text;
      var port = Convert.ToInt32(numericPort.Value);
      var workerThread = new Thread(() => asynchronousClient.StartClient(ip, port));
      workerThread.Start();  
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      asynchronousClient.Dispose();
    }

    private void PopulateEndpoints() {
      foreach (var ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList) {
        this.comboEndpoint.Items.Add(ipAddress.ToString());
      }
    }

    private void SetOutputText(string text) {
      if (this.txtReceieved.InvokeRequired) {
        this.Invoke(new Action<string>(s => SetOutputText(s)), new object[] {text});
      }
      else {
        this.txtReceieved.AppendText(text + Environment.NewLine);
      }
    }

    private void UpdateConnectionLabel(bool connected) {
      if (this.statusStrip.InvokeRequired) {
        this.Invoke(new Action<bool>((c) => { UpdateConnectionLabel(c); }), connected);
      }
      else {
        this.toolStripConnectionLabel.Text = connected ? "Connected" : "Not connected";
        this.toolStripConnectionLabel.ForeColor = connected ? Color.Green : Color.Red;
      }
    }

    private static void ClientMessageSubmitted(AsynchronousClient a, bool close) {
      if (close)
        a.Dispose();
    }
  }
}