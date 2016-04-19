using System;
using System.Net.Sockets;
using System.Text;

namespace SocketLib {
  public class StateObject {
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];

    public bool endOfRequest = false;
    // Received data string.

    public StringBuilder sb = new StringBuilder();
    public Action<StateObject, int> requestComplete;
  }
}
