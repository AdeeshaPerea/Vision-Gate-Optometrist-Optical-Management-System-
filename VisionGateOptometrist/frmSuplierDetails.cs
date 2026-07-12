using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmSuplierDetails : Form
    {
        //btn Designs--Check this
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
            (
            int nLeft,
            int nTop,
            int nRight,
            int nButtom,
            int nWidthEllipse,
            int nHeightEllipse
            );

        public frmSuplierDetails()
        {
            InitializeComponent();

            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmSuplierDetails_Load(object sender, EventArgs e)
        {
            pnlSupplierDetails.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;


        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One goBack = new frmMenuPage_All_in_One();
            goBack.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void picBoxSupplierDetails_Click(object sender, EventArgs e)
        {

        }

        private void txtEmployeeContactNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void picProducts_Click(object sender, EventArgs e)
        {

        }

        private void picEmployee_Click(object sender, EventArgs e)
        {

        }

        private void picFeedback_Click(object sender, EventArgs e)
        {

        }

        private void picSetings_Click(object sender, EventArgs e)
        {

        }
    }
}
