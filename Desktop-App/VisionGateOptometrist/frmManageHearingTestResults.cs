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
    public partial class frmManageHearingTestResults : Form
    {

        // Connection string for the database
        string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManageHearingTestResults()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageHearingTestResults_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Enable Update and Delete buttons initially
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            GenerateNextHearingTest_No();
        }

        private void GenerateNextHearingTest_No()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(HearingTest_No), 0) + 1 FROM tblManageHearingTestResults";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtTestNumber.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Hearing Test No: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Patient ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTestType.Text))
            {
                MessageBox.Show("Test Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPatientName.Text))
            {
                MessageBox.Show("Patient Name is required. Please find the Patient Name using the Find button.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnFind.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTestResults.Text))
            {
                MessageBox.Show("Test Results are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTestResults.Focus();
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Please enter a Patient ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtPatientID.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPatientName.Text = reader["Name"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No patient found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtPatientName.Clear();
                            }
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
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT HearingTest_No, Patient_ID, Test_Type, Patient_Name, Test_Results, Description FROM tblManageHearingTestResults WHERE HearingTest_No = @HTRNo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HTRNo", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate all fields including Patient_ID
                                txtTestNumber.Text = reader["HearingTest_No"].ToString();
                                txtPatientID.Text = reader["Patient_ID"].ToString(); // Added to load Patient ID
                                txtTestType.Text = reader["Test_Type"].ToString();
                                txtPatientName.Text = reader["Patient_Name"].ToString();
                                txtTestResults.Text = reader["Test_Results"].ToString();
                                txtDescription.Text = reader["Description"].ToString();

                                // Mark search as performed
                                isSearchPerformed = true;
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No result found with the given Hearing Test No.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for Hearing Test Result: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // Modified query to exclude HearingTest_No
                    string query = "INSERT INTO tblManageHearingTestResults (Patient_ID, Test_Type, Patient_Name, Test_Results, Description) VALUES (@PatientID, @TestType, @PatientName, @TestResults, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@TestType", txtTestType.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@TestResults", txtTestResults.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextHearingTest_No(); // Generate the next HearingTest_No for display
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding Hearing Test Result: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if a valid search has been performed
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't responding. Please search for the Hearing Test No first!",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Validate input fields
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to update the record
                    string query = "UPDATE tblManageHearingTestResults SET " +
                                   "Patient_ID = @PatientID, Test_Type = @TestType, " +
                                   "Patient_Name = @PatientName, Test_Results = @TestResults, " +
                                   "Description = @Description " +
                                   "WHERE HearingTest_No = @HTRNo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HTRNo", Convert.ToInt32(txtTestNumber.Text));
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@TestType", txtTestType.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@TestResults", txtTestResults.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record updated successfully!",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        // Clear fields and reset state
                        ClearFields();
                        GenerateNextHearingTest_No();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Hearing Test Result: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Check if a valid search has been performed
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't responding. Please search for the Hearing Test No first!",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion with the user
            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?",
                                                  "Confirm Deletion",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to delete the record
                    string query = "DELETE FROM tblManageHearingTestResults WHERE HearingTest_No = @HTRNo";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HTRNo", Convert.ToInt32(txtTestNumber.Text));

                        // Execute the query
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Record deleted successfully!",
                                        "Success",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);

                        // Clear fields and reset state
                        ClearFields();
                        GenerateNextHearingTest_No();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Hearing Test Result: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }   
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            isSearchPerformed = false;
        }

        private void ClearFields()
        {
            // Clear all fields except HearingTest_No
            txtSearch.Clear();
            txtTestResults.Clear();
            txtTestType.SelectedIndex = -1;
            txtPatientID.Clear();
            txtPatientName.Clear();
            txtDescription.Clear();

            // Regenerate and display the next HearingTest_No
            GenerateNextHearingTest_No();

            

            // Reset the search performed flag
            isSearchPerformed = false;
        }

    }
}
