namespace AutoTele
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rtxt_groups = new System.Windows.Forms.RichTextBox();
            this.rtxt_chats = new System.Windows.Forms.RichTextBox();
            this.btn_selectLD = new System.Windows.Forms.Button();
            this.txt_ldplayer_path = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_selectGroupPath = new System.Windows.Forms.Button();
            this.txt_grouppath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_selectChatPath = new System.Windows.Forms.Button();
            this.txt_chatpath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_end = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.btn_Start = new System.Windows.Forms.Button();
            this.dtgv_device_account = new System.Windows.Forms.DataGridView();
            this.rtxt_console = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installTeleForAllDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_selectedLD = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_device_account)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_selectedLD);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dtgv_device_account);
            this.panel1.Controls.Add(this.rtxt_console);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 593);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.rtxt_groups);
            this.panel2.Controls.Add(this.rtxt_chats);
            this.panel2.Controls.Add(this.btn_selectLD);
            this.panel2.Controls.Add(this.txt_ldplayer_path);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btn_selectGroupPath);
            this.panel2.Controls.Add(this.txt_grouppath);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.btn_selectChatPath);
            this.panel2.Controls.Add(this.txt_chatpath);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btn_end);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.btn_Start);
            this.panel2.Location = new System.Drawing.Point(9, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(522, 530);
            this.panel2.TabIndex = 23;
            // 
            // rtxt_groups
            // 
            this.rtxt_groups.Location = new System.Drawing.Point(270, 210);
            this.rtxt_groups.Name = "rtxt_groups";
            this.rtxt_groups.ReadOnly = true;
            this.rtxt_groups.Size = new System.Drawing.Size(212, 169);
            this.rtxt_groups.TabIndex = 22;
            this.rtxt_groups.Text = "";
            // 
            // rtxt_chats
            // 
            this.rtxt_chats.Location = new System.Drawing.Point(26, 210);
            this.rtxt_chats.Name = "rtxt_chats";
            this.rtxt_chats.ReadOnly = true;
            this.rtxt_chats.Size = new System.Drawing.Size(212, 169);
            this.rtxt_chats.TabIndex = 21;
            this.rtxt_chats.Text = "";
            // 
            // btn_selectLD
            // 
            this.btn_selectLD.Location = new System.Drawing.Point(388, 405);
            this.btn_selectLD.Name = "btn_selectLD";
            this.btn_selectLD.Size = new System.Drawing.Size(75, 23);
            this.btn_selectLD.TabIndex = 19;
            this.btn_selectLD.Text = "select path";
            this.btn_selectLD.UseVisualStyleBackColor = true;
            this.btn_selectLD.Click += new System.EventHandler(this.btn_selectLD_Click);
            // 
            // txt_ldplayer_path
            // 
            this.txt_ldplayer_path.Location = new System.Drawing.Point(141, 407);
            this.txt_ldplayer_path.Name = "txt_ldplayer_path";
            this.txt_ldplayer_path.ReadOnly = true;
            this.txt_ldplayer_path.Size = new System.Drawing.Size(209, 20);
            this.txt_ldplayer_path.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 410);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "LD PATH";
            // 
            // btn_selectGroupPath
            // 
            this.btn_selectGroupPath.Location = new System.Drawing.Point(388, 484);
            this.btn_selectGroupPath.Name = "btn_selectGroupPath";
            this.btn_selectGroupPath.Size = new System.Drawing.Size(75, 23);
            this.btn_selectGroupPath.TabIndex = 16;
            this.btn_selectGroupPath.Text = "select path";
            this.btn_selectGroupPath.UseVisualStyleBackColor = true;
            this.btn_selectGroupPath.Click += new System.EventHandler(this.btn_selectGroupPath_Click);
            // 
            // txt_grouppath
            // 
            this.txt_grouppath.Location = new System.Drawing.Point(141, 486);
            this.txt_grouppath.Name = "txt_grouppath";
            this.txt_grouppath.ReadOnly = true;
            this.txt_grouppath.Size = new System.Drawing.Size(209, 20);
            this.txt_grouppath.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 489);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "GROUP PATH";
            // 
            // btn_selectChatPath
            // 
            this.btn_selectChatPath.Location = new System.Drawing.Point(388, 443);
            this.btn_selectChatPath.Name = "btn_selectChatPath";
            this.btn_selectChatPath.Size = new System.Drawing.Size(75, 23);
            this.btn_selectChatPath.TabIndex = 13;
            this.btn_selectChatPath.Text = "select path";
            this.btn_selectChatPath.UseVisualStyleBackColor = true;
            this.btn_selectChatPath.Click += new System.EventHandler(this.btn_selectChatPath_Click);
            // 
            // txt_chatpath
            // 
            this.txt_chatpath.Location = new System.Drawing.Point(141, 445);
            this.txt_chatpath.Name = "txt_chatpath";
            this.txt_chatpath.ReadOnly = true;
            this.txt_chatpath.Size = new System.Drawing.Size(209, 20);
            this.txt_chatpath.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 448);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "CHAT PATH";
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(146, 19);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(75, 23);
            this.btn_end.TabIndex = 9;
            this.btn_end.Text = "stop";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Number of thread";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(146, 66);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown1.TabIndex = 4;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(40, 19);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(82, 23);
            this.btn_Start.TabIndex = 2;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // dtgv_device_account
            // 
            this.dtgv_device_account.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgv_device_account.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgv_device_account.Location = new System.Drawing.Point(818, 44);
            this.dtgv_device_account.Name = "dtgv_device_account";
            this.dtgv_device_account.Size = new System.Drawing.Size(272, 238);
            this.dtgv_device_account.TabIndex = 22;
            // 
            // rtxt_console
            // 
            this.rtxt_console.Location = new System.Drawing.Point(541, 44);
            this.rtxt_console.Name = "rtxt_console";
            this.rtxt_console.Size = new System.Drawing.Size(271, 531);
            this.rtxt_console.TabIndex = 21;
            this.rtxt_console.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1098, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolToolStripMenuItem
            // 
            this.toolToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.installTeleForAllDeviceToolStripMenuItem});
            this.toolToolStripMenuItem.Name = "toolToolStripMenuItem";
            this.toolToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.toolToolStripMenuItem.Text = "Menu";
            // 
            // installTeleForAllDeviceToolStripMenuItem
            // 
            this.installTeleForAllDeviceToolStripMenuItem.Name = "installTeleForAllDeviceToolStripMenuItem";
            this.installTeleForAllDeviceToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.installTeleForAllDeviceToolStripMenuItem.Text = "install tele for all device";
            this.installTeleForAllDeviceToolStripMenuItem.Click += new System.EventHandler(this.installTeleForAllDeviceToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(818, 337);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(272, 238);
            this.dataGridView1.TabIndex = 25;
            // 
            // btn_selectedLD
            // 
            this.btn_selectedLD.Location = new System.Drawing.Point(910, 299);
            this.btn_selectedLD.Name = "btn_selectedLD";
            this.btn_selectedLD.Size = new System.Drawing.Size(102, 23);
            this.btn_selectedLD.TabIndex = 26;
            this.btn_selectedLD.Text = "selected";
            this.btn_selectedLD.UseVisualStyleBackColor = true;
            this.btn_selectedLD.Click += new System.EventHandler(this.btn_selectedLD_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 593);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgv_device_account)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_selectLD;
        private System.Windows.Forms.TextBox txt_ldplayer_path;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_selectGroupPath;
        private System.Windows.Forms.TextBox txt_grouppath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_selectChatPath;
        private System.Windows.Forms.TextBox txt_chatpath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.DataGridView dtgv_device_account;
        private System.Windows.Forms.RichTextBox rtxt_console;
        private System.Windows.Forms.RichTextBox rtxt_groups;
        private System.Windows.Forms.RichTextBox rtxt_chats;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installTeleForAllDeviceToolStripMenuItem;
        private System.Windows.Forms.Button btn_selectedLD;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

