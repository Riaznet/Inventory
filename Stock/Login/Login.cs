using System;using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Common;
using System.Net;
using System.Threading;

namespace Stock.Login
{
    public partial class Login_ : Form
    {
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        DataSet dataSet = new DataSet();
        public static string SMSActive = "";
        BackgroundWorker bw;
        public Login_()
        {
            InitializeComponent();
            LoadData();
            string userId =Properties.Settings.Default.UserId;
            string Passowrd= Properties.Settings.Default.Password;
            if (txtPassword.Text != "" && txtUserId.Text != "")
            {
                chkRemember.Checked = true;
                txtUserId.Text = userId;
                txtPassword.Text = Passowrd;
            }
            else
                chkRemember.Checked = false;
            txtUserId.Focus();
        }
        public void ShowExicuting()
        { 
            btnLogin.Text = "Checking...";
        }

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            if(txtUserId.Text=="USER ID")
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.LightGray;
                lblUser.ForeColor = Color.LightGray;

            }
        }
        private void LoadData()
        {
            try
            {
                dataSet = BaseClass.DataSetData("select * from Resigtration where Status='A'");
            }
            catch
            {
                //Form fn = new Database_Configur();
                //fn.ShowDialog();
                //this.Close();
            }
        }
        private void txtUserId_Leave(object sender, EventArgs e)
        {
            if (txtUserId.Text == "")
            {
                txtUserId.Text = "USER ID";
                txtUserId.ForeColor = Color.DimGray;
                lblUser.ForeColor = Color.DimGray;
            }
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            if (txtPassword.Text == "PASSWORD")
            {
                txtPassword.Text = "";
                txtPassword.ForeColor = Color.LightGray;
                lblPassword.ForeColor = Color.LightGray;
                txtPassword.UseSystemPasswordChar = true;
            }

        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                txtPassword.Text = "PASSWORD";
                txtPassword.ForeColor = Color.DimGray;
                lblPassword.ForeColor = Color.DimGray;
                txtPassword.UseSystemPasswordChar = false;
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblMinim_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Login__MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void iconPictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        } 
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Form fn = new Parent();
                fn.Show();
                this.Close();
                //bw = new BackgroundWorker();
                //bw.DoWork += (obj, ea) => delay();
                //bw.RunWorkerAsync();
                //Thread th = new Thread(new ThreadStart(delay));
                //th.Start();
                //CheckAutontication();
                //new Thread(() =>
                //{
                //    btnSubmitLogIn.Invoke(new loading(ShowExicuting));
                //}).Start();
                //delay();
            }
            catch (Exception EX)
            {
                //picLoading.Visible = false;
            }
        }
        private async void delay()
        {
            for (int i = 0; i < 2000000000; i++)
            {

            }
        }
            private async void CheckAutontication()
        {
            try
            {
                //SqlConnection con = new SqlConnection(MyFunctions.connection);
                string uname = txtUserId.Text; //Get the username from the control
                string password = BaseClass.encryptHash(txtPassword.Text);//get the Password from the control

                if (uname == "")
                {
                    Invoke(new Action(() =>
                    {
                        lblMsg.Visible = true;
                        lblMsg .Text = "Username Required."; 
                        btnLogin.Text = "Log In"; 
                    }));
                }
                else if (password == "")
                {
                    Invoke(new Action(() =>
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "Password Required.";
                        btnLogin.Text = "Log In"; 
                    }));
                }
                else
                {
                    if (chkRemember.Checked == true)
                    { 
                        Properties.Settings.Default.UserId = txtUserId.Text;
                        Properties.Settings.Default.Password = txtPassword.Text;
                        Properties.Settings.Default.Save();
                    }
                    bool flag = AuthenticateUser(uname, password);
                    if (flag == true)
                    {
                        string strHostName = System.Net.Dns.GetHostName();
                        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                        IPAddress ipAddress = ipHostInfo.AddressList[0]; 
                        Session.IpAddress = ipAddress;
                        string UserTP = "";
                        DataRow[] result = dataSet.Tables[0].Select("loginid='" + uname + "'");
                        if (result.Length != 0)
                        {
                            string UserTp = result[0].ItemArray[7].ToString();
                            string Name = result[0].ItemArray[2].ToString();
                            string id = result[0].ItemArray[0].ToString();
                            string store = result[0].ItemArray[15].ToString();
                            Session.UserID = id;
                            Session.LoginId = uname;
                            UserTP = UserTp;
                            Session.UserTP = UserTP;
                            if (UserTP == "Admin" || UserTP == "Super Admin")
                                Session.UserStoreID = "";
                            else
                                Session.UserStoreID = store;
                            Session.UserNM = Name;
                            SMSActive = BaseClass.GetData("Select ActiveSMS from SMS_APIs");
                        }
                        Invoke(new Action(() =>
                        {
                            //picLoading.Show();
                            Form fn = new Parent();
                            fn.Show();
                            this.Hide();
                        }));


                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            btnLogin.Text = "Log In"; 
                            lblMsg.Visible = true;
                            lblMsg.Text = "User name or password mismatch."; 
                            txtPassword.Text = "";
                            txtPassword.Focus();
                        }));
                    }

                }
            }
            catch { }
        }
        private bool AuthenticateUser(string uname, string password)
        {
            bool bflag = false;
            string data = "";
            try
            {
                string s = "riazripon";
                DataRow[] result = dataSet.Tables[0].Select("loginid='" + uname + "'");
                if (result.Length != 0)
                {
                    string pw = result[0].ItemArray[3].ToString();
                    if (pw == password)
                        bflag = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return bflag;
        }
         
    }
}
