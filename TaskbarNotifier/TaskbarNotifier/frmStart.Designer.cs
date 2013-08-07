namespace TaskbarNotifier
{
    partial class frmStart
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.taskbarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmItenExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemOpenERP = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.taskbarMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.taskbarMenu;
            this.notifyIcon1.Text = "Taskbar Notifier";
            this.notifyIcon1.Visible = true;
            // 
            // taskbarMenu
            // 
            this.taskbarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemOpenERP,
            this.tsmItemLogout,
            this.tsmItenExit});
            this.taskbarMenu.Name = "contextMenuStrip1";
            this.taskbarMenu.Size = new System.Drawing.Size(153, 98);
            // 
            // tsmItenExit
            // 
            this.tsmItenExit.Name = "tsmItenExit";
            this.tsmItenExit.Size = new System.Drawing.Size(152, 24);
            this.tsmItenExit.Text = "退出";
            this.tsmItenExit.Click += new System.EventHandler(this.tsmItenExit_Click);
            // 
            // tsmItemOpenERP
            // 
            this.tsmItemOpenERP.Name = "tsmItemOpenERP";
            this.tsmItemOpenERP.Size = new System.Drawing.Size(152, 24);
            this.tsmItemOpenERP.Text = "打开ERP";
            this.tsmItemOpenERP.Click += new System.EventHandler(this.tsmItemOpenERP_Click);
            // 
            // tsmItemLogout
            // 
            this.tsmItemLogout.Name = "tsmItemLogout";
            this.tsmItemLogout.Size = new System.Drawing.Size(152, 24);
            this.tsmItemLogout.Text = "注销";
            this.tsmItemLogout.Click += new System.EventHandler(this.tsmItemLogout_Click);
            // 
            // frmStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 254);
            this.Name = "frmStart";
            this.ShowInTaskbar = false;
            this.Text = "frmStart";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmStart_Load);
            this.taskbarMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip taskbarMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmItenExit;
        private System.Windows.Forms.ToolStripMenuItem tsmItemOpenERP;
        private System.Windows.Forms.ToolStripMenuItem tsmItemLogout;
    }
}