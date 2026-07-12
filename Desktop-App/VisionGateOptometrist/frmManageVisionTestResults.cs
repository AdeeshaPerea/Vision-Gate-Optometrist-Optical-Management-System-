using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageVisionTestResults : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSearchPerformed = false;

        public frmManageVisionTestResults()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageVisionTestResults_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Populate Test Type dropdown
            txtTestType.Items.Add("Color Blindness Test");
            txtTestType.Items.Add("Visual Acuity Test");
            txtTestType.Items.Add("Refraction Test");
            txtTestType.Items.Add("Eye Pressure Test");

            // Generate the next Vision Test Number
            GenerateNextTestNumber();
        }

        private void GenerateNextTestNumber()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(VisionTestNumber), 0) + 1 FROM tblManageVisionTestResult";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextTestNumber = Convert.ToInt32(cmd.ExecuteScalar());
                        txtTestNumber.Text = nextTestNumber.ToString();
                        txtTestNumber.ReadOnly = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Test Number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Vision Test Number to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageVisionTestResult WHERE VisionTestNumber = @VisionTestNumber";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@VisionTestNumber", txtSearch.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtTestNumber.Text = reader["VisionTestNumber"].ToString();
                                txtTestType.SelectedItem = reader["TestType"].ToString();
                                txtPatientID.Text = reader["PatientID"].ToString();
                                txtTestDate.Value = Convert.ToDateTime(reader["TestDate"]); // Use DateTimePicker.Value
                                txtTestResult.Text = reader["TestResult"].ToString();
                                txtPatientName.Text = ""; // Clear Patient Name since we will find it next

                                isSearchPerformed = true;
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Vision Test Number.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Please enter a Patient ID to find.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text.Trim());

                        var patientName = cmd.ExecuteScalar();
                        if (patientName != null)
                        {
                            txtPatientName.Text = patientName.ToString();
                            MessageBox.Show("Patient found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageVisionTestResult (TestType, PatientID, PatientName, TestDate, TestResult) " +
                                   "VALUES (@TestType, @PatientID, @PatientName, @TestDate, @TestResult)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TestType", txtTestType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text); // Include PatientName
                        cmd.Parameters.AddWithValue("@TestDate", txtTestDate.Value); // Use DateTimePicker.Value
                        cmd.Parameters.AddWithValue("@TestResult", txtTestResult.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextTestNumber();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Vision Test Number first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageVisionTestResult SET TestType = @TestType, PatientID = @PatientID, TestDate = @TestDate, TestResult = @TestResult WHERE VisionTestNumber = @VisionTestNumber";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@VisionTestNumber", txtTestNumber.Text);
                        cmd.Parameters.AddWithValue("@TestType", txtTestType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@TestDate", txtTestDate.Value); // Use DateTimePicker.Value
                        cmd.Parameters.AddWithValue("@TestResult", txtTestResult.Text);

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Vision Test Number first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageVisionTestResult WHERE VisionTestNumber = @VisionTestNumber";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@VisionTestNumber", txtTestNumber.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextTestNumber();
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

        private void ClearFields()
        {
            txtSearch.Clear();
            //txtTestNumber.Clear();
            txtTestType.SelectedIndex = -1;
            txtPatientID.Clear();
            txtPatientName.Clear();
            txtTestDate.Value = DateTime.Now; // Use DateTimePicker.Value
            txtTestResult.Clear();
            isSearchPerformed = false;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTestType.Text))
            {
                MessageBox.Show("Test Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Patient ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTestResult.Text))
            {
                MessageBox.Show("Test Result is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (txtTestDate.Value > DateTime.Now)
            {
                MessageBox.Show("Test Date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // keep this event as it is
        }
    }
}
