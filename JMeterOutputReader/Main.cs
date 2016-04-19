using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Shared;
using SocketLib;

namespace JMeterOutputReader {
  public partial class Main : Form {

    private const int port = 8080;

    private delegate void SetTextCallback(string host, string text);
    private delegate void AddListItemCallback(string clientId);
    private object _locker;
    private Dictionary<string, StringBuilder> _internalStorage;
          
    public Main() {
      InitializeComponent();
      _locker = new object();
      _internalStorage = new Dictionary<string, StringBuilder>(); 

      listView1.ItemSelectionChanged += (sender, args) => {
        if (_internalStorage.ContainsKey(args.Item.Name)) {
          this.textBox1.Text = _internalStorage[args.Item.Name].ToString();
        }
      };
      
      var del = new Action<string>((string s) => {
        var xml = new XmlSerializer(typeof (StreamObject));
        StreamObject o;
        using (var sr = new StringReader(s)) {
          o = (StreamObject) xml.Deserialize(sr);
        }

        if (!_internalStorage.ContainsKey(o.ClientId)) {
          AddListItem(o.ClientId);
          lock (_locker) {
            _internalStorage.Add(o.ClientId, new StringBuilder());
          }
        }
        this.SetText(o.ClientId, o.Message);
      });

      Server.MessageReceived += (id, msg) => del(msg);
      var serverThread = new Thread(new ParameterizedThreadStart(o => Server.StartServer(Convert.ToInt32(o))));
      serverThread.Start(port);
    }

    private void AddListItem(string clientId) {
      if (this.listView1.InvokeRequired) {
        var a = new AddListItemCallback(AddListItem);
        this.Invoke(a, new object[] {clientId});
      }
      else {
        listView1.Items.Add(new ListViewItem() {Name = clientId, Text = clientId});
      }
    }

    private void SetText(string clientId, string message) {
      if (this.textBox1.InvokeRequired) {
        SetTextCallback d = new SetTextCallback(SetText);
        this.Invoke(d, new object[] { clientId, message });
      }
      else {
        if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Name == clientId) {
          _internalStorage[clientId].AppendLine(message);
          this.textBox1.Text = this.textBox1.Text + Environment.NewLine + message;
        }
        else {
          _internalStorage[clientId].AppendLine(message);
        }
      }
    }

    private StreamObject ByteArrayToObject(byte[] arrBytes, int length) {
       MemoryStream memStream = new MemoryStream();
       BinaryFormatter binForm = new BinaryFormatter();
       memStream.Write(arrBytes, 0, length);
       memStream.Seek(0, SeekOrigin.Begin);
       var obj = (StreamObject) binForm.Deserialize(memStream);
       return obj;
     }
  }
}
