namespace AsyncDemo.ServerApplication {
  partial class Configuration {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.comboIpAddr = new System.Windows.Forms.ComboBox();
      this.lblAddress = new System.Windows.Forms.Label();
      this.lblPort = new System.Windows.Forms.Label();
      this.numPort = new System.Windows.Forms.NumericUpDown();
      this.btnVerify = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
      this.SuspendLayout();
      // 
      // comboIpAddr
      // 
      this.comboIpAddr.FormattingEnabled = true;
      this.comboIpAddr.Location = new System.Drawing.Point(76, 6);
      this.comboIpAddr.Name = "comboIpAddr";
      this.comboIpAddr.Size = new System.Drawing.Size(333, 21);
      this.comboIpAddr.TabIndex = 0;
      // 
      // lblAddress
      // 
      this.lblAddress.AutoSize = true;
      this.lblAddress.Location = new System.Drawing.Point(12, 9);
      this.lblAddress.Name = "lblAddress";
      this.lblAddress.Size = new System.Drawing.Size(58, 13);
      this.lblAddress.TabIndex = 1;
      this.lblAddress.Text = "IP Address";
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(12, 33);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 2;
      this.lblPort.Text = "Port";
      // 
      // numPort
      // 
      this.numPort.Location = new System.Drawing.Point(76, 31);
      this.numPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
      this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.numPort.Name = "numPort";
      this.numPort.Size = new System.Drawing.Size(120, 20);
      this.numPort.TabIndex = 4;
      this.numPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
      // 
      // btnVerify
      // 
      this.btnVerify.Location = new System.Drawing.Point(196, 60);
      this.btnVerify.Name = "btnVerify";
      this.btnVerify.Size = new System.Drawing.Size(111, 29);
      this.btnVerify.TabIndex = 5;
      this.btnVerify.Text = "Verify Configuration";
      this.btnVerify.UseVisualStyleBackColor = true;
      this.btnVerify.Click += new System.EventHandler(this.button1_Click);
      // 
      // btnStart
      // 
      this.btnStart.Location = new System.Drawing.Point(313, 60);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(96, 29);
      this.btnStart.TabIndex = 6;
      this.btnStart.Text = "Start Server";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.button2_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 68);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(0, 13);
      this.label3.TabIndex = 7;
      // 
      // Configuration
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(419, 98);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnStart);
      this.Controls.Add(this.btnVerify);
      this.Controls.Add(this.numPort);
      this.Controls.Add(this.lblPort);
      this.Controls.Add(this.lblAddress);
      this.Controls.Add(this.comboIpAddr);
      this.Name = "Configuration";
      this.Text = "Configure Server";
      this.Load += new System.EventHandler(this.Configuration_Load);
      ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ComboBox comboIpAddr;
    private System.Windows.Forms.Label lblAddress;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.NumericUpDown numPort;
    private System.Windows.Forms.Button btnVerify;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Label label3;
  }
}