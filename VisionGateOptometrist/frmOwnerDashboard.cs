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
    public partial class frmOwnerDashboard : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        public frmOwnerDashboard()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmOwnerDashboard_Load(object sender, EventArgs e)
        {

            //pnlMainFrame.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;


            LoadEmployeeName();
            LoadNotifications();
            LoadLatestRevenue();
            lblRevenue.TextAlign = ContentAlignment.MiddleCenter;

            UpdateDaysToNearestEvent();
            pnlFeedbackType.Visible = false;

        }

        private void LoadEmployeeName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 2";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lblName.Text = result.ToString();
                        }
                        else
                        {
                            lblName.Text = "Name Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadNotifications()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Title, Description FROM tblManageNotification";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable notificationTable = new DataTable();
                            adapter.Fill(notificationTable);

                            dataGridView1.DataSource = notificationTable;

                            CustomizeNotificationGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeNotificationGrid()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void LoadLatestRevenue()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT TOP 1 Amount FROM tblManageRevenue ORDER BY RevenueID DESC";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && decimal.TryParse(result.ToString(), out decimal revenue))
                        {
                            lblRevenue.Text = $"Rs. {revenue:N0}"; // Format as currency with commas
                        }
                        else
                        {
                            lblRevenue.Text = "0"; // Display 0 if no revenue data is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblRevenue.Text = "0"; // Default to 0 in case of an error
            }
        }




        private void UpdateDaysToNearestEvent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Query to get the nearest upcoming charity event
                    string query = @"
                SELECT TOP 1 DATEDIFF(DAY, GETDATE(), [Date]) AS DaysToEvent
                FROM tblManageCharitySchedule
                WHERE [Date] > GETDATE()
                ORDER BY [Date] ASC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int days))
                        {
                            lblDays.Text = $"Charity event in : {days} Days"; // Update label with the number of days
                        }
                        else
                        {
                            lblDays.Text = "No upcoming events."; // Handle case when there are no future events
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error calculating days to nearest event: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmAddEmployee go = new frmAddEmployee();
            go.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmViewEmployee viewEmployeeForm = new frmViewEmployee(this); // Pass the current form (Owner Dashboard)
            this.Hide(); // Hide the current form
            viewEmployeeForm.Show(); // Show the View Employee form
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            frmManageEmployeePayment go = new frmManageEmployeePayment();
            this.Hide();
            go.Show();
        }

        private void richTextBox6_Click(object sender, EventArgs e)
        {
            frmManageEmployeePayment go = new frmManageEmployeePayment();
            this.Hide();
            go.Show();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            frmManageEmployeePayment go = new frmManageEmployeePayment();
            this.Hide();
            go.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmFinalReport go = new frmFinalReport();
            this.Hide();
            go.Show();
        }

        private void richTextBox7_Click(object sender, EventArgs e)
        {
            frmFinalReport go = new frmFinalReport();
            this.Hide();
            go.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            frmFinalReport go = new frmFinalReport();
            this.Hide();
            go.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmViewPromotions go = new frmViewPromotions();
            this.Hide();
            go.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            frmViewCharitySchedule go = new frmViewCharitySchedule();
            this.Hide();
            go.Show();
        }

        private void richTextBox9_Click(object sender, EventArgs e)
        {
            frmViewPromotions go = new frmViewPromotions();
            this.Hide();
            go.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            frmViewPromotions go = new frmViewPromotions();
            this.Hide();
            go.Show();
        }

        private void richTextBox8_Click(object sender, EventArgs e)
        {
            frmViewCharitySchedule go = new frmViewCharitySchedule();
            this.Hide();
            go.Show();
        }

        private void lblDays_Click(object sender, EventArgs e)
        {
            frmViewCharitySchedule go = new frmViewCharitySchedule();
            this.Hide();
            go.Show();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
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

        private void frmOwnerDashboard_Click(object sender, EventArgs e)
        {
            pnlFeedbackType.Visible = false;
        }

        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this);
            this.Hide();
            calendarForm.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmViewBranchTasks go = new frmViewBranchTasks();
            this.Hide();
            go.Show();
        }

        private void picMore_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One go = new frmMenuPage_All_in_One();
            this.Hide();
            go.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmLoginPage go = new frmLoginPage();
            this.Hide();
            go.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmProductCatelog go = new frmProductCatelog();
            this.Hide();
            go.Show();
        }
    }
}
