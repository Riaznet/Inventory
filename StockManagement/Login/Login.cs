using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NewTemplate.Login
{
    public partial class Login_ : Form
    {
        public Login_()
        {
            InitializeComponent();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void txtUserId_Enter(object sender, EventArgs e)
        {
            if(txtUserId.Text=="USER ID")
            {
                txtUserId.Text = "";
                txtUserId.ForeColor = Color.LightGray;
                lblUser.ForeColor = Color.LightGray;

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
    }
}
