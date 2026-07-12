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
    public partial class frmManageRevenue : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManageRevenue()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageRevenue_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Generate the next RevenueID
            GenerateNextRevenueID();
        }

        private void GenerateNextRevenueID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch the next RevenueID
                    string query = "SELECT ISNULL(MAX(RevenueID), 0) + 1 FROM tblManageRevenue";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtRevenueID.Text = nextId.ToString(); // Display next RevenueID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating RevenueID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert a new revenue record
                    string query = "INSERT INTO tblManageRevenue (Amount, Date, Description) VALUES (@Amount, @Date, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtdescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Revenue record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextRevenueID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding revenue record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid Revenue ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to search for the revenue record by RevenueID
                    string query = "SELECT RevenueID, Amount, Date, Description FROM tblManageRevenue WHERE RevenueID = @RevenueID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RevenueID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate text boxes
                                txtRevenueID.Text = reader["RevenueID"].ToString();
                                txtAmount.Text = reader["Amount"].ToString();
                                txtDate.Text = Convert.ToDateTime(reader["Date"]).ToShortDateString();
                                txtdescription.Text = reader["Description"].ToString();

                                // Mark search as successful
                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No revenue record found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false; // Reset flag
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for revenue record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't clickable. Search the Revenue ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Update the revenue record
                    string query = "UPDATE tblManageRevenue SET Amount = @Amount, Date = @Date, Description = @Description WHERE RevenueID = @RevenueID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RevenueID", Convert.ToInt32(txtRevenueID.Text));
                        cmd.Parameters.AddWithValue("@Amount", Convert.ToInt32(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtdescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Revenue record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextRevenueID();
                        isSearchPerformed = false; // Reset flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating revenue record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeactivate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't clickable. Search the Revenue ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Delete the revenue record
                    string query = "DELETE FROM tblManageRevenue WHERE RevenueID = @RevenueID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@RevenueID", Convert.ToInt32(txtRevenueID.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Revenue record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextRevenueID();
                        isSearchPerformed = false; // Reset flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting revenue record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            // Clear all input fields except RevenueID
            txtAmount.Clear();
            txtDate.Text = DateTime.Now.ToShortDateString();
            txtdescription.Clear();
            txtSearch.Clear();

            // Reset search flag
            isSearchPerformed = false;

            // Regenerate next RevenueID
            GenerateNextRevenueID();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !int.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Amount must be a valid integer and cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDate.Text) || !DateTime.TryParse(txtDate.Text, out _))
            {
                MessageBox.Show("Date must be valid and cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtdescription.Text))
            {
                MessageBox.Show("Description cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
