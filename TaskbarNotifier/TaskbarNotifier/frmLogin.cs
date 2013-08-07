using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using LitJson;

namespace TaskbarNotifier
{
    public partial class frmLogin : Form
    {
        private frmStart startForm;
        public frmLogin(frmStart startForm)
        {
            InitializeComponent();

            this.startForm = startForm;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.lblWarning.Text = "";
            string username = this.txtUsername.Text.Trim();
            string password = this.txtPwd.Text.Trim();
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                this.lblWarning.Text = "用户名或密码不能为空！";
                return;
            }
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
                    this.txtPwd.Text = "";
                    this.lblWarning.Text = credential.Message;
                }
                else
                {
                    credential.UserId = (string)jsonData["emp_no"];
                    credential.SessionId = (string)jsonData["sid"];
                    ApplicationContext.Credential = credential;
                    try
                    {
                        string commandText = "INSERT INTO user (id, username, password) VALUES (1, '"
                            + username 
                            + "', '" 
                            + password 
                            + "')";
                        SQLiteHelper.ExecuteNonQuery(commandText, CommandType.Text);
                    }
                    catch (Exception ex)
                    {
                    }
                    this.startForm.ActiveTimer();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.txtPwd.Text = "";
                this.lblWarning.Text = "登录失败！";
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }
    }
}
