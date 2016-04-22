namespace AsyncDemo.ServerApplication {
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
      this.splitContainer = new System.Windows.Forms.SplitContainer();
      this.listViewClients = new System.Windows.Forms.ListView();
      this.colClientId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnAdditional = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.txtClientId = new System.Windows.Forms.TextBox();
      this.lblClientId = new System.Windows.Forms.Label();
      this.btnStopClient = new System.Windows.Forms.Button();
      this.txtClientMessage = new System.Windows.Forms.TextBox();
      this.btnSendMessage = new System.Windows.Forms.Button();
      this.txtOutput = new System.Windows.Forms.TextBox();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
      this.splitContainer.Panel1.SuspendLayout();
      this.splitContainer.Panel2.SuspendLayout();
      this.splitContainer.SuspendLayout();
      this.SuspendLayout();
      // 
      // splitContainer
      // 
      this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer.Location = new System.Drawing.Point(0, 0);
      this.splitContainer.Name = "splitContainer";
      // 
      // splitContainer.Panel1
      // 
      this.splitContainer.Panel1.Controls.Add(this.listViewClients);
      // 
      // splitContainer.Panel2
      // 
      this.splitContainer.Panel2.Controls.Add(this.txtClientId);
      this.splitContainer.Panel2.Controls.Add(this.lblClientId);
      this.splitContainer.Panel2.Controls.Add(this.btnStopClient);
      this.splitContainer.Panel2.Controls.Add(this.txtClientMessage);
      this.splitContainer.Panel2.Controls.Add(this.btnSendMessage);
      this.splitContainer.Panel2.Controls.Add(this.txtOutput);
      this.splitContainer.Size = new System.Drawing.Size(857, 514);
      this.splitContainer.SplitterDistance = 285;
      this.splitContainer.TabIndex = 0;
      // 
      // listViewClients
      // 
      this.listViewClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.listViewClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colClientId,
            this.columnAdditional});
      this.listViewClients.FullRowSelect = true;
      this.listViewClients.GridLines = true;
      this.listViewClients.Location = new System.Drawing.Point(3, 3);
      this.listViewClients.Name = "listViewClients";
      this.listViewClients.Size = new System.Drawing.Size(279, 508);
      this.listViewClients.TabIndex = 0;
      this.listViewClients.UseCompatibleStateImageBehavior = false;
      this.listViewClients.View = System.Windows.Forms.View.Details;
      // 
      // colClientId
      // 
      this.colClientId.Text = "ClientId";
      this.colClientId.Width = 100;
      // 
      // columnAdditional
      // 
      this.columnAdditional.Text = "Infomation";
      this.columnAdditional.Width = 174;
      // 
      // txtClientId
      // 
      this.txtClientId.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.txtClientId.Location = new System.Drawing.Point(60, 17);
      this.txtClientId.Name = "txtClientId";
      this.txtClientId.ReadOnly = true;
      this.txtClientId.Size = new System.Drawing.Size(404, 13);
      this.txtClientId.TabIndex = 5;
      // 
      // lblClientId
      // 
      this.lblClientId.AutoSize = true;
      this.lblClientId.Location = new System.Drawing.Point(3, 17);
      this.lblClientId.Name = "lblClientId";
      this.lblClientId.Size = new System.Drawing.Size(51, 13);
      this.lblClientId.TabIndex = 4;
      this.lblClientId.Text = "Client Id: ";
      // 
      // btnStopClient
      // 
      this.btnStopClient.Location = new System.Drawing.Point(470, 12);
      this.btnStopClient.Name = "btnStopClient";
      this.btnStopClient.Size = new System.Drawing.Size(86, 23);
      this.btnStopClient.TabIndex = 3;
      this.btnStopClient.Text = "Stop Client";
      this.btnStopClient.UseVisualStyleBackColor = true;
      this.btnStopClient.Click += new System.EventHandler(this.btnStopClient_Click);
      // 
      // txtClientMessage
      // 
      this.txtClientMessage.Location = new System.Drawing.Point(3, 484);
      this.txtClientMessage.Name = "txtClientMessage";
      this.txtClientMessage.Size = new System.Drawing.Size(461, 20);
      this.txtClientMessage.TabIndex = 2;
      // 
      // btnSendMessage
      // 
      this.btnSendMessage.Location = new System.Drawing.Point(470, 482);
      this.btnSendMessage.Name = "btnSendMessage";
      this.btnSendMessage.Size = new System.Drawing.Size(95, 23);
      this.btnSendMessage.TabIndex = 1;
      this.btnSendMessage.Text = "Send Message";
      this.btnSendMessage.UseVisualStyleBackColor = true;
      this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
      // 
      // txtOutput
      // 
      this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.txtOutput.Location = new System.Drawing.Point(3, 41);
      this.txtOutput.Multiline = true;
      this.txtOutput.Name = "txtOutput";
      this.txtOutput.ReadOnly = true;
      this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.txtOutput.Size = new System.Drawing.Size(562, 435);
      this.txtOutput.TabIndex = 0;
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(857, 514);
      this.Controls.Add(this.splitContainer);
      this.Name = "Main";
      this.Text = "Server Application";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
      this.splitContainer.Panel1.ResumeLayout(false);
      this.splitContainer.Panel2.ResumeLayout(false);
      this.splitContainer.Panel2.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
      this.splitContainer.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.SplitContainer splitContainer;
    private System.Windows.Forms.ListView listViewClients;
    private System.Windows.Forms.TextBox txtOutput;
    private System.Windows.Forms.ColumnHeader colClientId;
    private System.Windows.Forms.ColumnHeader columnAdditional;
    private System.Windows.Forms.Button btnSendMessage;
    private System.Windows.Forms.TextBox txtClientMessage;
    private System.Windows.Forms.TextBox txtClientId;
    private System.Windows.Forms.Label lblClientId;
    private System.Windows.Forms.Button btnStopClient;
  }
}

