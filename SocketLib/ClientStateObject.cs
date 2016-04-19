using System;
using System.Net.Sockets;
using System.Text;

namespace SocketLib {
  public class ClientStateObject {
    public int Id;
    // Client  socket.
    public Socket workSocket = null;
    // Size of receive buffer.
    public const int BufferSize = 1024;
    // Receive buffer.
    public byte[] buffer = new byte[BufferSize];
    // Received data string.
    public StringBuilder sb = new StringBuilder();

    public bool isConnected = false;
  }
}
