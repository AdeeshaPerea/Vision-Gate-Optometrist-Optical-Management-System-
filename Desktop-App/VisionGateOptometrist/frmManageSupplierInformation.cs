using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageSupplierInformation : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSupplierSearched = false; // Flag to check if a supplier has been searched

        public frmManageSupplierInformation()
        {
            InitializeComponent();
        }

        private void frmManageSupplierInformation_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
            GenerateNextSupplierID(); // Generate the next Supplier ID
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Supplier ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GenerateNextSupplierID(); // Generate the next Supplier ID
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT SupplierID, SupplierName, Email, Address, ContactNumber FROM tblManageSuppliers WHERE SupplierID = @SupplierID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtSupplierID.Text = reader["SupplierID"].ToString();
                                txtSupplierName.Text = reader["SupplierName"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                txtNumber.Text = reader["ContactNumber"].ToString();

                                isSupplierSearched = true; // Set flag
                            }
                            else
                            {
                                MessageBox.Show("No supplier found with the given Supplier ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                GenerateNextSupplierID(); // Generate the next Supplier ID
                                isSupplierSearched = false; // Reset flag
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "INSERT INTO tblManageSuppliers (SupplierName, Email, Address, ContactNumber) VALUES (@SupplierName, @Email, @Address, @ContactNumber)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtNumber.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Supplier added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSupplierID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSupplierSearched)
            {
                MessageBox.Show("Please search the Supplier ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageSuppliers SET SupplierName = @SupplierName, Email = @Email, Address = @Address, ContactNumber = @ContactNumber WHERE SupplierID = @SupplierID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text);
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                        cmd.Parameters.AddWithValue("@ContactNumber", txtNumber.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Supplier updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSupplierSearched)
            {
                MessageBox.Show("Please search the Supplier ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageSuppliers WHERE SupplierID = @SupplierID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Supplier deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSupplierID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            GenerateNextSupplierID(); // Generate the next Supplier ID
        }

        private void GenerateNextSupplierID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(SupplierID), 0) + 1 FROM tblManageSuppliers";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtSupplierID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Supplier ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtSearch.Clear();
            //txtSupplierID.Clear();
            txtSupplierName.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtNumber.Clear();
            isSupplierSearched = false; // Reset search flag
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Supplier Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("Contact Number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumber.Focus();
                return false;
            }

            // Check if the contact number is numeric
            if (!long.TryParse(txtNumber.Text, out _))
            {
                MessageBox.Show("Contact Number must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumber.Focus();
                return false;
            }

            // Check the length of the contact number (e.g., 10 digits)
            if (txtNumber.Text.Length != 10)
            {
                MessageBox.Show("Contact Number must be exactly 10 digits.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumber.Focus();
                return false;
            }

            return true;
        }

    }
}
