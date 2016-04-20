using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JMeterOutputReader {
  static class Program {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args) {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      string ip = String.Empty;
      int port = 0;

      if (args.Length > 1) {
        ip = args[0];
        port = Convert.ToInt32(args[1]);
      }

      if (!String.IsNullOrWhiteSpace(ip) && port > 0) {
        Application.Run(new Main(ip, port));
      }
      else {
        Application.Run(new Configuration());
      }
    }
  }
}
