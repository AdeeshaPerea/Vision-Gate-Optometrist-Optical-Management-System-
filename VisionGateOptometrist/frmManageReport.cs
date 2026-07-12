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
    public partial class frmManageReport : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isReportSearched = false;  // Flag to check if report ID has been searched

        public frmManageReport()
        {
            InitializeComponent();
        }

        private void frmManageReport_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Populate Type dropdown
            txtType.Items.Add("Patient Report");
            txtType.Items.Add("Financial Report");
            txtType.Items.Add("Sales Report");
            txtType.Items.Add("Channelling Report");
            txtType.Items.Add("Feedback Report");
            txtType.Items.Add("Final Report");

            // Generate Next Report ID
            GenerateNextReportID();
            // Load data into GridView
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ReportID, Name, Type, Date, Description FROM tblManageReport";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;

                            // Auto-fit columns except for specific ones
                            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                            // Set specific column widths
                            dataGridView1.Columns["ReportID"].Width = 80; // Narrow column for ID
                            dataGridView1.Columns["Name"].Width = 150;
                            dataGridView1.Columns["Type"].Width = 140;
                            dataGridView1.Columns["Date"].Width = 140;
                            dataGridView1.Columns["Description"].Width = 200; // Wider column for Description

                            // Style Column Headers
                            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
                            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                            // Style Rows
                            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
                            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                            // Disable "Add New Row"
                            dataGridView1.AllowUserToAddRows = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // Method to Generate Next Report ID on form load
        private void GenerateNextReportID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(ReportID), 0) + 1 FROM tblManageReport";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtReportID.Text = nextId.ToString();  // Auto-generate Report ID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Report ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pnlPatientInquiry_Paint(object sender, PaintEventArgs e)
        {
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void btnSearchMR_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchMR.Text))
            {
                MessageBox.Show("Please enter a valid Report ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ReportID, Name, Type, Date, Description FROM tblManageReport WHERE ReportID = @ReportID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReportID", txtSearchMR.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate fields with fetched data
                                txtReportID.Text = reader["ReportID"].ToString();
                                txtName.Text = reader["Name"].ToString();
                                txtType.SelectedItem = reader["Type"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtDescription.Text = reader["Description"].ToString();

                                isReportSearched = true; // Set flag that Report ID has been searched
                                //MessageBox.Show("Report found and loaded!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearchMR.Clear();

                                // Highlight the row and navigate to it in the DataGridView
                                HighlightRowInGrid(txtReportID.Text);
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Report ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isReportSearched = false; // Reset flag
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HighlightRowInGrid(string reportID)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["ReportID"].Value != null && row.Cells["ReportID"].Value.ToString() == reportID)
                {
                    row.Selected = true; // Highlight the row
                    dataGridView1.FirstDisplayedScrollingRowIndex = row.Index; // Scroll to the row
                    return;
                }
            }
            MessageBox.Show("Report found but not displayed in the grid view.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }


        private void btnAddchan_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageReport (Name, Type, Date, Description) VALUES (@Name, @Type, @Date, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Type", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Report added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextReportID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdatechan_Click(object sender, EventArgs e)
        {
            if (!isReportSearched)
            {
                MessageBox.Show("Update button not responding. Please search the Report ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageReport SET Name = @Name, Type = @Type, Date = @Date, Description = @Description WHERE ReportID = @ReportID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReportID", txtReportID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Type", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Report updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeletechan_Click(object sender, EventArgs e)
        {
            if (!isReportSearched)
            {
                MessageBox.Show("Delete button not responding. Please search the Report ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageReport WHERE ReportID = @ReportID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ReportID", txtReportID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Report deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextReportID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnclearchan_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtType.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            txtDescription.Clear();
            isReportSearched = false;  // Reset search flag
        }


        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (txtType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReportData(); // Refresh the table by reloading data from the database
        }
    }
}
