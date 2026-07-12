using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewSystemFeedback : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Variable to store the selected SystemBugID
        private int selectedBugID;

        public frmViewSystemFeedback()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewSystemFeedback_Load(object sender, EventArgs e)
        {
            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load system feedback data into the DataGridView
            LoadSystemFeedback();

            // Customize the DataGridView appearance
            CustomizeDataGridView();
            // Refresh the DataGridView
            LoadSystemFeedback();
        }

        private void LoadSystemFeedback()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch feedback data
                    string query = "SELECT SystemBugID, LodgeDate, Status, Reason FROM tblManageSystemFeedback";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading system feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Remove row headers
            dataGridView1.RowHeadersVisible = false;

            // Adjust column widths to fill the DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set alternating row colors
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");

            // Adjust row heights for better spacing
            dataGridView1.RowTemplate.Height = 40;

            // Center-align header text for better presentation
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set header font to bold for readability
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");

            // Set default cell style for better UI
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Disable visual styles for headers
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the user clicked on a valid row
            if (e.RowIndex >= 0)
            {
                // Get the SystemBugID of the selected row
                selectedBugID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SystemBugID"].Value);
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (selectedBugID == 0)
            {
                MessageBox.Show("Please select a row first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Update the Status column in the database to "Solved"
                    string query = "UPDATE tblManageSystemFeedback SET Status = 'Solved' WHERE SystemBugID = @SystemBugID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SystemBugID", selectedBugID);

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Status updated to 'Solved' successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh the DataGridView
                    LoadSystemFeedback();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmAdminDashboard go = new frmAdminDashboard();
            this.Hide();
            go.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
