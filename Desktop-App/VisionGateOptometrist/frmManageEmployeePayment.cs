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
    public partial class frmManageEmployeePayment : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSearchPerformed = false;

        public frmManageEmployeePayment()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageEmployeePayment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            pnlStock.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Disable fields initially
            DisableFields();

            // Generate the next EmployeePaymentID when the form loads
            GenerateNextEmployeePaymentID();
        }

        private void DisableFields()
        {
            txtEmployeePaymentID.Enabled = false;
            txtDate.Enabled = false;
            txtAmount.Enabled = false;
            txtMethod.Enabled = false;
            txtStatus.Enabled = false;
        }

        private void EnableFields()
        {
            txtEmployeePaymentID.Enabled = true;
            txtDate.Enabled = true;
            txtAmount.Enabled = true;
            txtMethod.Enabled = true;
            txtStatus.Enabled = true;
        }

        private void GenerateNextEmployeePaymentID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(EmployeePaymentID), 0) + 1 FROM tblManageEmployeePayment";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtEmployeePaymentID.Text = nextId.ToString(); // Display the next EmployeePaymentID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating EmployeePaymentID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void pnlStock_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEPAmmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Validate that the search field is not empty
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Employee Payment ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageEmployeePayment WHERE EmployeePaymentID = @EmployeePaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use the value from txtSearch for the parameter
                        cmd.Parameters.AddWithValue("@EmployeePaymentID", txtSearch.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // If a record is found, populate the fields
                            if (reader.Read())
                            {
                                txtEmployeeID.Text = reader["EmployeeID"].ToString();
                                txtEmployeePaymentID.Text = reader["EmployeePaymentID"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["PaymentDate"]);
                                txtAmount.Text = reader["PaymentAmount"].ToString();
                                txtMethod.Text = reader["PaymentMethod"].ToString();
                                txtStatus.Text = reader["PaymentStatus"].ToString();

                                // Enable fields for further actions
                                isSearchPerformed = true;
                                EnableFields();

                                MessageBox.Show("Record found!", "Search Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                // If no record is found
                                MessageBox.Show("No record found with the given Payment ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                DisableFields();
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
            if (string.IsNullOrWhiteSpace(txtEmployeeID.Text) || !int.TryParse(txtEmployeeID.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric Employee ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Name FROM tblManageEmployee WHERE EmployeeID = @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);

                        var employeeName = cmd.ExecuteScalar();
                        if (employeeName != null)
                        {
                            // If employee exists, show message and enable the fields
                            MessageBox.Show($"Employee found: {employeeName}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            EnableFields();
                        }
                        else
                        {
                            // No employee found
                            MessageBox.Show("No employee found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            DisableFields();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtEmployeeID.Text) || !int.TryParse(txtEmployeeID.Text, out _))
            {
                MessageBox.Show("Employee ID is required and must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeeID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmployeePaymentID.Text))
            {
                MessageBox.Show("Employee Payment ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmployeePaymentID.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Payment Amount is required and must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMethod.Text))
            {
                MessageBox.Show("Payment Method is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMethod.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtStatus.Text))
            {
                MessageBox.Show("Payment Status is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStatus.Focus();
                return false;
            }
            if (txtDate.Value > DateTime.Now)
            {
                MessageBox.Show("Payment Date cannot be in the future.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDate.Focus();
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
                    string query = "INSERT INTO tblManageEmployeePayment (EmployeeID, PaymentDate, PaymentAmount, PaymentMethod, PaymentStatus) " +
                                   "VALUES (@EmployeeID, @PaymentDate, @PaymentAmount, @PaymentMethod, @PaymentStatus)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
                        cmd.Parameters.AddWithValue("@PaymentDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@PaymentAmount", decimal.Parse(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@PaymentMethod", txtMethod.Text); // Take the entered value
                        cmd.Parameters.AddWithValue("@PaymentStatus", txtStatus.Text); // Take the entered value

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        DisableFields();
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
                MessageBox.Show("Search the Employee Payment ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageEmployeePayment SET PaymentDate = @PaymentDate, PaymentAmount = @PaymentAmount, PaymentMethod = @PaymentMethod, PaymentStatus = @PaymentStatus WHERE EmployeePaymentID = @EmployeePaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeePaymentID", txtEmployeePaymentID.Text);
                        cmd.Parameters.AddWithValue("@PaymentDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@PaymentAmount", decimal.Parse(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@PaymentMethod", txtMethod.Text);
                        cmd.Parameters.AddWithValue("@PaymentStatus", txtStatus.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        DisableFields();
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
                MessageBox.Show("Search the Employee Payment ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageEmployeePayment WHERE EmployeePaymentID = @EmployeePaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeePaymentID", txtEmployeePaymentID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        DisableFields();
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
            DisableFields();
        }

        private void ClearFields()
        {
            txtEmployeeID.Clear();
            //txtEmployeePaymentID.Clear();
            txtAmount.Clear();
            txtMethod.SelectedIndex = -1;
            txtStatus.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            txtSearch.Clear();

            isSearchPerformed = false;
        }
    }
}
