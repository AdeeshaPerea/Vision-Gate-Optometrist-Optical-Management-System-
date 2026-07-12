using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmAdminDashboard : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        public frmAdminDashboard()
        {
            InitializeComponent();
        }

        private void frmAdminDashboard_Load(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = false;
            this.FormBorderStyle = FormBorderStyle.None;
            // Load admin name
            LoadAdminName();

            // Load notifications into the DataGridView
            LoadNotifications();

            // Set border styles
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            // Adjust column widths
            if (dataGridView1.Columns["Title"] != null)
            {
                dataGridView1.Columns["Title"].Width = 150; // Reduce the width of the Title column
            }

            // Hide the NotificationID column
            if (dataGridView1.Columns["NotificationID"] != null)
            {
                dataGridView1.Columns["NotificationID"].Visible = false;
            }
        }

        private void LoadNotifications()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT NotificationID, Title, Description FROM tblManageNotification";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable notificationTable = new DataTable();
                            adapter.Fill(notificationTable);

                            dataGridView1.DataSource = notificationTable; // Bind data to DataGridView

                            // Customize DataGridView appearance
                            CustomizeDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Hide unnecessary elements for a clean notification look
            dataGridView1.RowHeadersVisible = false; // Remove row headers
            dataGridView1.BorderStyle = BorderStyle.None; // Remove grid border
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None; // Remove cell borders

            // Adjust column and row properties
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Columns fill the width
            dataGridView1.RowTemplate.Height = 50; // Set a taller height for better spacing

            // Remove gridlines for a cleaner look
            dataGridView1.GridColor = Color.White;

            // Customize the header to look like part of the grid
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F4F8"); // Subtle light blue
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#333333"); // Dark gray for text
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold); // Bold and professional
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Customize the cell appearance for notifications
            dataGridView1.DefaultCellStyle.BackColor = Color.White; // Clean white background
            dataGridView1.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#4A4A4A"); // Subtle dark gray for text
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12); // Clean, readable font
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True; // Enable text wrapping

            // Alternating row colors for readability
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F9FBFD"); // Light blue for alternate rows

            // Remove vertical scrolling indicators for a clean appearance
            dataGridView1.ScrollBars = ScrollBars.Vertical;

            // Only display Title and Description columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Name != "Title" && column.Name != "Description")
                {
                    column.Visible = false; // Hide all columns except Title and Description
                }
            }

            // Overall background color for the grid
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");
        }


        private void LoadAdminName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 1";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lblName.Text = result.ToString();
                        }
                        else
                        {
                            lblName.Text = "Admin Name Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading admin name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {
            //dont remove.. keep this event as it is...
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmManageUserAccounts go = new frmManageUserAccounts();
            this.Hide();
            go.Show();

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmViewUserAccounts go = new frmViewUserAccounts();
            this.Hide();
            go.Show();
        }

        private void richTextBox6_TextChanged(object sender, EventArgs e)
        {
            frmSendNotification go = new frmSendNotification();
            this.Hide();
            go.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmSendNotification go = new frmSendNotification();
            this.Hide();
            go.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            frmSendNotification go = new frmSendNotification();
            this.Hide();
            go.Show();
        }

        

        private void richTextBox7_TextChanged(object sender, EventArgs e)
        {
            frmManageUserForgotPassword go = new frmManageUserForgotPassword();
            this.Hide();
            go.Show();
        }

        private void label18_Click_1(object sender, EventArgs e)
        {
            frmManageUserForgotPassword go = new frmManageUserForgotPassword();
            this.Hide();
            go.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmManageUserForgotPassword go = new frmManageUserForgotPassword();
            this.Hide();
            go.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            frmViewSystemFeedback go = new frmViewSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void richTextBox9_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmViewSystemFeedback go = new frmViewSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void richTextBox8_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void label20_Click(object sender, EventArgs e)
        {
            frmViewPatientFeddback go = new frmViewPatientFeddback();
            this.Hide();
            go.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmViewPatientFeddback go = new frmViewPatientFeddback();
            this.Hide();
            go.Show();
        }

        private void richTextBox10_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void label23_Click(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = true; // Ensure the panel is visible
            pnlFeedbackType.Size = new Size(0, 83); // Start collapsed with height set to 100
            pnlFeedbackType.Location = new Point(pnlFeedbackType.Location.X, pnlFeedbackType.Location.Y); // Maintain current position

            Timer animationTimer = new Timer();
            animationTimer.Interval = 3; // Set a small interval for smooth animation
            animationTimer.Tick += (s, args) =>
            {
                if (pnlFeedbackType.Width < 245) // Target width
                {
                    pnlFeedbackType.Width += 17; // Increase width incrementally
                }
                else
                {
                    animationTimer.Stop(); // Stop the timer when the animation is complete
                }
            };

            animationTimer.Start(); // Start the animation

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = true; // Ensure the panel is visible
            pnlFeedbackType.Size = new Size(0, 83); // Start collapsed with height set to 100
            pnlFeedbackType.Location = new Point(pnlFeedbackType.Location.X, pnlFeedbackType.Location.Y); // Maintain current position

            Timer animationTimer = new Timer();
            animationTimer.Interval = 3; // Set a small interval for smooth animation
            animationTimer.Tick += (s, args) =>
            {
                if (pnlFeedbackType.Width < 245) // Target width
                {
                    pnlFeedbackType.Width += 17; // Increase width incrementally
                }
                else
                {
                    animationTimer.Stop(); // Stop the timer when the animation is complete
                }
            };

            animationTimer.Start(); // Start the animation
        }

        private void btnPatientFeedbackHistory_Click(object sender, EventArgs e)
        {
            frmviewPatientFeedbackHistoty go = new frmviewPatientFeedbackHistoty();
            this.Hide();
            go.Show();
        }

        private void btnSystemFeedbackHistory_Click(object sender, EventArgs e)
        {
            frmViewSystemFeedbackHistory go = new frmViewSystemFeedbackHistory();
            this.Hide();
            go.Show();
        }

        private void richTextBox10_Click(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = true; // Ensure the panel is visible
            pnlFeedbackType.Size = new Size(0, 83); // Start collapsed with height set to 100
            pnlFeedbackType.Location = new Point(pnlFeedbackType.Location.X, pnlFeedbackType.Location.Y); // Maintain current position

            Timer animationTimer = new Timer();
            animationTimer.Interval = 3; // Set a small interval for smooth animation
            animationTimer.Tick += (s, args) =>
            {
                if (pnlFeedbackType.Width < 245) // Target width
                {
                    pnlFeedbackType.Width += 17; // Increase width incrementally
                }
                else
                {
                    animationTimer.Stop(); // Stop the timer when the animation is complete
                }
            };

            animationTimer.Start(); // Start the animation
        }

        private void richTextBox9_Click(object sender, EventArgs e)
        {
            frmViewSystemFeedback go = new frmViewSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void richTextBox8_Click(object sender, EventArgs e)
        {
            frmViewPatientFeddback go = new frmViewPatientFeddback();
            this.Hide();
            go.Show();
        }

        private void frmAdminDashboard_Click(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = false;
        }

        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this);
            this.Hide();
            calendarForm.Show();
        }

        private void picMore_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One go = new frmMenuPage_All_in_One();
            this.Hide();
            go.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmLoginPage frmLoginPage = new frmLoginPage();
            this.Hide();
            frmLoginPage.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmProductCatelog go = new frmProductCatelog();
            this.Hide();
            go.Show();
        }
    }
}
