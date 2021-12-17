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
    public partial class MdiParent : Form
    {
        double tax = 0;
        int SrNo = 0;
        int flag = 0;

        public MdiParent()
        {
            InitializeComponent();
            getInvoiceId();
            textBoxUser.Text = Login.username;
            toolStripStatusLabel1.Text = "Welcome! Select an Item";
            GetItems();
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "SR No";
            dataGridView1.Columns[1].Name = "Item Name";
            dataGridView1.Columns[2].Name = "Unit Price";
            dataGridView1.Columns[3].Name = "Discount Per Item";
            dataGridView1.Columns[4].Name = "Quantity";
            dataGridView1.Columns[5].Name = "Subtotal";
            dataGridView1.Columns[6].Name = "Tax";
            dataGridView1.Columns[7].Name = "Total Cost";
            flag = 1;
        }


        public void GetItems()
        {
            try
            {
                string sql = "select * from items_table";
                DataTable dt = DataAccess.GetData(sql);
                if (dt != null)
                {
                    comboBoxItems.DataSource = dt;
                    comboBoxItems.DisplayMember = "item_Name";
                    comboBoxItems.ValueMember = "item_Name";

                }
                //using (SqlConnection conn = new SqlConnection(connString))
                //{
                //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                //    {
                //        conn.Open();
                //        SqlDataReader dr = cmd.ExecuteReader();

                //        while (dr.Read())
                //        {
                //            string item_name = dr.GetString(1);
                //            comboBoxItems.Items.Add(item_name);
                //        }
                //        comboBoxItems.Sorted = true;
                //        conn.Close();
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        public void GetPrice()
        {

            try
            {
                if (comboBoxItems.SelectedItem == null)
                {

                }
                else
                {
                    double price = 0;
                    DataTable data = new DataTable();
                    string sql = $@"select item_price from items_table where item_name = '{comboBoxItems.SelectedValue}'";
                    price = Convert.ToDouble(DataAccess.GetValue(sql));

                    textBoxUnitPrice.Text = price.ToString();
                    //using (SqlConnection conn = new SqlConnection(connString))
                    //{
                    //    using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                    //    {
                    //        sda.SelectCommand.Parameters.AddWithValue("@item_name", comboBoxItems.SelectedItem.ToString());
                    //        

                    //        sda.Fill(data);

                    //        
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }


        public void GetDiscount()
        {
            try
            {

                if (comboBoxItems.SelectedItem == null)
                {

                }
                else
                {
                    double discount = 0;
                    string sql = $@"select item_discount from items_table where item_name = '{comboBoxItems.SelectedValue}'";
                    discount = Convert.ToDouble(DataAccess.GetValue(sql));
                    textBoxDiscount.Text = discount.ToString();

                    //using (SqlConnection conn = new SqlConnection(connString))
                    //{
                    //    using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                    //    {
                    //        sda.SelectCommand.Parameters.AddWithValue("@item_name", comboBoxItems.SelectedItem.ToString());
                    //        DataTable data = new DataTable();

                    //        sda.Fill(data);

                    //        if (data.Rows.Count > 0)
                    //        {
                    //            discount = Convert.ToDouble(data.Rows[0]["item_discount"]);
                    //        }
                    //        
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void comboBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                GetPrice();
                GetDiscount();
                textBoxQuantity.Enabled = true;

                toolStripStatusLabel1.Text = "Your Item Is Selected. Enter Quantity !";
            }
        }

        private void textBoxQuantity_TextChanged(object sender, EventArgs e)
        {
            double subtotal = 0;
            try
            {
                if (string.IsNullOrEmpty(textBoxQuantity.Text))
                {

                }
                else
                {
                    double price = Convert.ToDouble(textBoxUnitPrice.Text);
                    double discount = Convert.ToDouble(textBoxDiscount.Text);
                    double quantity = Convert.ToDouble(textBoxQuantity.Text);

                    subtotal = price * quantity;
                    subtotal = subtotal - (discount * quantity);
                    textBoxSubTotal.Text = subtotal.ToString();

                    toolStripStatusLabel1.Text = "Quantity is Entered! Nice. Now about to Add the item";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

        }


        private void textBoxSubTotal_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSubTotal.Text) == true)
            {
            }
            else
            {
                double subtotal = Convert.ToDouble(textBoxSubTotal.Text);

                //business rule
                if (subtotal >= 150)
                {
                    tax = subtotal * 0.20;
                    textBoxTax.Text = tax.ToString();
                }
                else if (subtotal >= 100)
                {
                    tax = subtotal * 0.18;
                    textBoxTax.Text = tax.ToString();
                }
                else if (subtotal >= 50)
                {
                    tax = subtotal * 0.15;
                    textBoxTax.Text = tax.ToString();
                }
                else if (subtotal >= 25)
                {
                    tax = subtotal * 0.10;
                    textBoxTax.Text = tax.ToString();
                }
                else if (subtotal >= 5)
                {
                    tax = subtotal * 0.05;
                    textBoxTax.Text = tax.ToString();
                }
                else
                {
                    textBoxTotalCost.Text = textBoxSubTotal.Text;
                }
            }
        }

        private void textBoxTax_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxTax.Text))
            {
            }
            else
            {
                double subtotal = Convert.ToDouble(textBoxSubTotal.Text);

                double tax = Convert.ToDouble(textBoxTax.Text);
                double totalCost = subtotal + tax;
                textBoxTotalCost.Text = totalCost.ToString();
            }
        }

        private void textBoxAmountPaid_TextChanged(object sender, EventArgs e)
        {
            double change = 0;
            double fCost;
            double amountPaid;
            try
            {
                if (double.TryParse(textBoxAmountPaid.Text, out amountPaid))
                {
                    fCost = Convert.ToDouble(textBoxFinalCost.Text);
                    amountPaid = Convert.ToDouble(textBoxAmountPaid.Text);
                    change = amountPaid - fCost;
                    textBoxChange.Text = change.ToString();
                }
                else
                {
                    MessageBox.Show("Please Enter Amount-Paid");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        void AddDataToGridView(string Sr_No, string item_name, string unit_price, string discount, string quantity, string sub_total, string tax, string total_cost)
        {
            string[] row = { Sr_No, item_name, unit_price, discount, quantity, sub_total, tax, total_cost };
            dataGridView1.Rows.Add(row);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //pre-increment
            if (comboBoxItems.SelectedItem != null)
            {

                AddDataToGridView((++SrNo).ToString(),
                                   comboBoxItems.SelectedValue.ToString(),
                                   textBoxUnitPrice.Text,
                                   textBoxDiscount.Text,
                                   textBoxQuantity.Text,
                                   textBoxSubTotal.Text,
                                   textBoxTax.Text,
                                   textBoxTotalCost.Text);
                resetControls();
                CalculateFinalCost();

                toolStripStatusLabel1.Text = "You Added the item!. You can see it Above in the Box!";
            }
            else
            {
                MessageBox.Show("Please Select an Item!");
            }
        }


        void resetControls()
        {
            comboBoxItems.SelectedItem = null;
            textBoxUnitPrice.Clear();
            textBoxDiscount.Clear();
            textBoxQuantity.Clear();
            textBoxTax.Clear();
            textBoxSubTotal.Clear();
            textBoxTotalCost.Clear();
            textBoxFinalCost.Clear();
            textBoxAmountPaid.Clear();
            textBoxChange.Clear();
            textBoxQuantity.Enabled = false;
        }

        void CalculateFinalCost()
        {
            double finalCost = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                finalCost = finalCost + Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value);
            }
            textBoxFinalCost.Text = finalCost.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            resetControls();
        }

        //seperate button because is below line code place in resetControls, we cand add the data
        private void ClearGridview_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            SrNo = 0;
        }

        void getInvoiceId()
        {
            try
            {
                string sql = "select invoice_id from order_master";
                DataTable dt = DataAccess.GetData(sql);
                if (dt.Rows.Count < 1)
                {
                    textBoxInvoice.Text = "1";
                }
                else
                {
                    string sql2 = "select max(invoice_id) from order_master";
                    int a = Convert.ToInt32(DataAccess.GetValue(sql2));
                    a++;
                    textBoxInvoice.Text = a.ToString();
                }
                //using (SqlConnection conn = new SqlConnection(connString))
                //{
                //    using (SqlDataAdapter sda = new SqlDataAdapter(sql, conn))
                //    {
                //        DataTable data = new DataTable();
                //        sda.Fill(data);

                //        if (data.Rows.Count < 1)
                //        {
                //            textBoxInvoice.Text = "1";
                //        }
                //        else
                //        {
                //            string sql2 = "select max(invoice_id) from order_master";

                //            using (SqlCommand cmd = new SqlCommand(sql2, conn))
                //            {
                //                conn.Open();
                //                int a = Convert.ToInt32(cmd.ExecuteScalar());
                //                a++;
                //                textBoxInvoice.Text = a.ToString();

                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }


        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string sql = "insert into order_master values('" + textBoxInvoice.Text + "','" + textBoxUser.Text + "', '" + DateTime.Now.ToString() + "', '" + textBoxFinalCost.Text + "')";
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        cmd.Parameters.AddWithValue("@id", textBoxInvoice.Text);
            //        cmd.Parameters.AddWithValue("@user", textBoxUser.Text);
            //        cmd.Parameters.AddWithValue("@datetime", DateTime.Now.ToString());
            //        cmd.Parameters.AddWithValue("@finalcost", textBoxFinalCost.Text);

            //        conn.Open();

            int a = DataAccess.ExecuteNonQuery(sql);
            if (a > 0)
            {
                MessageBox.Show("Inserted Successfully !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getInvoiceId();
                resetControls();
            }
            else
            {
                MessageBox.Show("Inserted Failed !", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //onn.Close();

            InsertIntoOrderDetails();
            //}
            //}
        }

        private void textBoxQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch))
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Can not Order more than 10");
            }
        }

        private void textBoxAmountPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (char.IsDigit(ch))
            {
                e.Handled = false;
            }
            else if (ch == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.logo2;
            Image img = bmp;
            e.Graphics.DrawImage(img, 30, 5, 800, 250);
            e.Graphics.DrawString($"Invoice ID: {textBoxInvoice.Text}", new Font("Arial", 15), Brushes.Black, new Point(30, 300));
            e.Graphics.DrawString($"User Name: {textBoxUser.Text}", new Font("Arial", 15), Brushes.Black, new Point(30, 330));
            e.Graphics.DrawString($"Date: {DateTime.Now.ToShortDateString()}", new Font("Arial", 15), Brushes.Black, new Point(30, 360));
            e.Graphics.DrawString($"Time: {DateTime.Now.ToShortTimeString()}", new Font("Arial", 15), Brushes.Black, new Point(30, 390));
            e.Graphics.DrawString($"-------------------------------------------------------------------------------------------------------------", new Font("Arial", 15), Brushes.Black, new Point(30, 420));
            e.Graphics.DrawString($"Item", new Font("Arial", 15), Brushes.Black, new Point(30, 450));
            e.Graphics.DrawString($"Price", new Font("Arial", 15), Brushes.Black, new Point(330, 450));
            e.Graphics.DrawString($"Quantity", new Font("Arial", 15), Brushes.Black, new Point(460, 450));
            e.Graphics.DrawString($"Discount", new Font("Arial", 15), Brushes.Black, new Point(650, 450));
            e.Graphics.DrawString($"-------------------------------------------------------------------------------------------------------------", new Font("Arial", 15), Brushes.Black, new Point(30, 490));


            //for printing itemName
            int space = 510;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "", new Font("Arial", 15), Brushes.Black, new Point(30, space));
                            space = space + 30;
                        }
                        catch { }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            //for printing UnitPrice
            int space1 = 510;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[2].Value?.ToString() ?? "", new Font("Arial", 15), Brushes.Black, new Point(340, space1));
                            space1 = space1 + 30;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            //for printing Quantity
            int space2 = 510;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[3].Value?.ToString() ?? "", new Font("Arial", 15), Brushes.Black, new Point(685, space2));
                            space2 = space2 + 30;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            //for printing Discount
            int space3 = 510;
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        try
                        {
                            //i wrote .Value?.ToString() ?? "" for checking null reference in .toString(). found this from internet. called type-casting. because if you .toString() in dataGridView, toString() may contain null reference.
                            e.Graphics.DrawString(dataGridView1.Rows[i].Cells[4].Value?.ToString() ?? "", new Font("Arial", 15), Brushes.Black, new Point(490, space3));
                            space3 = space3 + 30;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, ex.GetType().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }

            //printing SubTotal
            double subtotalPrint = 0;
            double taxPrint = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    subtotalPrint = subtotalPrint + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value);
                    taxPrint = taxPrint + Convert.ToDouble(dataGridView1.Rows[i].Cells[6].Value);
                }
                try
                {
                    e.Graphics.DrawString($"Sub-Total: {subtotalPrint.ToString()}", new Font("Arial", 15), Brushes.Black, new Point(30, 850));
                    e.Graphics.DrawString($"Tax: {taxPrint.ToString()}", new Font("Arial", 15), Brushes.Black, new Point(30, 880));
                    e.Graphics.DrawString($"Final Cost: {textBoxFinalCost.Text}", new Font("Arial", 15), Brushes.Black, new Point(30, 910));
                    e.Graphics.DrawString($"Amount Paid: {textBoxAmountPaid.Text}", new Font("Arial", 15), Brushes.Black, new Point(30, 940));
                    e.Graphics.DrawString($"Change {textBoxChange.Text}", new Font("Arial", 15), Brushes.Black, new Point(30, 970));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }


        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItemForm adf = new AddItemForm();
            adf.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //activated event, used for getting things while the page is active
        private void Form1_Activated(object sender, EventArgs e)
        {
            //comboBoxItems.DataSource = null;
            //comboBoxItems.Items.Clear();
            flag = 0;
            GetItems();
            flag = 1;

        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManagement edf = new frmManagement();
            edf.ShowDialog(); //if i use ShowDialog, i can not edit or go in the back form. i have to close the current form first. 
        }

        private void viewDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewDataForm vdf = new ViewDataForm();
            vdf.ShowDialog();
        }


        int getLastInsertedInvoiceID() // int because it returns MAX value
        {
            string sql = "select max(invoice_id) from order_master";
            int MaxInvoiceID = Convert.ToInt32(DataAccess.GetValue(sql));
            return MaxInvoiceID;

            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            //    using (SqlCommand cmd = new SqlCommand(sql, conn))
            //    {
            //        conn.Open();

            //        int MaxInvoiceID = Convert.ToInt32(cmd.ExecuteScalar());

            //        conn.Close();
            //        return MaxInvoiceID;
            //    }
            //}
        }

        void InsertIntoOrderDetails()
        {
            //using (SqlConnection conn = new SqlConnection(connString))
            //{
            int a = 0;
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string sql = $@"insert into order_details values ({getLastInsertedInvoiceID()}, 
                        {dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? ""}, {dataGridView1.Rows[i].Cells[2].Value},
                        {dataGridView1.Rows[i].Cells[3].Value}, {dataGridView1.Rows[i].Cells[4].Value},
                        {dataGridView1.Rows[i].Cells[5].Value}, {dataGridView1.Rows[i].Cells[6].Value},
                        {dataGridView1.Rows[i].Cells[7].Value})";

                    a += DataAccess.ExecuteNonQuery(sql);
                    //using (SqlCommand cmd = new SqlCommand(sql, conn))
                    //{
                    //    cmd.Parameters.AddWithValue("invoice_id", getLastInsertedInvoiceID());
                    //    cmd.Parameters.AddWithValue("name", dataGridView1.Rows[i].Cells[1].Value?.ToString() ?? "");
                    //    cmd.Parameters.AddWithValue("price", dataGridView1.Rows[i].Cells[2].Value);
                    //    cmd.Parameters.AddWithValue("discount", dataGridView1.Rows[i].Cells[3].Value);
                    //    cmd.Parameters.AddWithValue("quantity", dataGridView1.Rows[i].Cells[4].Value);
                    //    cmd.Parameters.AddWithValue("subtotal", dataGridView1.Rows[i].Cells[5].Value);
                    //    cmd.Parameters.AddWithValue("tax", dataGridView1.Rows[i].Cells[6].Value);
                    //    cmd.Parameters.AddWithValue("finalcost", dataGridView1.Rows[i].Cells[7].Value);

                    //    conn.Open();
                    //    a = a + cmd.ExecuteNonQuery();
                    //    conn.Close();
                    //}

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
            // }
        }

        private void searchDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchByInvoiceID das = new SearchByInvoiceID();
            das.Show();
        }

        private void browseByNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchByName sbn = new SearchByName();
            sbn.Show();
        }

        private void aboutMeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.Show();
        }

        private void textBoxQuantity_Validating(object sender, CancelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                int i;
                if (int.TryParse(tb.Text, out i))
                {
                    if (i >= 1 && i <= 10)
                        return;
                }
            }
            MessageBox.Show("You can only order maximum 10 for this item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            e.Cancel = true;
        }
    }
}
