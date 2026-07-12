using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmCashierDashBoard : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmCashierDashBoard()
        {
            InitializeComponent();
        }

        private void frmCashierDashBoard_Load(object sender, EventArgs e)
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
                    // Query to fetch the name of the employee with EmployeeID = 9
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = 9";
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

        // Existing Event Handlers
        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this);
            this.Hide();
            calendarForm.Show();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmViewChannelling go = new frmViewChannelling();
            this.Hide();
            go.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            frmViewPatientPayment go = new frmViewPatientPayment();
            this.Hide();
            go.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            frmViewOrders go = new frmViewOrders();
            this.Hide();
            go.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            frmViewSupplierPayment frmViewSupplierPayment = new frmViewSupplierPayment();
            this.Hide();
            frmViewSupplierPayment.Show();
        }
    }
}
