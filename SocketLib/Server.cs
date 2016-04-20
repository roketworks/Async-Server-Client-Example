using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketLib {
  public class Server {
    private static ManualResetEvent _resetEvent = new ManualResetEvent(false);
    private static Dictionary<int, ClientStateObject> _clients = new Dictionary<int, ClientStateObject>();
    private static object m_locker = new Object();

    public delegate void MessageReceivedHandler(int id, String msg);
    public static event MessageReceivedHandler MessageReceived;
    public delegate void MessageSubmittedHandler(bool close);
    public static event MessageSubmittedHandler MessageSubmitted;

    private static bool stopServer = false;

    public Server() {}

    public static void StartServer(string ip, int port) {
      //var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
      //var ipAddress = ipHostInfo.AddressList[0];
      // TODO: CHECK IP VALID
      var localEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

      var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socket.Bind(localEndPoint);
      socket.Listen(3000);
      stopServer = false;

      while (true) {
        _resetEvent.Reset();
        socket.BeginAccept(new AsyncCallback(OnClientConnect), socket);
        _resetEvent.WaitOne();
        if (stopServer) {
          InternalStop(socket);
          return;
        }
      }
    }

    public static void StopServer() {
      _resetEvent.Set();
      stopServer = true;

    }

    private static void InternalStop(Socket socket) {
      var connectedClients =
        _clients.Where(c => c.Value.workSocket.Connected && c.Value.workSocket.Poll(1000, SelectMode.SelectRead)
                            && c.Value.workSocket.Available == 0);
      socket.Close();
      foreach (var connectedClient in connectedClients) {
        connectedClient.Value.workSocket.Shutdown(SocketShutdown.Both);
        connectedClient.Value.workSocket.Close();
      }
      _clients = new Dictionary<int, ClientStateObject>();
    }

    public static void Send(int clientId, string data) {
      var state = new ClientStateObject();
      var clientState = _clients.TryGetValue(clientId, out state) ? state : null;
      
      if (clientState == null || clientState.isConnected == false) {
        throw new Exception("Client doesnt exisit or is not connected"); 
      }

      var bytes = Encoding.UTF8.GetBytes(data);
      clientState.workSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, new AsyncCallback(SendCallback), clientState);

    }

    private static void SendCallback(IAsyncResult ar) {
      var state = (ClientStateObject) ar.AsyncState;
      try {
        state.workSocket.EndSend(ar);
      }
      catch (ObjectDisposedException) {
        // Connection likely closed
      }
    }

    private static void OnClientConnect(IAsyncResult ar) {
      _resetEvent.Set();
      var clientStateObject = new ClientStateObject();
      clientStateObject.isConnected = true;
      var socket = (Socket) ar.AsyncState;

      lock (m_locker) {
        clientStateObject.Id = !_clients.Any() ? 1 : _clients.Keys.Max() + 1;
        _clients.Add(clientStateObject.Id, clientStateObject);
      }

      try {
        clientStateObject.workSocket = socket.EndAccept(ar);
        clientStateObject.workSocket.BeginReceive(clientStateObject.buffer, 0, ClientStateObject.BufferSize,
          SocketFlags.None, RecieveCallback, clientStateObject);
      }
      catch (ObjectDisposedException) {
        // Connection is likely closed
      }
    }

    private static void RecieveCallback(IAsyncResult ar) {
      var clientState = (ClientStateObject) ar.AsyncState;
      int bytesReceived = clientState.workSocket.EndReceive(ar);
      if (bytesReceived > 0) {
        clientState.sb.Append(Encoding.UTF8.GetString(clientState.buffer, 0, bytesReceived));
      }
      if (bytesReceived == ClientStateObject.BufferSize) {
        clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None,
          RecieveCallback, clientState);
      }
      else {
        if (!String.IsNullOrWhiteSpace(clientState.sb.ToString())) {
          MessageReceived(clientState.Id, clientState.sb.ToString());
          clientState.sb = new StringBuilder();
          clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None,
            RecieveCallback, clientState);
        }
        else {
          // Likely connection is closing
          clientState.isConnected = false;
          clientState.workSocket.Dispose();
          _clients[clientState.Id] = clientState;
        }
      }
    }
  }
}