using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoyalMartApp
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "You are on About Page!";
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            lbl.Text = "This will close entire Application";
            btnExit.Text = "Sure?";
            btnExit.BackColor = Color.Green;
            //btnExit.Font = new Font("mv boli", 26);
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            lbl.Text = "";
            btnExit.Text = "Exit";
            btnExit.BackColor = Color.Coral;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
