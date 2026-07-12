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
    public partial class frmManageHearingMaintenenanceDetails : Form
    {

        // Database connection string
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        private bool isSearchPerformed = false; // To track if a search was performed

        public frmManageHearingMaintenenanceDetails()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5"); // Set background color
        }

        private void frmManageHearingMaintenenanceDetails_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            GenerateNextHearingMaintenanceID();
        }

        private void GenerateNextHearingMaintenanceID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(HearingMaintenanceID), 0) + 1 FROM tblManageHearingMaintenance";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtHearingMaintenanceID.Text = nextId.ToString(); // Display the next ID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Hearing Maintenance ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageHearingMaintenance (PatientID, PatientName, DeviceType, Description, Date) " +
                                   "VALUES (@PatientID, @PatientName, @DeviceType, @Description, @Date)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@DeviceType", txtType.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextHearingMaintenanceID(); // Refresh the ID
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
                MessageBox.Show("Search the Hearing Maintenance ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageHearingMaintenance SET PatientID = @PatientID, PatientName = @PatientName, DeviceType = @DeviceType, Description = @Description, Date = @Date " +
                                   "WHERE HearingMaintenanceID = @HearingMaintenanceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HearingMaintenanceID", txtHearingMaintenanceID.Text);
                        cmd.Parameters.AddWithValue("@PatientID", txtPatientID.Text);
                        cmd.Parameters.AddWithValue("@PatientName", txtPatientName.Text);
                        cmd.Parameters.AddWithValue("@DeviceType", txtType.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextHearingMaintenanceID(); // Refresh the ID
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
                MessageBox.Show("Search the Hearing Maintenance ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageHearingMaintenance WHERE HearingMaintenanceID = @HearingMaintenanceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HearingMaintenanceID", txtHearingMaintenanceID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextHearingMaintenanceID(); // Refresh the ID
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
            GenerateNextHearingMaintenanceID(); // Refresh the ID
        }
        private void ClearFields()
        {
            txtHearingMaintenanceID.Clear();
            txtPatientID.Clear();
            txtPatientName.Clear();
            txtType.SelectedIndex = -1;
            txtDescription.Clear();
            txtDate.Value = DateTime.Now;
            txtSearch.Clear();

            isSearchPerformed = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric Hearing Maintenance ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageHearingMaintenance WHERE HearingMaintenanceID = @HearingMaintenanceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@HearingMaintenanceID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtHearingMaintenanceID.Text = reader["HearingMaintenanceID"].ToString();
                                txtPatientID.Text = reader["PatientID"].ToString();
                                txtPatientName.Text = reader["PatientName"].ToString();
                                txtType.Text = reader["DeviceType"].ToString();
                                txtDescription.Text = reader["Description"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);

                                isSearchPerformed = true;
                                //MessageBox.Show("Record found!", "Search Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Device Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
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
    }
}
