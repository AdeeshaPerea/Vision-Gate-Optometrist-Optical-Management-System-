using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageSupplierPayment : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSearchPerformed = false;

        public frmManageSupplierPayment()
        {
            InitializeComponent();
        }

        private void frmManageSupplierPayment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Initialize DateTimePicker to the current date
            txtDate.Value = DateTime.Now;

            GenerateNextSupplierPaymentID(); // Auto-generate SupplierPaymentID
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Supplier Payment ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageSupplierPayment WHERE SupplierPaymentID = @SupplierPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierPaymentID", txtSearch.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtSupplierPaymentID.Text = reader["SupplierPaymentID"].ToString();
                                txtSupplierID.Text = reader["SupplierID"].ToString();
                                txtSupplierName.Text = reader["SupplierName"].ToString();
                                txtAmount.Text = reader["PaymentAmount"].ToString();
                                txtMethod.Text = reader["PaymentMethod"].ToString();

                                // Set the PaymentDate if available
                                if (reader["PaymentDate"] != DBNull.Value)
                                {
                                    txtDate.Value = Convert.ToDateTime(reader["PaymentDate"]);
                                }
                                else
                                {
                                    txtDate.Value = DateTime.Now;
                                }

                                txtStatus.Text = reader["Status"].ToString();
                                isSearchPerformed = true;
                                txtSearch.Clear();
                            }
                            else
                            {
                                // Clear fields if no data is found
                                MessageBox.Show("No record found with the given Supplier Payment ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (string.IsNullOrWhiteSpace(txtSupplierID.Text))
            {
                MessageBox.Show("Please enter a Supplier ID to find.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT SupplierName FROM tblManageSuppliers WHERE SupplierID = @SupplierID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text.Trim());

                        var supplierName = cmd.ExecuteScalar();
                        if (supplierName != null)
                        {
                            txtSupplierName.Text = supplierName.ToString();
                            MessageBox.Show("Supplier found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No supplier found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSupplierName.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding supplier: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "INSERT INTO tblManageSupplierPayment (SupplierID, SupplierName, PaymentAmount, PaymentMethod, PaymentDate, Status) " +
                                   "VALUES (@SupplierID, @SupplierName, @PaymentAmount, @PaymentMethod, @PaymentDate, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text);
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@PaymentAmount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@PaymentMethod", txtMethod.Text);
                        cmd.Parameters.AddWithValue("@PaymentDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Status", txtStatus.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSupplierPaymentID();
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
                MessageBox.Show("Search the Supplier Payment ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageSupplierPayment SET SupplierID = @SupplierID, SupplierName = @SupplierName, PaymentAmount = @PaymentAmount, PaymentMethod = @PaymentMethod, PaymentDate = @PaymentDate, Status = @Status " +
                                   "WHERE SupplierPaymentID = @SupplierPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierPaymentID", txtSupplierPaymentID.Text);
                        cmd.Parameters.AddWithValue("@SupplierID", txtSupplierID.Text);
                        cmd.Parameters.AddWithValue("@SupplierName", txtSupplierName.Text);
                        cmd.Parameters.AddWithValue("@PaymentAmount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@PaymentMethod", txtMethod.Text);
                        cmd.Parameters.AddWithValue("@PaymentDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Status", txtStatus.Text);

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
                MessageBox.Show("Search the Supplier Payment ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageSupplierPayment WHERE SupplierPaymentID = @SupplierPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SupplierPaymentID", txtSupplierPaymentID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Record deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSupplierPaymentID();
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

        private void GenerateNextSupplierPaymentID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(SupplierPaymentID), 0) + 1 FROM tblManageSupplierPayment";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtSupplierPaymentID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Supplier Payment ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtSearch.Clear();
            //txtSupplierPaymentID.Clear();
            txtSupplierID.Clear();
            txtSupplierName.Clear();
            txtAmount.Clear();
            txtMethod.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            txtStatus.SelectedIndex = -1;
            isSearchPerformed = false;
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtSupplierID.Text))
            {
                MessageBox.Show("Supplier ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Supplier Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupplierName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Payment Amount must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            return true;
        }
    }
}
