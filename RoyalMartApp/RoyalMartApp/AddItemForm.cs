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
    public partial class AddItemForm : Form
    {
        string connString = AP.ConnectionStrings["RoyalMartConnStr"].ConnectionString;
        public AddItemForm()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Welcome! You can Add Items here!";
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "insert into items_table values('" + textBoxName.Text + "'," +
                    " '" + textBoxPrice.Text + "', '" + textBoxDiscount.Text + "')";
                int a = DataAccess.ExecuteNonQuery(sql);
                if (a > 0)
                {
                    MessageBox.Show("Inserted Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    toolStripStatusLabel1.Text = "Item Added!";
                    toolStripProgressBar1.Value = 100;
                    textBoxName.Clear();
                    textBoxPrice.Clear();
                    textBoxDiscount.Clear();
                    textBoxName.Focus();
                }
                else
                {
                    MessageBox.Show("Register Failed Something is Empty!", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
    }
}
