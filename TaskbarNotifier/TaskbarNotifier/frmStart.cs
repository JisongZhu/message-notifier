using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Net;
using System.IO;
using LitJson;

namespace TaskbarNotifier
{
    public partial class frmStart : Form
    {

        public frmStart()
        {
            InitializeComponent();
        }

        public void ActiveTimer()
        {
            this.timer1.Enabled = true;
            this.tsmItemLogout.Text = "注销";
        }

        private void tsmItenExit_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = false;
            this.Close();
            this.Dispose();
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            List<MessageModel> resultList = MysqlHelper.GetNewMessage(ApplicationContext.Credential.UserId);
            if (resultList.Count != 0)
            {
                foreach (MessageModel message in resultList)
                {
                    frmTaskbarNotifier tn = new frmTaskbarNotifier(message);
                    tn.ScrollShow();
                }
            }
        }

        private void frmStart_Load(object sender, EventArgs e)
        {
            MysqlHelper.InitData();
            this.timer1.Interval = ApplicationContext.TimerSpan();
            //指定一个图标
            this.notifyIcon1.Icon = new Icon(System.Windows.Forms.Application.StartupPath + "\\notify.ico");

            // 查询user表，如果有账号，则自动登录；否则，弹出登录窗口
            DbDataReader reader = null;
            try
            {
                reader = SQLiteHelper.ExecuteReader("SELECT id, username, password FROM user WHERE id=1", CommandType.Text);
                if (reader != null && reader.HasRows)
                {
                    string username = "";
                    string password = "";
                    while (reader.Read())
                    {
                        username = Convert.IsDBNull(reader["username"]) ? "" : reader["username"].ToString();
                        password = Convert.IsDBNull(reader["password"]) ? "" : reader["password"].ToString();
                    }
                    DoAutoLogin(username, password);
                }
                else
                {
                    this.ShowLoginForm();
                }
            }
            catch (Exception ex)
            {
                this.ShowLoginForm();
            }
        }

        private void tsmItemLogout_Click(object sender, EventArgs e)
        {
            if (this.tsmItemLogout.Text == "登录")
            {
                this.ShowLoginForm();
            }
            else
            {
                try
                {
                    SQLiteHelper.ExecuteNonQuery("DELETE FROM user", CommandType.Text);
                    ApplicationContext.Logout();
                    this.timer1.Enabled = false;
                    this.ShowLoginForm();
                    this.tsmItemLogout.Text = "登录";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("注销账号失败");
                }
            }
        }

        private void tsmItemOpenERP_Click(object sender, EventArgs e)
        {
            string target = ApplicationContext.ServerHost()
                + "index.php?sid="
                + ApplicationContext.Credential.SessionId;
            System.Diagnostics.Process.Start("iexplore.exe", target);
        }

        private void ShowLoginForm()
        {
            var loginForm = new frmLogin(this);
            loginForm.Show();
        }

        private void DoAutoLogin(string username, string password)
        {
            string postData = "tuser=" + username;
            postData += ("&pw=" + password);
            postData += "&from=1";

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(ApplicationContext.ServerHost() + "login2.php");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            Stream newStream = myRequest.GetRequestStream();
            StreamWriter sw = new StreamWriter(newStream, Encoding.Default);
            sw.Write(postData);
            sw.Close();
            newStream.Close();
            try
            {
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.Default);
                string content = reader.ReadToEnd();
                JsonData jsonData = JsonMapper.ToObject(content);
                CredentialModel credential = new CredentialModel();
                credential.StatusCode = (int)jsonData["success"];
                credential.Message = (string)jsonData["msg"];
                if (credential.StatusCode != 1)
                {
                    this.ShowLoginForm();
                }
                else
                {
                    credential.UserId = (string)jsonData["emp_no"];
                    credential.SessionId = (string)jsonData["sid"];
                    ApplicationContext.Credential = credential;
                    this.ActiveTimer();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
