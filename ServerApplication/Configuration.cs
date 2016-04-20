using System;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace AsyncDemo.ServerApplication {
  public partial class Configuration : Form {

    private bool isConfigValid = false;
    
    public Configuration() {
      InitializeComponent();
    }

    private void Configuration_Load(object sender, EventArgs e) {
      foreach (var ipAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList) {
        this.comboBox1.Items.Add(ipAddress.ToString());
      }
    }

    private void button1_Click(object sender, EventArgs e) {
      VerifySettings();
    }

    private void VerifySettings() {
      bool isValid = true;
      IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
      TcpConnectionInformation[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

      foreach (TcpConnectionInformation tcpi in tcpConnInfoArray) {
        if (tcpi.LocalEndPoint.Port == this.numericUpDown1.Value) {
          isValid = false;
          break;
        }
      }

      if (this.comboBox1.SelectedItem == null) {
        isValid = false;
      }

      if (isValid) {
        //.Show("Settings Valid.", "Configuration Verified", MessageBoxButtons.OK, MessageBoxIcon.None);
        this.label3.Text = "Settings Valid";
        this.label3.ForeColor = Color.Green;
      }
      else {
        //MessageBox.Show("Please check select IP is valid & port select is not in use.", "Configuration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.label3.Text = "Invalid settings";
        this.label3.ForeColor = Color.Red;
      }
    }

    private void button2_Click(object sender, EventArgs e) {
      VerifySettings();
      var main = new Main(this.comboBox1.Text, Convert.ToInt32(this.numericUpDown1.Value));
      main.Show();
      this.Hide();
    }
  }
}
