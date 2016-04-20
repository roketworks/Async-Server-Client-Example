using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Shared;
using SocketLib;

namespace JMeterOutputReader {
  public partial class Main : Form {

    private delegate void SetTextCallback(int id, string host, string text);
    private delegate void AddListItemCallback(string clientId);
    private object _locker;
    private Dictionary<int, ReponseStorage> _internalStorage;
    private string _currentClientId = String.Empty;
          
    public Main(string ip, int port) {
      InitializeComponent();
      _locker = new object();
      _internalStorage = new Dictionary<int, ReponseStorage>(); 

      listViewClients.ItemSelectionChanged += (sender, args) => {
        if (_internalStorage.Any(c => c.Value.ClientId == args.Item.Name)) {
          _currentClientId = args.Item.Name;
          this.txtOutput.Clear();
          this.txtOutput.Text = _internalStorage.First(c => c.Value.ClientId == args.Item.Name).Value.Message.ToString();
        }
      };
      
      var del = new Action<int, string>((int id, string s) => {
         var o = (StreamObject)StringToObject(s);
       
        if (!_internalStorage.ContainsKey(id)) {
          AddListItem(o.ClientId);
          lock (_locker) {
            _internalStorage.Add(id, new ReponseStorage { ClientId = o.ClientId, Message = new StringBuilder() });
          }
        }
        this.SetText(id, o.ClientId, o.Message);
      });

      Server.MessageReceived += (id, msg) => del(id, msg);
      var serverThread = new Thread(() => Server.StartServer(ip, port));
      serverThread.Start();
    }

    private void btnSendMessage_Click(object sender, EventArgs e) {
      //var clientId = listViewClients.SelectedItems[0].Name;
      var id = _internalStorage.First(c => c.Value.ClientId == _currentClientId).Key;
      Server.Send(id, this.txtClientMessage.Text);
    }

    private void AddListItem(string clientId) {
      if (this.listViewClients.InvokeRequired) {
        var a = new AddListItemCallback(AddListItem);
        this.Invoke(a, new object[] {clientId});
      }
      else {
        var listViewItem = new ListViewItem() { Name = clientId, Text = clientId };
        listViewItem.SubItems.Add("Additional Info: ");
        listViewClients.Items.Add(listViewItem);
      }
    }

    private void SetText(int id, string clientId, string message) {
      if (this.txtOutput.InvokeRequired) {
        SetTextCallback d = new SetTextCallback(SetText);
        this.Invoke(d, new object[] { id, clientId, message });
      }
      else {
        if (listViewClients.SelectedItems.Count > 0 && listViewClients.SelectedItems[0].Name == clientId) {
          _internalStorage[id].Message.AppendLine(message);
          this.txtOutput.AppendText(message + Environment.NewLine);
        }
        else {
          _internalStorage[id].Message.AppendLine(message);
        }
      }
    }

    private object StringToObject(string base64String) {
      byte[] bytes = Convert.FromBase64String(base64String);
      using (var ms = new MemoryStream(bytes, 0, bytes.Length)) {
        ms.Write(bytes, 0, bytes.Length);
        ms.Position = 0;
        return new BinaryFormatter().Deserialize(ms);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      Server.StopServer();
      Thread.Sleep(10);
      Application.Exit();
    } 
  }

  internal struct ReponseStorage {
    public string ClientId;
    public StringBuilder Message;
  }
}
