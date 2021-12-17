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

namespace RoyalMartApp
{
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $@"insert into signup values('{NametextBox.Text}', '{SurnametextBox.Text}', 
                            '{GendercomboBox1.SelectedItem}', 
                            '{AgenumericUpDown1.Value}', '{AddresstextBox.Text}', '{EmailtextBox.Text}',
                            '{PasswordtextBox.Text}')
                ";


                int a = DataAccess.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    MessageBox.Show("Register Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show($"Username is: '{NametextBox.Text}' \n Password is: '{PasswordtextBox.Text}'", "Your Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Login loginForm = new Login();
                    this.Hide();
                    loginForm.ShowDialog();

                    frmSplashScreen splash = new frmSplashScreen();
                    splash.Hide();
                }
                else
                {
                    MessageBox.Show("Register Failed !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {         
            Login loginForm = new Login();
            this.Hide();
            loginForm.ShowDialog();
        }
    }
}
