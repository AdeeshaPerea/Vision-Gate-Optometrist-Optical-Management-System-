using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmOpenPage : Form
    {
        // Button designs using rounded edges
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeft,
            int nTop,
            int nRight,
            int nBottom,
            int nWidthEllipse,
            int nHeightEllipse
        );

        public frmOpenPage()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(frmOpenPage_Paint);
        }

        // New method for painting the form with a gradient background
        private void frmOpenPage_Paint(object sender, PaintEventArgs e)
        {
            // Create a LinearGradientBrush with lighter colors
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                ColorTranslator.FromHtml("#D6E4F1"), // Lighter starting color
                ColorTranslator.FromHtml("#BFD7FF"), // Lighter ending color
                LinearGradientMode.Vertical)) // Gradient direction
            {
                // Fill the form background with the gradient
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void frmOpenPage_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None; // Remove title bar and borders
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            // Close the current form
            this.Close();
        }

        private void btnOwner_Click(object sender, EventArgs e)
        {
            // Create an instance of frmLoginPage
            frmLoginPage loginForm = new frmLoginPage();

            // Pass the text to be displayed in the lblDescription
            // Pass the text with a line break
            loginForm.DescriptionText = "Your optical store is in good hands,\nlet's ensure everything's running smoothly!";


            // Show the login form
            loginForm.Show();

            // Hide the current form
            this.Hide();
        }

        // Don't remove unused events
        private void label2_Click(object sender, EventArgs e)
        {
            // Don't remove
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Don't remove
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Don't remove
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Don't remove
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            // Create an instance of the Login Page
            frmLoginPage loginPage = new frmLoginPage();

            // Pass parameter to make admin-specific fields visible
            loginPage.ShowAdminTokenFields = true;
            loginPage.DescriptionText = "Ready to monitor and maintain the system?"; // Set the description text
            loginPage.HideForgotPasswordLabel = true; // Hide the forgot password label for admin

            // Show the Login Page
            loginPage.Show();

            // Hide the current form
            this.Hide();
        }

        private void btnOther_Click(object sender, EventArgs e)
        {
            // Create an instance of frmLoginPage
            frmLoginPage loginForm = new frmLoginPage();

            // Pass the text to be displayed in the lblDescription
            loginForm.DescriptionText = "Your expertise is needed today.\nLet’s ensure everything runs smoothly!";

            // Show the login form
            loginForm.Show();

            // Hide the current form
            this.Hide();
        }
    }
}
