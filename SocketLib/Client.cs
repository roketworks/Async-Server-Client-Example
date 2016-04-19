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
      try {
        // Create the state object.
        ClientStateObject state = new ClientStateObject();
        state.workSocket = _listener;

        // Begin receiving the data from the remote device.
        state.workSocket.BeginReceive(state.buffer, 0, ClientStateObject.BufferSize, 0,
          new AsyncCallback(ReceiveCallback), state);
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    private void ReceiveCallback(IAsyncResult ar) {
      try {
        // Retrieve the state object and the client socket 
        // from the asynchronous state object.
        ClientStateObject state = (ClientStateObject) ar.AsyncState;
        Socket client = state.workSocket;

        // Read data from the remote device.
        int bytesRead = client.EndReceive(ar);

        if (bytesRead > 0) {
          // There might be more data, so store the data received so far.
          //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

          // Get the rest of the data.
          client.BeginReceive(state.buffer, 0, ClientStateObject.BufferSize, 0,
            new AsyncCallback(ReceiveCallback), state);
        }
        else {
          // All the data has arrived; put it in response.

          // Signal that all bytes have been received.
          receiveDone.Set();
        }
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
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