using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmBranchManagerDashBoard : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmBranchManagerDashBoard()
        {
            InitializeComponent();
        }

        private void frmBranchManagerDashBoard_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load the name of the Branch Manager (EmployeeID = 4)
            LoadBranchManagerName();

            // Load notifications into DataGridView
            LoadNotifications();
        }

        private void LoadBranchManagerName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 4";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            lblName.Text = result.ToString(); // Update lblName with the manager's name
                        }
                        else
                        {
                            lblName.Text = "Branch Manager Name Not Found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branch manager name: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Existing navigation methods remain unchanged
        private void frmBranchManagerDashBoard_Click(object sender, EventArgs e)
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
            frmManageReport go = new frmManageReport();
            this.Hide();
            go.Show();
        }

        private void richTextBox8_Click(object sender, EventArgs e)
        {
            frmManageReport go = new frmManageReport();
            this.Hide();
            go.Show();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            frmManageReport go = new frmManageReport();
            this.Hide();
            go.Show();
        }

        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this);
            this.Hide();
            calendarForm.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            frmLoginPage go = new frmLoginPage();
            this.Hide();
            go.Show();
        }

        private void picEmployee_Click(object sender, EventArgs e)
        {
            frmViewHearingTestResults go = new frmViewHearingTestResults();
            this.Hide();
            go.Show();
        }

        private void picProducts_Click(object sender, EventArgs e)
        {
            frmViewEarGuidance frmViewReturnedItems = new frmViewEarGuidance();
            this.Hide();
            frmViewReturnedItems.Show();
        }

        private void picRevunue_Click(object sender, EventArgs e)
        {
            frmProductCatelog go = new frmProductCatelog();
            this.Hide();
            go.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmLoginPage frmLoginPage = new frmLoginPage();
            this.Hide();
            frmLoginPage.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            frmViewDeliveryInformation go = new frmViewDeliveryInformation();
            this.Hide();
            go.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmViewTretmentSchedule go = new frmViewTretmentSchedule();
            this.Hide();
            go.Show(); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmViewSupplierInformation go = new frmViewSupplierInformation();
            this.Hide();
            go.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            frmViewVisionTestResult go = new frmViewVisionTestResult();
            this.Hide();
            go.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            frmViewInvoice go = new frmViewInvoice();
            this.Hide();
            go.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            frmViewOrders gp = new frmViewOrders();
            this.Hide();
            gp.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            frmViewPromotions gp = new frmViewPromotions();
            this.Hide();
            gp.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            frmViewReturnedItems gp = new frmViewReturnedItems();
            this.Hide();
            gp.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            frmViewEmployeePayment go = new frmViewEmployeePayment();
            this.Hide();
            go.Show();
        }

        private void picMore_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One go = new frmMenuPage_All_in_One();
            this.Hide();
            go.Show();
        }

        private void guna2Button3_Click_1(object sender, EventArgs e)
        {
            fmViewStock go = new fmViewStock();
            this.Hide();
            go.Show();
        }
    }
}
