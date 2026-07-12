using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManagePatientPayment : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isPaymentSearched = false; // Flag to track if Payment ID has been searched

        public frmManagePatientPayment()
        {
            InitializeComponent();
        }

        private void frmManagePatientPayment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Populate the Method and Status dropdowns
            PopulateMethodDropdown();
            PopulateStatusDropdown();

            // Generate next PaymentID
            GenerateNextPaymentID();
        }

        private void PopulateMethodDropdown()
        {
            txtMethod.Items.Add("Cash");
            txtMethod.Items.Add("Credit");
            txtMethod.Items.Add("Debit");
            // Add more methods if necessary
        }

        private void PopulateStatusDropdown()
        {
            txtStatus.Items.Add("Pending");
            txtStatus.Items.Add("Completed");
            txtStatus.Items.Add("Cancelled");
            // Add more statuses if necessary
        }

        private void GenerateNextPaymentID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(PatientPaymentID), 0) + 1 FROM tblManagePatientPayment";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtPaymentID.Text = nextId.ToString(); // Auto-generate Patient Payment ID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Payment ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Patient Payment ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT PatientPaymentID, Date, Amount, Method, Status FROM tblManagePatientPayment WHERE PatientPaymentID = @PatientPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientPaymentID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtPaymentID.Text = reader["PatientPaymentID"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtAmount.Text = reader["Amount"].ToString();
                                txtMethod.Text = reader["Method"].ToString();
                                txtStatus.Text = reader["Status"].ToString();

                                //MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Patient Payment ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    string query = "INSERT INTO tblManagePatientPayment (Date, Amount, Method, Status) VALUES (@Date, @Amount, @Method, @Status)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@Method", txtMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Payment added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextPaymentID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isPaymentSearched)
            {
                MessageBox.Show("Update button not responding. Please search the Payment ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManagePatientPayment SET Date = @Date, Amount = @Amount, Method = @Method, Status = @Status WHERE PatientPaymentID = @PatientPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientPaymentID", txtPaymentID.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Amount", txtAmount.Text);
                        cmd.Parameters.AddWithValue("@Method", txtMethod.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Payment updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        isPaymentSearched = false; // Reset after successful update
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isPaymentSearched)
            {
                MessageBox.Show("Delete button not responding. Please search the Payment ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManagePatientPayment WHERE PatientPaymentID = @PatientPaymentID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PatientPaymentID", txtPaymentID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Payment deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextPaymentID();
                        isPaymentSearched = false; // Reset after successful deletion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting payment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtPaymentID.Clear();
            txtAmount.Clear();
            txtMethod.SelectedIndex = -1;
            txtStatus.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            GenerateNextPaymentID();
            isPaymentSearched = false; // Reset search flag
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Amount must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            if (txtMethod.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a payment method.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMethod.Focus();
                return false;
            }

            if (txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStatus.Focus();
                return false;
            }

            return true;
        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One go = new frmMenuPage_All_in_One();
            go.Show();
            this.Hide();
        }
    }
}
