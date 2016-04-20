namespace TestSocketClient {
  partial class Main {
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
      this.btnSend = new System.Windows.Forms.Button();
      this.textBoxClientId = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textMessage = new System.Windows.Forms.TextBox();
      this.lblOutput = new System.Windows.Forms.Label();
      this.txtReceieved = new System.Windows.Forms.TextBox();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.toolStripConnectionLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.lblPort = new System.Windows.Forms.Label();
      this.lblEndpoint = new System.Windows.Forms.Label();
      this.comboEndpoint = new System.Windows.Forms.ComboBox();
      this.numericPort = new System.Windows.Forms.NumericUpDown();
      this.btnConnect = new System.Windows.Forms.Button();
      this.statusStrip.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericPort)).BeginInit();
      this.SuspendLayout();
      // 
      // btnSend
      // 
      this.btnSend.Location = new System.Drawing.Point(447, 43);
      this.btnSend.Name = "btnSend";
      this.btnSend.Size = new System.Drawing.Size(75, 23);
      this.btnSend.TabIndex = 0;
      this.btnSend.Text = "Send";
      this.btnSend.UseVisualStyleBackColor = true;
      this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
      // 
      // textBoxClientId
      // 
      this.textBoxClientId.Location = new System.Drawing.Point(60, 46);
      this.textBoxClientId.Name = "textBoxClientId";
      this.textBoxClientId.ReadOnly = true;
      this.textBoxClientId.Size = new System.Drawing.Size(368, 20);
      this.textBoxClientId.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(4, 49);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(45, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Client Id";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(4, 75);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(50, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Message";
      // 
      // textMessage
      // 
      this.textMessage.Location = new System.Drawing.Point(60, 72);
      this.textMessage.Name = "textMessage";
      this.textMessage.Size = new System.Drawing.Size(465, 20);
      this.textMessage.TabIndex = 3;
      // 
      // lblOutput
      // 
      this.lblOutput.AutoSize = true;
      this.lblOutput.Location = new System.Drawing.Point(4, 110);
      this.lblOutput.Name = "lblOutput";
      this.lblOutput.Size = new System.Drawing.Size(104, 13);
      this.lblOutput.TabIndex = 5;
      this.lblOutput.Text = "Received Messages";
      // 
      // txtReceieved
      // 
      this.txtReceieved.Location = new System.Drawing.Point(7, 127);
      this.txtReceieved.Multiline = true;
      this.txtReceieved.Name = "txtReceieved";
      this.txtReceieved.ReadOnly = true;
      this.txtReceieved.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtReceieved.Size = new System.Drawing.Size(519, 105);
      this.txtReceieved.TabIndex = 6;
      // 
      // statusStrip
      // 
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripConnectionLabel});
      this.statusStrip.Location = new System.Drawing.Point(0, 239);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
      this.statusStrip.Size = new System.Drawing.Size(532, 22);
      this.statusStrip.TabIndex = 7;
      this.statusStrip.Text = "statusStrip";
      // 
      // toolStripConnectionLabel
      // 
      this.toolStripConnectionLabel.Name = "toolStripConnectionLabel";
      this.toolStripConnectionLabel.Size = new System.Drawing.Size(142, 17);
      this.toolStripConnectionLabel.Text = "toolStripConnectionLabel";
      // 
      // lblPort
      // 
      this.lblPort.AutoSize = true;
      this.lblPort.Location = new System.Drawing.Point(324, 12);
      this.lblPort.Name = "lblPort";
      this.lblPort.Size = new System.Drawing.Size(26, 13);
      this.lblPort.TabIndex = 9;
      this.lblPort.Text = "Port";
      // 
      // lblEndpoint
      // 
      this.lblEndpoint.AutoSize = true;
      this.lblEndpoint.Location = new System.Drawing.Point(5, 14);
      this.lblEndpoint.Name = "lblEndpoint";
      this.lblEndpoint.Size = new System.Drawing.Size(49, 13);
      this.lblEndpoint.TabIndex = 8;
      this.lblEndpoint.Text = "Endpoint";
      this.lblEndpoint.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // comboEndpoint
      // 
      this.comboEndpoint.FormattingEnabled = true;
      this.comboEndpoint.Location = new System.Drawing.Point(60, 9);
      this.comboEndpoint.Name = "comboEndpoint";
      this.comboEndpoint.Size = new System.Drawing.Size(258, 21);
      this.comboEndpoint.TabIndex = 10;
      // 
      // numericPort
      // 
      this.numericPort.Location = new System.Drawing.Point(357, 10);
      this.numericPort.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
      this.numericPort.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
      this.numericPort.Name = "numericPort";
      this.numericPort.Size = new System.Drawing.Size(71, 20);
      this.numericPort.TabIndex = 11;
      this.numericPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
      // 
      // btnConnect
      // 
      this.btnConnect.Location = new System.Drawing.Point(447, 9);
      this.btnConnect.Name = "btnConnect";
      this.btnConnect.Size = new System.Drawing.Size(75, 23);
      this.btnConnect.TabIndex = 12;
      this.btnConnect.Text = "Connect";
      this.btnConnect.UseVisualStyleBackColor = true;
      this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(532, 261);
      this.Controls.Add(this.btnConnect);
      this.Controls.Add(this.numericPort);
      this.Controls.Add(this.comboEndpoint);
      this.Controls.Add(this.lblPort);
      this.Controls.Add(this.lblEndpoint);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.txtReceieved);
      this.Controls.Add(this.lblOutput);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textMessage);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxClientId);
      this.Controls.Add(this.btnSend);
      this.MaximizeBox = false;
      this.Name = "Main";
      this.Text = "Test Socket Client";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.numericPort)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnSend;
    private System.Windows.Forms.TextBox textBoxClientId;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textMessage;
    private System.Windows.Forms.Label lblOutput;
    private System.Windows.Forms.TextBox txtReceieved;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.ToolStripStatusLabel toolStripConnectionLabel;
    private System.Windows.Forms.Label lblPort;
    private System.Windows.Forms.Label lblEndpoint;
    private System.Windows.Forms.ComboBox comboEndpoint;
    private System.Windows.Forms.NumericUpDown numericPort;
    private System.Windows.Forms.Button btnConnect;
  }
}

