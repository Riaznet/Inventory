﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock.Login
{
    public partial class Background : Form
    {
        public Background()
        {
            InitializeComponent();
            
        }

        private void Background_Load(object sender, EventArgs e)
        {

            Form fn = new Login_();
            fn.ShowDialog();
        }
    }
}
