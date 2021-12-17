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


namespace RoyalMartApp
{

    public partial class frmManagement : Form
    {
        int currentItemID = 1;
        int firstItemID = 0;
        int lastItemID = 0;
        int? previousItemID;
        int? nextItemID;

        public frmManagement()
        {
            InitializeComponent();
            //BindGridView();
            LoadData();
            toolStripStatusLabel1.Text = "You are at Maintenance Form";
        }


        public void LoadData()
        {
            try
            {
                string[] sqlStatements = new string[]
                {
                $@"select ROW_NUMBER() OVER(ORDER BY order_detailsID) as 'Sr. No.',item_name as 'Item',unit_price as 'Price',
                quantity as 'Quantity',discount_peritem as 'Discount per Item',subtotal as 'Subtotal',
                 tax as 'Tax',totalcost as 'Total' from order_details o inner join order_master m
                 on o.invoice_id=m.invoice_id where o.invoice_id={currentItemID}",

                    $@"
                   SELECT 
                (
                    SELECT TOP(1) o.invoice_id as FirstItemId FROM order_details o inner join order_master m
                on o.invoice_id=m.invoice_id 
                
                ) as FirstItemId,
                q.NextItemId,
                q.PreviousItemId,
                (
                    SELECT TOP(1) o.invoice_id as LastCourseId FROM order_details o inner join order_master m
                on o.invoice_id=m.invoice_id ORDER BY order_detailsID Desc
                ) as LastItemId,
                q.RowNumber,
                q.order_detailsID,invoice_id,q.username,q.finalcost
                FROM
                (
                    SELECT order_detailsID,o.invoice_id,m.username,finalcost,
                	LEAD(o.invoice_id) OVER(ORDER BY o.invoice_id) AS NextItemId,
                	LAG(o.invoice_id) OVER(ORDER BY o.invoice_id) AS PreviousItemId,
                    ROW_NUMBER() OVER(ORDER BY o.invoice_id) AS 'RowNumber'
                    FROM order_details o inner join order_master m
                    on o.invoice_id=m.invoice_id 
                ) AS q where q.invoice_id={currentItemID}
                ORDER BY q.order_detailsID
                "
                };

                DataSet ds = DataAccess.GetData(sqlStatements);

                if (ds.Tables[0].Rows.Count == 0)
                {

                    MessageBox.Show("Data was deleted");
                    return;
                }

                dataGridView1.DataSource = ds.Tables[0];
                DataRow selectedItem = ds.Tables[0].Rows[0];

                int row = ds.Tables[1].Rows.Count;
                DataRow metaData = ds.Tables[1].Rows[row - 1];
                DataRow metaData2 = ds.Tables[1].Rows[0];

                firstItemID = Convert.ToInt32(metaData["firstItemId"]);
                previousItemID = metaData2["previousItemId"] != DBNull.Value ? Convert.ToInt32(metaData2["previousItemId"]) : (int?)null;
                nextItemID = metaData["nextItemId"] != DBNull.Value ? Convert.ToInt32(metaData["nextItemId"]) : (int?)null;
                lastItemID = Convert.ToInt32(metaData["lastItemId"]);
                int currentRowNumber = Convert.ToInt32(metaData["RowNumber"]);
                lblname.Text = metaData["username"].ToString();
                lbltotal.Text = metaData["finalcost"].ToString();
                NavigationButtonManagement();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }

        private void Navigation_Handler(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            switch (btn.Name)
            {
                case "btnFirst":
                    currentItemID = firstItemID;
                    LoadData();
                    toolStripStatusLabel1.Text = "You are at First Order!";
                    break;

                case "btnNext":
                    currentItemID = nextItemID.Value;
                    toolStripStatusLabel1.Text = "You are watching Orders";
                    LoadData();
                    break;

                case "btnPrevious":
                    currentItemID = previousItemID.Value;
                    LoadData();
                    toolStripStatusLabel1.Text = "You are watching Orders";
                    break;

                case "btnLast":
                    currentItemID = lastItemID;
                    LoadData();
                    toolStripStatusLabel1.Text = "You are at Last Order!";
                    break;

                case "btnDelete":
                    Deletedata();
                    toolStripStatusLabel1.Text = "Data Deleted!";
                    toolStripProgressBar1.Value = 100;
                    break;

                case "btnCancel":
                    MdiParent frmm = new MdiParent();
                    this.Hide();
                    frmm.ShowDialog();
                    break;
            }
        }

        public void Deletedata()
        {
            string[] sqlStatements = new string[]
            {
                 $@"delete from order_details where invoice_id={currentItemID}",
                 $@"delete from  order_master where invoice_id={currentItemID}"
            };
            int i = DataAccess.ExecuteNonQuery(sqlStatements[0]);
            i += DataAccess.ExecuteNonQuery(sqlStatements[1]);
            if (i > 0)
            {
                MessageBox.Show("Data deleted successfully");
            }
            currentItemID = 1;
            LoadData();

        }
        private void NavigationButtonManagement()
        {
            btnPrevious.Enabled = previousItemID != null;
            btnNext.Enabled = nextItemID != null;
        }
    }
}
