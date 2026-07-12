using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmPatientInquiries : Form
    {
        public frmPatientInquiries()
        {
            InitializeComponent();

            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmPatientInquiries_Load(object sender, EventArgs e)
        {
            pnlPatientInquiry.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One goBack = new frmMenuPage_All_in_One();
            goBack.Show();
            this.Hide();
        }
    }
}
