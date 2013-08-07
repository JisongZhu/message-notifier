using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TaskbarNotifier
{
    public partial class frmTaskbarNotifier : Form
    {
        private double formOpacity;

        public frmTaskbarNotifier(MessageModel message)
        {
            InitializeComponent();
            Screen screen = Screen.PrimaryScreen;//获取屏幕变量
            this.Height = 300;
            this.Width = 410;
            this.Location = new Point(screen.WorkingArea.Width - 425, screen.WorkingArea.Height - 320);
            this.Opacity = 0;

            fillNotifierContent(message);
        }

        public double FormOpacity
        {
            set
            {
                formOpacity = value;
            }
            get
            {
                return formOpacity;
            }
        }

        public void SetLocation(Point value)
        {
            this.Location = value;
        }

        public void SetOpacity(double value)
        {
            this.Opacity = value;
        }

        public void ScrollShow()
        {
            this.Show();

            this.timer1.Enabled = true;
        }

        private void ScrollUp()
        {
            if (FormOpacity < 1)
            {
                FormOpacity += 0.1;
                this.Opacity = FormOpacity;
            }
            else
            {
                this.timer1.Enabled = false;
            }
        }

        private void fillNotifierContent(MessageModel messageModel)
        {
            this.lblDate.Text = "创建日期： " + messageModel.CreateTime;
            this.lnkTitle.Text = messageModel.Title;
            this.lnkTitle.Enabled = true;
            this.lnkTitle.LinkArea = new LinkArea(0, this.lnkTitle.Text.Length);
            this.lnkTitle.Links[0].LinkData = ApplicationContext.ServerHost() 
                + "index.php?&sid="
                + ApplicationContext.Credential.SessionId;

            // update message state
            MysqlHelper.UpdateMessageState(messageModel.Id);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ScrollUp();
        }

        private void frmTaskbarNotifier_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void lnkTitle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start("iexplore.exe", target);
        }
    }
}
