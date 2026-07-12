using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmOwnerAssistantDashboard : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmOwnerAssistantDashboard()
        {
            InitializeComponent();
        }

        private void frmOwnerAssistantDashboard_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load the name of the employee with ID = 3
            LoadEmployeeName();

            // Load notifications into DataGridView
            LoadNotifications();
        }

        private void LoadEmployeeName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 3";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lblName.Text = result.ToString(); // Update lblName with the employee name
                        }
                        else
                        {
                            lblName.Text = "Employee Name Not Found";
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

                            dataGridView1.DataSource = notificationTable; // Bind data to DataGridView

                            // Customize the appearance of the DataGridView
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

        private void frmOwnerAssistantDashboard_Click(object sender, EventArgs e)
        {
            frmSendNotification go = new frmSendNotification();
            this.Hide();
            go.Show();
        }

        private void richTextBox6_Click(object sender, EventArgs e)
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frmViewBranchTasks go = new frmViewBranchTasks();
            this.Hide();
            go.Show();
        }

        private void richTextBox7_Click(object sender, EventArgs e)
        {
            frmViewBranchTasks go = new frmViewBranchTasks();
            this.Hide();
            go.Show();
        }

        private void label18_Click(object sender, EventArgs e)
        {
            frmViewBranchTasks go = new frmViewBranchTasks();
            this.Hide();
            go.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            frmManageSystemFeedback go = new frmManageSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void richTextBox9_Click(object sender, EventArgs e)
        {
            frmManageSystemFeedback go = new frmManageSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void label19_Click(object sender, EventArgs e)
        {
            frmManageSystemFeedback go = new frmManageSystemFeedback();
            this.Hide();
            go.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Don't remove. Keep this event as it is...
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            fmViewStock go = new fmViewStock();
            this.Hide();
            go.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmLoginPage frmLoginPage = new frmLoginPage();
            this.Hide();
            frmLoginPage.Show();
        }
    }
}
