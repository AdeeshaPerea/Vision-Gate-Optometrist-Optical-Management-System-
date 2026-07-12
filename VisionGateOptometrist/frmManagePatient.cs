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
    public partial class frmManagePatient : Form
    {

        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManagePatient()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManagePatient_Load(object sender, EventArgs e)
        {
            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Set the panel background color (semi-transparent light gray)
            pnlPatient.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Generate the next PatientID
            GenerateNextPatientID();
        }

        private void GenerateNextPatientID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(PatientID), 0) + 1 FROM tblManagePatient";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtPatientID.Text = nextId.ToString(); // Display next PatientID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating PatientID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid Email.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }
            if (!int.TryParse(txtContactNo.Text, out _) || txtContactNo.Text.Length < 10)
            {
                MessageBox.Show("Please enter a valid Contact Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNo.Focus();
                return false;
            }
            if (txtDate.Value > DateTime.Now)
            {
                MessageBox.Show("Date of Birth cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
                return false;
            }
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid Age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManagePatient (Name, Email, Address, ContactNo, DOB, Age) VALUES (@Name, @Email, @Address, @ContactNo, @DOB, @Age)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextPatientID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                    string query = "UPDATE tblManagePatient SET Name = @Name, Email = @Email, Address = @Address, ContactNo = @ContactNo, DOB = @DOB, Age = @Age WHERE PatientID = @PatientID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtPatientID.Text));
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNo.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Age", int.Parse(txtAge.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextPatientID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManagePatient WHERE PatientID = @PatientID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPatientID.Text = reader["PatientID"].ToString();
                                txtName.Text = reader["Name"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                txtContactNo.Text = reader["ContactNo"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["DOB"]);
                                txtAge.Text = reader["Age"].ToString();

                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No patient found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
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
                    string query = "DELETE FROM tblManagePatient WHERE PatientID = @PatientID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(txtPatientID.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Patient deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextPatientID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtContactNo.Clear();
            txtAge.Clear();
            txtSearch.Clear();
            txtDate.Value = DateTime.Now;

            isSearchPerformed = false;
        }
    }
}
