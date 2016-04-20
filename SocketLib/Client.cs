using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Shared;

namespace SocketLib {
  public class AsynchronousClient : IDisposable {
    // The port number for the remote device.
    private const int port = 8080;

    // ManualResetEvent instances signal completion.
    public ManualResetEvent connected = new ManualResetEvent(false);
    public ManualResetEvent sendDone = new ManualResetEvent(false);
    public ManualResetEvent receiveDone = new ManualResetEvent(false);

    private Socket _listener;
    private bool close = false;

    public delegate void ConnectedHandler(AsynchronousClient a);
    public static event ConnectedHandler Connected;

    public delegate void MessageReceivedHandler(AsynchronousClient a, String msg);
    public static event MessageReceivedHandler MessageReceived;

    public delegate void MessageSubmittedHandler(AsynchronousClient a, bool close);
    public static event MessageSubmittedHandler MessageSubmitted;

    public void StartClient() {
      try {
        IPHostEntry ipHostInfo = Dns.Resolve("TM000080-VM");
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

        _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        _listener.BeginConnect(remoteEP, new AsyncCallback(ConnectCallback), _listener);
        connected.WaitOne();
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    public bool IsConnected() {
      return !(_listener.Poll(1000, SelectMode.SelectRead) && _listener.Available == 0);
    }

    private void ConnectCallback(IAsyncResult ar) {
      Socket client = (Socket) ar.AsyncState;
      client.EndConnect(ar);
      connected.Set();
      if (Connected != null) {
        Connected(this);
      }
    }

    public void Receive() {
      if (!IsConnected()) {
        throw new Exception("Client not connected");
      }

      var state = new ClientStateObject();
      state.workSocket = _listener;
      _listener.BeginReceive(state.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None, ReceiveCallback, state);
    }

    private void ReceiveCallback(IAsyncResult ar) {
      var clientState = (ClientStateObject) ar.AsyncState;
      try {
        int bytesRecieved = clientState.workSocket.EndReceive(ar);
        if (bytesRecieved > 0) {
          clientState.sb.Append(Encoding.UTF8.GetString(clientState.buffer, 0, bytesRecieved));
        }
        if (bytesRecieved == ClientStateObject.BufferSize) {
          clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None,
            ReceiveCallback, clientState);
        }
        else {
          if (!String.IsNullOrEmpty(clientState.sb.ToString())) {
            MessageReceived(this, clientState.sb.ToString());
            clientState.sb = new StringBuilder();
            receiveDone.Set();
            clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None,
              ReceiveCallback, clientState);
          }
          else {
            Console.WriteLine("Connection likely closing");
          }
        }
      }
      catch (ObjectDisposedException) {
        // Connection likely closed
      }
      catch (SocketException) {
        // Server connection failed
      }
    }

    public void Send(String data, bool close) {
      if (!IsConnected()) {
        throw new Exception("Client not connected");
      }

      byte[] response = Encoding.UTF8.GetBytes(data);
      this.close = close;
      _listener.BeginSend(response, 0, response.Length, SocketFlags.None, new AsyncCallback(SendCallback), _listener);
    }


    private void SendCallback(IAsyncResult ar) {
      Socket client = (Socket) ar.AsyncState;
      int bytesSent = client.EndSend(ar);
      //MessageSubmitted(this, close);
      sendDone.Set();
    }

    public void Dispose() {
      connected.Close();
      receiveDone.Close();
      sendDone.Close();

      if (IsConnected()) {
        _listener.Shutdown(SocketShutdown.Both);
        _listener.Close();
      }
    }
  }
}