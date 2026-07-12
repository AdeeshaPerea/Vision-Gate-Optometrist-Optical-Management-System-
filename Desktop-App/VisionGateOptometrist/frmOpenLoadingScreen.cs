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
    public partial class frmOpenLoadingScreen : Form
    {
        public frmOpenLoadingScreen()
        {
            InitializeComponent();
        }

        private void frmOpenLoadingScreen_Load(object sender, EventArgs e)
        {
            Timer_OpenPageLoadingScreen.Start();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void Timer_OpenPageLoadingScreen_Tick(object sender, EventArgs e)
        {
            if(ProgressBar_OpenPageLoading.Value < 100)
            {
                ProgressBar_OpenPageLoading.Value += 1;

                lblPresentage_Loading.Text  = ProgressBar_OpenPageLoading.Value.ToString() + "%";
                
            }
            else
            {
                Timer_OpenPageLoadingScreen.Stop();

                // Open the frmLogin form when progress reaches 100%
                frmOpenPage goLogin = new frmOpenPage();
                goLogin.Show();

                this.Hide();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }


}
