namespace WinFormsApp1
{
    partial class DemoTransmit
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.messageID1 = new System.Windows.Forms.TextBox();
            this.messageData1 = new System.Windows.Forms.TextBox();
            this.messageData2 = new System.Windows.Forms.TextBox();
            this.messageID2 = new System.Windows.Forms.TextBox();
            this.messageData3 = new System.Windows.Forms.TextBox();
            this.messageID3 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IndexColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TimeStampColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLCColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BusColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.Label();
            this.Data = new System.Windows.Forms.Label();
            this.sendMessage1 = new System.Windows.Forms.Button();
            this.sendMessage2 = new System.Windows.Forms.Button();
            this.sendMessage3 = new System.Windows.Forms.Button();
            this.sendAllMessage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // messageID1
            // 
            this.messageID1.Location = new System.Drawing.Point(12, 30);
            this.messageID1.Name = "messageID1";
            this.messageID1.Size = new System.Drawing.Size(97, 27);
            this.messageID1.TabIndex = 0;
            // 
            // messageData1
            // 
            this.messageData1.Location = new System.Drawing.Point(115, 30);
            this.messageData1.Name = "messageData1";
            this.messageData1.Size = new System.Drawing.Size(273, 27);
            this.messageData1.TabIndex = 1;
            // 
            // messageData2
            // 
            this.messageData2.Location = new System.Drawing.Point(115, 63);
            this.messageData2.Name = "messageData2";
            this.messageData2.Size = new System.Drawing.Size(273, 27);
            this.messageData2.TabIndex = 3;
            // 
            // messageID2
            // 
            this.messageID2.Location = new System.Drawing.Point(12, 63);
            this.messageID2.Name = "messageID2";
            this.messageID2.Size = new System.Drawing.Size(97, 27);
            this.messageID2.TabIndex = 2;
            // 
            // messageData3
            // 
            this.messageData3.Location = new System.Drawing.Point(115, 96);
            this.messageData3.Name = "messageData3";
            this.messageData3.Size = new System.Drawing.Size(273, 27);
            this.messageData3.TabIndex = 5;
            // 
            // messageID3
            // 
            this.messageID3.Location = new System.Drawing.Point(12, 96);
            this.messageID3.Name = "messageID3";
            this.messageID3.Size = new System.Drawing.Size(97, 27);
            this.messageID3.TabIndex = 4;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndexColumn,
            this.TimeStampColumn,
            this.IDColumn,
            this.DataColumn,
            this.DLCColumn,
            this.BusColumn});
            this.dataGridView1.Location = new System.Drawing.Point(12, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(792, 286);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // IndexColumn
            // 
            this.IndexColumn.HeaderText = "Index";
            this.IndexColumn.MinimumWidth = 6;
            this.IndexColumn.Name = "IndexColumn";
            this.IndexColumn.ReadOnly = true;
            this.IndexColumn.Width = 62;
            // 
            // TimeStampColumn
            // 
            this.TimeStampColumn.HeaderText = "Time Stamp";
            this.TimeStampColumn.MinimumWidth = 6;
            this.TimeStampColumn.Name = "TimeStampColumn";
            this.TimeStampColumn.ReadOnly = true;
            this.TimeStampColumn.Width = 160;
            // 
            // IDColumn
            // 
            this.IDColumn.HeaderText = "ID";
            this.IDColumn.MinimumWidth = 6;
            this.IDColumn.Name = "IDColumn";
            this.IDColumn.ReadOnly = true;
            this.IDColumn.Width = 90;
            // 
            // DataColumn
            // 
            this.DataColumn.HeaderText = "Data";
            this.DataColumn.MinimumWidth = 6;
            this.DataColumn.Name = "DataColumn";
            this.DataColumn.ReadOnly = true;
            this.DataColumn.Width = 270;
            // 
            // DLCColumn
            // 
            this.DLCColumn.HeaderText = "DLC";
            this.DLCColumn.MinimumWidth = 6;
            this.DLCColumn.Name = "DLCColumn";
            this.DLCColumn.ReadOnly = true;
            this.DLCColumn.Width = 62;
            // 
            // BusColumn
            // 
            this.BusColumn.HeaderText = "Bus";
            this.BusColumn.MinimumWidth = 6;
            this.BusColumn.Name = "BusColumn";
            this.BusColumn.ReadOnly = true;
            this.BusColumn.Width = 122;
            // 
            // ID
            // 
            this.ID.AutoSize = true;
            this.ID.Location = new System.Drawing.Point(49, 7);
            this.ID.Name = "ID";
            this.ID.Size = new System.Drawing.Size(24, 20);
            this.ID.TabIndex = 7;
            this.ID.Text = "ID";
            // 
            // Data
            // 
            this.Data.AutoSize = true;
            this.Data.Location = new System.Drawing.Point(224, 7);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(41, 20);
            this.Data.TabIndex = 8;
            this.Data.Text = "Data";
            // 
            // sendMessage1
            // 
            this.sendMessage1.Location = new System.Drawing.Point(394, 28);
            this.sendMessage1.Name = "sendMessage1";
            this.sendMessage1.Size = new System.Drawing.Size(112, 29);
            this.sendMessage1.TabIndex = 9;
            this.sendMessage1.Text = "Send";
            this.sendMessage1.UseVisualStyleBackColor = true;
            this.sendMessage1.Click += new System.EventHandler(this.sendMessage1_Click);
            // 
            // sendMessage2
            // 
            this.sendMessage2.Location = new System.Drawing.Point(394, 63);
            this.sendMessage2.Name = "sendMessage2";
            this.sendMessage2.Size = new System.Drawing.Size(112, 29);
            this.sendMessage2.TabIndex = 10;
            this.sendMessage2.Text = "Send";
            this.sendMessage2.UseVisualStyleBackColor = true;
            this.sendMessage2.Click += new System.EventHandler(this.sendMessage2_Click);
            // 
            // sendMessage3
            // 
            this.sendMessage3.Location = new System.Drawing.Point(394, 96);
            this.sendMessage3.Name = "sendMessage3";
            this.sendMessage3.Size = new System.Drawing.Size(112, 29);
            this.sendMessage3.TabIndex = 11;
            this.sendMessage3.Text = "Send";
            this.sendMessage3.UseVisualStyleBackColor = true;
            this.sendMessage3.Click += new System.EventHandler(this.sendMessage3_Click);
            // 
            // sendAllMessage
            // 
            this.sendAllMessage.Location = new System.Drawing.Point(512, 30);
            this.sendAllMessage.Name = "sendAllMessage";
            this.sendAllMessage.Size = new System.Drawing.Size(104, 95);
            this.sendAllMessage.TabIndex = 12;
            this.sendAllMessage.Text = "Send All";
            this.sendAllMessage.UseVisualStyleBackColor = true;
            this.sendAllMessage.Click += new System.EventHandler(this.sendAllMessage_Click);
            // 
            // DemoTransmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 444);
            this.Controls.Add(this.sendAllMessage);
            this.Controls.Add(this.sendMessage3);
            this.Controls.Add(this.sendMessage2);
            this.Controls.Add(this.sendMessage1);
            this.Controls.Add(this.Data);
            this.Controls.Add(this.ID);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.messageData3);
            this.Controls.Add(this.messageID3);
            this.Controls.Add(this.messageData2);
            this.Controls.Add(this.messageID2);
            this.Controls.Add(this.messageData1);
            this.Controls.Add(this.messageID1);
            this.MaximizeBox = false;
            this.Name = "DemoTransmit";
            this.Text = "DemoTransmit";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox messageID1;
        private TextBox messageData1;
        private TextBox messageData2;
        private TextBox messageID2;
        private TextBox messageData3;
        private TextBox messageID3;
        public DataGridView dataGridView1;
        private Label ID;
        private Label Data;
        private DataGridViewTextBoxColumn IndexColumn;
        private DataGridViewTextBoxColumn TimeStampColumn;
        private DataGridViewTextBoxColumn IDColumn;
        private DataGridViewTextBoxColumn DataColumn;
        private DataGridViewTextBoxColumn DLCColumn;
        private DataGridViewTextBoxColumn BusColumn;
        private Button sendMessage1;
        private Button sendMessage2;
        private Button sendMessage3;
        private Button sendAllMessage;
    }
}