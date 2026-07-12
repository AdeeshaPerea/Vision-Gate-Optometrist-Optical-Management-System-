using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageEarGuidance : Form
    {
        // Database connection string
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        private bool isSearchPerformed = false; // Track if search was performed

        public frmManageEarGuidance()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5"); // Set form background color
        }

        private void frmManageEarGuidance_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            pnlStock.BackColor = Color.FromArgb(200, 240, 240, 240); // Set panel background color
            txtCategory.SelectedIndex = -1; // Reset ComboBox selection
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text) || !int.TryParse(txtPatientID.Text, out _))
            {
                MessageBox.Show("Patient ID is required and must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                MessageBox.Show("Patient Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientName.Focus();
                return false;
            }
            if (txtCategory.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return false;
            }
            if (txtDate.Value > DateTime.Now)
            {
                MessageBox.Show("Date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            txtPatientID.Clear();
            txtPatientName.Clear();
            txtCategory.SelectedIndex = -1; // Reset ComboBox selection
            txtDate.Value = DateTime.Now;
            txtDescription.Clear();
            txtSearch.Clear();

            isSearchPerformed = false;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageEarGuidance (PatientID, PatientName, Category, Date, Description) " +
                                   "VALUES (@PatientID, @PatientName, @Category, @Date, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@Category", txtCategory.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Error adding record: {ex.Message}\nPlease verify the table name and database connection.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Patient ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageEarGuidance SET PatientName = @PatientName, Category = @Category, Date = @Date, Description = @Description " +
                                   "WHERE PatientID = @PatientID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@Category", txtCategory.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Patient ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageEarGuidance WHERE PatientID = @PatientID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text) || !int.TryParse(txtPatientID.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric Patient ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManagePatient WHERE PatientID = @PatientID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);

                        var patientName = cmd.ExecuteScalar();
                        if (patientName != null)
                        {
                            txtPatientName.Text = patientName.ToString();
                        }
                        else
                        {
                            MessageBox.Show("No patient found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPatientName.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric Patient ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageEarGuidance WHERE PatientID = @PatientID"; // Ensure the table name is correct
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPatientID.Text = reader["PatientID"].ToString();
                                txtPatientName.Text = reader["PatientName"].ToString();
                                txtCategory.Text = reader["Category"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtDescription.Text = reader["Description"].ToString();

                                isSearchPerformed = true;
                                MessageBox.Show("Record found!", "Search Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Patient ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
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
    }
}
