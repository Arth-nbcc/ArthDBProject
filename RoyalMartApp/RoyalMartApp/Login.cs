using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using AP = System.Configuration.ConfigurationManager;
using System.Threading;

namespace RoyalMartApp
{
    public partial class Login : Form
    {
        public static string username = "";

        public Login()
        {
            InitializeComponent();

            Thread t = new Thread(new ThreadStart(SplashStart));
            t.Start();
            Thread.Sleep(12000);
            t.Abort();
        }

        public void SplashStart()
        {
            Application.Run(new frmSplashScreen());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_Username.Text != "" && textBox_Password.Text != "")
                {
                    //sql query
                    string sql = $@"select * from signup where name= '{textBox_Username.Text}' 
                    and password = '{textBox_Password.Text}'";
                    DataTable dt = DataAccess.GetData(sql);
                    if (dt != null & dt.Rows.Count > 0)
                    {
                        MessageBox.Show("LOGIN SUCCESSFULL !!!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        username = textBox_Username.Text;
                        this.Hide();
                        MdiParent MainForm = new MdiParent();
                        MainForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("LOGIN FAILED! Something is Incorrect :(", "FAILURE", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Enter Both Fields !!!", "FAILURE", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //bool is for true and false
            //if checkBox is checked, it will set as true or else, false.
            bool check = checkBox1.Checked;

            switch (check)              //it can also be done by if-else 
            {
                case true:
                    textBox_Password.UseSystemPasswordChar = false;
                    break;

                default:
                    textBox_Password.UseSystemPasswordChar = true;
                    break;
            }
        }

        private void textBox_Username_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Username.Text) == true)
            {
                textBox_Username.Focus();
                errorProvider1.SetError(this.textBox_Username, "Please Fill UserName");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textBox_Password_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Password.Text) == true)
            {
                textBox_Password.Focus();
                errorProvider2.SetError(this.textBox_Password, "Please Fill Password");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signup su = new Signup();
            this.Hide();
            su.ShowDialog();

        }
    }
}

