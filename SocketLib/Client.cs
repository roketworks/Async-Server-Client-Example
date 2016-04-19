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
  public class AsynchronousClient {
    // The port number for the remote device.
    private const int port = 8080;

    // ManualResetEvent instances signal completion.
    public static ManualResetEvent connectDone =
      new ManualResetEvent(false);

    public static ManualResetEvent sendDone =
      new ManualResetEvent(false);

    private static ManualResetEvent receiveDone =
      new ManualResetEvent(false);

    public static Socket client;

    // The response from the remote device.
    public static String response = String.Empty;

    public static void StartClient() {
      // Connect to a remote device.
      try {
        // Establish the remote endpoint for the socket.
        // The name of the 
        // remote device is "host.contoso.com".
        IPHostEntry ipHostInfo = Dns.Resolve("TM000080-VM");
        IPAddress ipAddress = ipHostInfo.AddressList[0];
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

        // Create a TCP/IP socket.
        client = new Socket(AddressFamily.InterNetwork,
          SocketType.Stream, ProtocolType.Tcp);

        // Connect to the remote endpoint.
        client.BeginConnect(remoteEP,
          new AsyncCallback(ConnectCallback), client);
        connectDone.WaitOne();

        //var o = new StreamObject() {ClientId = "121", Message = "MESSAGE"};
        //var xml = new XmlSerializer(typeof(StreamObject));
        //using (var sr = new StringWriter()) {
        //  xml.Serialize(sr, o);
        //  Send(client, sr.ToString());
        //}

        // Send test data to the remote device.
        //sendDone.WaitOne();

        // Receive the response from the remote device.
        //Receive(client);
        //receiveDone.WaitOne();

        // Write the response to the console.
        //Console.WriteLine("Response received : {0}", response);

        // Release the socket.
       // client.Shutdown(SocketShutdown.Both);
        //client.Close();
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    private static void ConnectCallback(IAsyncResult ar) {
      try {
        // Retrieve the socket from the state object.
        Socket client = (Socket) ar.AsyncState;

        // Complete the connection.
        client.EndConnect(ar);

        Console.WriteLine("Socket connected to {0}",
          client.RemoteEndPoint.ToString());

        // Signal that the connection has been made.
        connectDone.Set();
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    private static void Receive(Socket client) {
      try {
        // Create the state object.
        StateObject state = new StateObject();
        state.workSocket = client;

        // Begin receiving the data from the remote device.
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
          new AsyncCallback(ReceiveCallback), state);
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }

    private static void ReceiveCallback(IAsyncResult ar) {
      try {
        // Retrieve the state object and the client socket 
        // from the asynchronous state object.
        StateObject state = (StateObject) ar.AsyncState;
        Socket client = state.workSocket;

        // Read data from the remote device.
        int bytesRead = client.EndReceive(ar);

        if (bytesRead > 0) {
          // There might be more data, so store the data received so far.
          //state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

          // Get the rest of the data.
          client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
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

    public static void Send(String data) {
      if (client != null && client.Connected) {
        client.Shutdown(SocketShutdown.Both);
        client.Close();  
      }
      StartClient();
      Send(client, data);
      sendDone.WaitOne();
      client.Shutdown(SocketShutdown.Both);
      client.Close();
    }

    public static void Send(Socket client, String data) {
      // Convert the string data to byte data using ASCII encoding.
      byte[] byteData = Encoding.ASCII.GetBytes(data);

      // Begin sending the data to the remote device.
      client.BeginSend(byteData, 0, byteData.Length, 0,
        new AsyncCallback(SendCallback), client);
    }

    private static void SendCallback(IAsyncResult ar) {
      try {
        // Retrieve the socket from the state object.
        Socket client = (Socket) ar.AsyncState;

        // Complete sending the data to the remote device.
        int bytesSent = client.EndSend(ar);
        Console.WriteLine("Sent {0} bytes to server.", bytesSent);

        // Signal that all bytes have been sent.
        sendDone.Set();
      }
      catch (Exception e) {
        Console.WriteLine(e.ToString());
      }
    }
  }
}