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

    public Server() {}

    public static void StartServer(int port) {
      var ipHostInfo = Dns.Resolve(Dns.GetHostName());
      var ipAddress = ipHostInfo.AddressList[0];
      var localEndPoint = new IPEndPoint(ipAddress, port);
          
      var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socket.Bind(localEndPoint);
      socket.Listen(3000);

      while (true) {
        _resetEvent.Reset();
        socket.BeginAccept(new AsyncCallback(OnClientConnect), socket);
        _resetEvent.WaitOne();
      }
    }

    private static void OnClientConnect(IAsyncResult ar) {
      _resetEvent.Set();
      var clientStateObject = new ClientStateObject();
      var socket = (Socket) ar.AsyncState;

      lock (m_locker) {
        clientStateObject.Id = !_clients.Any() ? 1 : _clients.Keys.Max() + 1;
        _clients.Add(clientStateObject.Id, clientStateObject);   
      }

      clientStateObject.workSocket = socket.EndAccept(ar);
      clientStateObject.workSocket.BeginReceive(clientStateObject.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None, RecieveCallback, clientStateObject);
    }

    private static void RecieveCallback(IAsyncResult ar) {
      var clientState = (ClientStateObject) ar.AsyncState;
      int bytesReceived = clientState.workSocket.EndReceive(ar);
      if (bytesReceived > 0) {
        clientState.sb.Append(Encoding.UTF8.GetString(clientState.buffer, 0, bytesReceived));
      }
      if (bytesReceived == ClientStateObject.BufferSize) {
        clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None, RecieveCallback, clientState);
      }
      else {
        if (!String.IsNullOrWhiteSpace(clientState.sb.ToString())) {
          MessageReceived(clientState.Id, clientState.sb.ToString());
        }
        clientState.sb = new StringBuilder();
        clientState.workSocket.BeginReceive(clientState.buffer, 0, ClientStateObject.BufferSize, SocketFlags.None, RecieveCallback, clientState);
      }
    }
  }
}                                                     