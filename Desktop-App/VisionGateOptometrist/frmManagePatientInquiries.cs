using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManagePatientInquiries : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmManagePatientInquiries()
        {
            InitializeComponent();
        }

        private void frmManagePatientInquiries_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Populate Inquiry Type dropdown
            PopulateInquiryTypeDropdown();

            // Generate the next InquiryID
            GenerateNextInquiryID();
        }

        private void PopulateInquiryTypeDropdown()
        {
            txtType.Items.Add("General Inquiry");
            txtType.Items.Add("Follow-up");
            txtType.Items.Add("Complaint");
            // Add any other inquiry types as necessary
        }

        private void GenerateNextInquiryID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(InquiryID), 0) + 1 FROM tblPatientInquiries";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtInquiryID.Text = nextId.ToString(); // Auto-generate InquiryID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Inquiry ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Inquiry ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT InquiryID, InquiryType, Description FROM tblPatientInquiries WHERE InquiryID = @InquiryID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InquiryID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Correct mapping to table columns
                                txtInquiryID.Text = reader["InquiryID"].ToString();
                                txtType.SelectedItem = reader["InquiryType"].ToString(); // Correct column name
                                txtDescription.Text = reader["Description"].ToString(); // Correct column name

                                MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Inquiry ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblPatientInquiries (InquiryType, Description) VALUES (@InquiryType, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InquiryType", txtType.SelectedItem.ToString()); // Correct column name
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text); // Correct column name

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Inquiry added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextInquiryID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding inquiry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Ensure InquiryID is searched first before allowing update
            if (string.IsNullOrWhiteSpace(txtInquiryID.Text) || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Update button not responding. Please search the Inquiry ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblPatientInquiries SET InquiryType = @InquiryType, Description = @Description WHERE InquiryID = @InquiryID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InquiryID", txtInquiryID.Text);
                        cmd.Parameters.AddWithValue("@InquiryType", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        // Execute the update operation
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Inquiry updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear fields after successful update
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating inquiry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Ensure InquiryID is searched first before allowing delete
            if (string.IsNullOrWhiteSpace(txtInquiryID.Text) || string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Delete button not responding. Please search the Inquiry ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblPatientInquiries WHERE InquiryID = @InquiryID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InquiryID", txtInquiryID.Text); // Ensure correct InquiryID is used

                        // Execute the delete operation
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Inquiry deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear fields after successful delete
                        ClearFields();
                        GenerateNextInquiryID(); // Generate new InquiryID for the next inquiry
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting inquiry: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtInquiryID.Clear();
            txtType.SelectedIndex = -1;
            txtDescription.Clear();
            GenerateNextInquiryID(); // Generate next InquiryID after clearing
        }

        private bool ValidateInputs()
        {
            if (txtType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Inquiry Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
