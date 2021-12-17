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
    public partial class SearchByName : Form
    {
        string connString = AP.ConnectionStrings["RoyalMartConnStr"].ConnectionString;
        public SearchByName()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "You are at Search by Name Form";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $@"
                            SELECT 
	                        A.invoice_id,	                           
                            A.username,
                            A.[datetime],
	                        B.item_name,
	                        B.unit_price,
	                        B.discount_peritem,
	                        B.quantity, 
	                        B.subtotal,
	                        B.tax,
	                        B.totalcost,
	                        A.finalcost
                            from order_master  as   A
                            INNER JOIN    order_details as   B
                            ON A.invoice_id = B.invoice_id
                            WHERE A.username = '{txtBoxSearchByName.Text.Trim()}'
                ";
                DataTable data = DataAccess.GetData(sql);
                dataGridView.DataSource = data;

                dataGridView.Columns[10].Visible = false;
                txtfinalCost.Text = dataGridView.Rows[0].Cells[10].Value.ToString();

                toolStripProgressBar1.Value = 100;
                toolStripStatusLabel1.Text = $"You are watching {txtBoxSearchByName.Text}'s Data";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }
    }
}