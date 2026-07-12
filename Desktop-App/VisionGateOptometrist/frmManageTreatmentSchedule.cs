using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageTreatmentSchedule : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        private bool isSearchPerformed = false; // Flag to track if a search was performed

        public frmManageTreatmentSchedule()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageTreatmentSchedule_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            
            

            // Add dummy values to Schedule Category
            txtScheduleCategory.Items.Add("Physiotherapy");
            txtScheduleCategory.Items.Add("Speech Therapy");
            txtScheduleCategory.Items.Add("Hearing Therapy");
            txtScheduleCategory.Items.Add("Vision Therapy");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a Patient ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch the treatment schedule for the entered Patient ID
                    string query = "SELECT * FROM tblManageTreatmentSchedule WHERE PatientID = @PatientID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPatientID.Text = reader["PatientID"].ToString();
                                txtPatientName.Text = reader["PatientName"].ToString();
                                txtScheduleCategory.Text = reader["Category"].ToString();
                                txtDate.Text = Convert.ToDateTime(reader["Date"]).ToString("yyyy-MM-dd");
                                txtDescription.Text = reader["Description"].ToString();

                                isSearchPerformed = true;

                                // Enable Update and Delete buttons after successful search
                                btnUpdate.Enabled = true;
                                btnDelete.Enabled = true;
                            }
                            else
                            {
                                MessageBox.Show("No record found for the entered Patient ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for treatment schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                    // Query to fetch the patient name for the entered Patient ID
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to insert a new treatment schedule
                    string query = "INSERT INTO tblManageTreatmentSchedule (PatientName, Category, Date, Description) VALUES (@PatientName, @Category, @Date, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@Category", txtScheduleCategory.Text);
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Treatment Schedule added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding treatment schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Please search for a record first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to update the treatment schedule
                    string query = "UPDATE tblManageTreatmentSchedule SET PatientName = @PatientName, Category = @Category, Date = @Date, Description = @Description WHERE PatientID = @PatientID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtPatientID.Text));
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@Category", txtScheduleCategory.Text);
                        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(txtDate.Text));
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Treatment Schedule updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        isSearchPerformed = false;

                        // Disable Update and Delete buttons after update
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating treatment schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Please search for a record first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();

                        // Query to delete the treatment schedule
                        string query = "DELETE FROM tblManageTreatmentSchedule WHERE PatientID = @PatientID";

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtPatientID.Text));

                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Treatment Schedule deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearFields();
                            isSearchPerformed = false;

                            // Disable Update and Delete buttons after deletion
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting treatment schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            isSearchPerformed = false;

            
        }

        private void ClearFields()
        {
            txtSearch.Clear();
            txtPatientID.Clear();
            txtPatientName.Clear();
            txtScheduleCategory.SelectedIndex = -1;
            txtDate.ResetText();
            txtDescription.Clear();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtPatientID.Text))
            {
                MessageBox.Show("Patient ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtScheduleCategory.Text))
            {
                MessageBox.Show("Schedule Category is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtScheduleCategory.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDate.Text))
            {
                MessageBox.Show("Schedule Date is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Schedule Description is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            return true;
        }

        private void pnlStock_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
