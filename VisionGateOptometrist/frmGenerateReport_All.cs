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
    public partial class frmGenerateReport_All : Form
    {
        public frmGenerateReport_All()
        {
            InitializeComponent();
        }

        private void frmGenerateReport_All_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            //crystalReportViewer1.ReportSource = @"";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = @"C:\Users\Chaniru\source\repos\VisionGateOptometrist\VisionGateOptometrist\CrystalReport1.rpt";
        }

        private void label2_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = @"C:\Users\Chaniru\source\repos\VisionGateOptometrist\VisionGateOptometrist\CrystalReport2.rpt";
        }
    }
}
