using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketLib {
  public class AsyncSocketManager {
    public static ManualResetEvent allDone = new ManualResetEvent(false);
    public static Action<string> requestComplete;

    public AsyncSocketManager() { }

    public static void SetCompleteAction(Action<string> action) {
      requestComplete = action;
    }

    public static void StartServer() {

      byte[] bytes = new Byte[1024];

      // Establish the local endpoint for the socket.
      // The DNS name of the computer
      // running the listener is "host.contoso.com".
      var ipHostInfo = Dns.Resolve(Dns.GetHostName());
      var ipAddress = ipHostInfo.AddressList[0];
      var localEndPoint = new IPEndPoint(ipAddress, 8080);

      // Create a TCP/IP socket.
      var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      try {
        listener.Bind(localEndPoint);
        listener.Listen(1000);

        while (true) {
          // Set the event to nonsignaled state.
          allDone.Reset();

          // Start an asynchronous socket to listen for connections.
          listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);

          // Wait until a connection is made before continuing.
          allDone.WaitOne();
        }
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    public static void AcceptCallback(IAsyncResult ar) {
      // Signal the main thread to continue.
      allDone.Set();

      // Get the socket that handles the client request.
      Socket listener = (Socket)ar.AsyncState;
      Socket handler = listener.EndAccept(ar);

      // Create the state object.
      StateObject state = new StateObject();
      state.workSocket = handler;
      handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar) {
      String content = String.Empty;

      // Retrieve the state object and the handler socket
      // from the asynchronous state object.
      StateObject state = (StateObject)ar.AsyncState;
      Socket handler = state.workSocket;

      // Read data from the client socket. 
      int bytesRead = handler.EndReceive(ar);

      if (bytesRead > 0) {
        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        // Echo the data back to the client.
        //Send(handler, content);
      }
      else {
        if (state.sb.Length > 1) {
          string res = state.sb.ToString();
          requestComplete(res);
        }
        handler.Close();
      }
    }

    private static void Send(Socket handler, String data) {
      // Convert the string data to byte data using ASCII encoding.
      byte[] byteData = Encoding.ASCII.GetBytes(data);

      // Begin sending the data to the remote device.
      handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar) {
      try {
        // Retrieve the socket from the state object.
        Socket handler = (Socket)ar.AsyncState;

        // Complete sending the data to the remote device.
        int bytesSent = handler.EndSend(ar);
        Console.WriteLine("Sent {0} bytes to client.", bytesSent);

        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    private static StateObject ByteArrayToObject(byte[] arrBytes, int length) {
      MemoryStream memStream = new MemoryStream();
      BinaryFormatter binForm = new BinaryFormatter();
      memStream.Write(arrBytes, 0, length);
      memStream.Seek(0, SeekOrigin.Begin);
      var obj = (StateObject)binForm.Deserialize(memStream);
      return obj;
    }
  }
}