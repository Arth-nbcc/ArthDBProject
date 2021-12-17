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
    public partial class ViewDataForm : Form
    {
        string connString = AP.ConnectionStrings["RoyalMartConnStr"].ConnectionString;
        public ViewDataForm()
        {
            InitializeComponent();
            BindGridView();
        }

        void BindGridView()
        {
            string sql = "select * from items_table";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                {
                    DataTable data = new DataTable();

                    sda.Fill(data);
                    dataGridView1.DataSource = data;
                }
            }
        }

        private void btnIwanttoEdit_Click(object sender, EventArgs e)
        {
            frmManagement edf = new frmManagement();
            this.Hide();
            edf.ShowDialog();
        }
    }
}
