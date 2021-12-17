using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace splashScreen
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);

            label2.Text = progressBar1.Value.ToString() + "%";
            lblVersion.Text = Application.ProductVersion;

            if (progressBar1.Value == 100)
            {
                timer1.Stop();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {

        }
    }
}
