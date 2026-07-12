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
    public partial class frmBranchAssistantDashBoard : Form
    {

        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmBranchAssistantDashBoard()
        {
            InitializeComponent();
        }

        private void frmBranchAssistantDashBoard_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load Employee Name and Notifications
            LoadEmployeeName();
            LoadNotifications();
        }

        private void LoadEmployeeName()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Query to fetch the name of the employee with EmployeeID = 5
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 5";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        var result = cmd.ExecuteScalar(); // Execute the query and get a single result
                        if (result != null)
                        {
                            lblName.Text = result.ToString(); // Set the fetched name to lblName
                        }
                        else
                        {
                            lblName.Text = "Name Not Found"; // Set default text if no data is returned
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
                    // Query to fetch all notifications
                    string query = "SELECT Title, Description FROM tblManageNotification";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable notificationTable = new DataTable();
                            adapter.Fill(notificationTable); // Fill the DataTable with query results
                            dataGridView1.DataSource = notificationTable; // Set the data source for DataGridView

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
            // Customizing the appearance of the DataGridView
            dataGridView1.RowHeadersVisible = false; // Hide row headers
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Adjust column width
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.RowTemplate.Height = 50; // Set row height
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5"); // Set background color
        }

        private void pictureBox2_Click(object sender, EventArgs e)
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
            frmViewCalender go = new frmViewCalender(this);
            this.Hide();
            go.Show();
        }

        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this);
            this.Hide();
            calendarForm.Show();
        }

        private void richTextBox8_Click(object sender, EventArgs e)
        {
            frmViewCalender go = new frmViewCalender(this);
            this.Hide();
            go.Show();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            frmViewCalender go = new frmViewCalender(this);
            this.Hide();
            go.Show();
        }

        private void picEmployee_Click(object sender, EventArgs e)
        {
            frmViewInvoice go = new frmViewInvoice();
            this.Hide();
            go.Show();
        }

        private void picProducts_Click(object sender, EventArgs e)
        {
            frmViewChannelling go = new frmViewChannelling();
            this.Hide();
            go.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmViewReturnedItems go = new frmViewReturnedItems();
            this.Hide();
            go.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmViewVisionTestResult go = new frmViewVisionTestResult();
            this.Hide();
            go.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmViewHearingMaintenanceDetails go = new frmViewHearingMaintenanceDetails();
            this.Hide();
            go.Show();

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            frmViewDeliveryInformation go = new frmViewDeliveryInformation();
            this.Hide();
            go.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            frmViewHearingTestResults go = new frmViewHearingTestResults();
            this.Hide();
            go.Show();
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            frmViewEyeGuidance go = new frmViewEyeGuidance();
            this.Hide();
            go.Show();
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            frmViewTretmentSchedule go = new frmViewTretmentSchedule();
            this.Hide();
            go.Show(); 
        }

        private void picRevunue_Click(object sender, EventArgs e)
        {
            frmViewEarGuidance go = new frmViewEarGuidance();
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
            frmLoginPage frmLoginPage = new frmLoginPage();
            this.Hide();
            frmLoginPage.Show();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }
    }
}
