using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewUserAccounts : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewUserAccounts()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewUserAccounts_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load all user accounts into the DataGridView
            LoadAllUserAccounts();
        }

        private void LoadAllUserAccounts()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch all user accounts
                    string query = "SELECT id, username, loginDate, adminToken, isActive FROM tblManageUserAccount";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the DataTable to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Apply customizations
                        CustomizeDataGridView();

                        dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["id"].Value?.ToString() == "1").ToList().ForEach(r => r.Cells["adminToken"].Style.BackColor = Color.Black);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Remove row headers for a cleaner look
            dataGridView1.RowHeadersVisible = false;

            // Set the column widths to fill the DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set alternating row colors for better readability
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");

            // Adjust the row height for better visibility
            dataGridView1.RowTemplate.Height = 35;

            // Center-align header text for a professional appearance
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set the header font to bold for emphasis
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Set the header background color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");

            // Apply default cell font and colors for readability
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Disable the visual styles for the header
            dataGridView1.EnableHeadersVisualStyles = false;

            // Set the background color of the DataGridView
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Check if txtSearch is empty
                    if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    {
                        MessageBox.Show("Please enter a search term.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Query to search for matching user accounts by ID or username
                    string query = "SELECT id, username, loginDate, adminToken, isActive FROM tblManageUserAccount " +
                                   "WHERE CAST(id AS NVARCHAR) LIKE @search OR username LIKE @search";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use the input to search by ID or username
                        cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text}%");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                // Bind the filtered results to the DataGridView
                                dataGridView1.DataSource = dataTable;

                                // Apply customizations
                                CustomizeDataGridView();
                            }
                            else
                            {
                                MessageBox.Show("No records found matching your search.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                dataGridView1.DataSource = null; // Clear the DataGridView
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching user accounts: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch the admin token for the user with id = 1
                    string tokenQuery = "SELECT adminToken FROM tblManageUserAccount WHERE id = 1";

                    using (SqlCommand cmd = new SqlCommand(tokenQuery, con))
                    {
                        var result = cmd.ExecuteScalar();

                        if (result != null && result.ToString() == txtGetpassword.Text)
                        {
                            // If the admin token matches, fetch all user accounts including the password
                            string query = "SELECT id, username, password, loginDate, adminToken, isActive FROM tblManageUserAccount";

                            using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                // Bind the DataTable to the DataGridView
                                dataGridView1.DataSource = dataTable;

                                // Apply customizations
                                CustomizeDataGridView();

                                dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["id"].Value?.ToString() == "1").ToList().ForEach(r => r.Cells["adminToken"].Style.BackColor = Color.Black);

                                MessageBox.Show("Password column is now visible.", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid Admin Token. Access denied.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnReload_Click(object sender, EventArgs e)
        {
            try
            {
                // Reload all user accounts into the DataGridView
                LoadAllUserAccounts();
                txtSearch.Clear(); // Clear the search field
                txtGetpassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing the table: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlUserAcconts_Paint(object sender, PaintEventArgs e)
        {
            // keep this as it is
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmAdminDashboard go = new frmAdminDashboard();
            this.Hide();
            go.Show();
        }
    }
}