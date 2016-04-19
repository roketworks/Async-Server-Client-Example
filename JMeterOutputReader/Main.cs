using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Shared;
using SocketLib;

namespace JMeterOutputReader {
  public partial class Main : Form {

    delegate void SetTextCallback(string host, string text);
    private object locker;

    private delegate void AddListItemCallback(string clientId);
    //private TcpListener listener;                      
    //private TcpClient client;
    //private NetworkStream ns;
    //private Thread workerThread;

    private Dictionary<string, StringBuilder> internalStorage;
          
    public Main() {
      InitializeComponent();
      locker = new object();
      listView1.ItemSelectionChanged += (sender, args) => {
        if (this.internalStorage.ContainsKey(args.Item.Name)) {
          this.textBox1.Text = internalStorage[args.Item.Name].ToString();
        }
      };
      internalStorage = new Dictionary<string, StringBuilder>(); 
      AsyncSocketManager.SetCompleteAction((s) => {
        //var s = ByteArrayToObject(o.buffer, l);
        var xml = new XmlSerializer(typeof (StreamObject));
        StreamObject o;
        using (var sr = new StringReader(s)) {
          o = (StreamObject) xml.Deserialize(sr);
        }

        if (!internalStorage.ContainsKey(o.ClientId)) {
          AddListItem(o.ClientId);
          lock (locker) {
            internalStorage.Add(o.ClientId, new StringBuilder(o.Message));
          }
          this.SetText(o.ClientId, o.Message);
        }
      });
      Thread worker = new Thread(AsyncSocketManager.StartServer);
      worker.Start();
    }

    //private void DoWork() {
    //  var bytes = new byte[1024];
    //  while (true) {
    //    int bytesRead = ns.Read(bytes, 0, bytes.Length);
    //    var o = ByteArrayToObject(bytes, bytesRead);

    //    if (!internalStorage.ContainsKey(o.ClientId)) {
    //      AddListItem(o.ClientId);
    //      lock (locker) {
    //        internalStorage.Add(o.ClientId, new StringBuilder(o.Message));  
    //      }
    //    }

    //    this.SetText(o.ClientId, o.Message);
    //    Thread.Sleep(10); // Avoid memory issues
    //  }
    //}

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
      // InvokeRequired required compares the thread ID of the
      // calling thread to the thread ID of the creating thread.
      // If these threads are different, it returns true.
      if (this.textBox1.InvokeRequired) {
        SetTextCallback d = new SetTextCallback(SetText);
        this.Invoke(d, new object[] { clientId, message });
      }
      else {
        if (listView1.SelectedItems.Count > 0 && listView1.SelectedItems[0].Name == clientId) {
          this.internalStorage[clientId].AppendLine(message);
          this.textBox1.Text = this.textBox1.Text + Environment.NewLine + message;
        }
        else {
          this.internalStorage[clientId].AppendLine(message);
        }
      }
    }

    private StreamObject ByteArrayToObject(byte[] arrBytes, int length)
     {
       MemoryStream memStream = new MemoryStream();
       BinaryFormatter binForm = new BinaryFormatter();
       memStream.Write(arrBytes, 0, length);
       memStream.Seek(0, SeekOrigin.Begin);
       var obj = (StreamObject) binForm.Deserialize(memStream);
       return obj;
     }
  }
}
