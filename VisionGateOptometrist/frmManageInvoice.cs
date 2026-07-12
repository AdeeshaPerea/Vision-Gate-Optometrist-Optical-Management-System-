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
    public partial class frmManageInvoice : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmManageInvoice()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageInvoice_Load(object sender, EventArgs e)
        {
            // Set form properties
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Bind the SelectedIndexChanged event
            txtType.SelectedIndexChanged += TxtType_SelectedIndexChanged;

            // Set a default selection for the Invoice Type dropdown
            if (txtType.Items.Count > 0)
                txtType.SelectedIndex = 0;

            // Generate the next Invoice ID and display it
            GenerateNextInvoiceID();
        }

        private void GenerateNextInvoiceID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(InvoiceID), 0) + 1 FROM tblManageInvoice";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtInvoiceID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Invoice ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void TxtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtType.SelectedItem == null)
            {
                lblChangeID.Text = "ID"; // Default fallback when no item is selected
                return;
            }

            // Update lblChangeID text based on Invoice Type
            switch (txtType.SelectedItem.ToString())
            {
                case "Employee Invoice":
                    lblChangeID.Text = "Employee ID";
                    break;
                case "Patient Invoice":
                    lblChangeID.Text = "Patient ID";
                    break;
                case "Supplier Invoice":
                    lblChangeID.Text = "Supplier ID";
                    break;
                default:
                    lblChangeID.Text = "ID"; // Default fallback
                    break;
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Validate Search Input
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid Invoice ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            // Fetch and populate invoice data
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageInvoice WHERE InvoiceID = @InvoiceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceID", txtSearch.Text);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtInvoiceID.Text = reader["InvoiceID"].ToString();
                                txtName.Text = reader["Name"].ToString();
                                txtType.SelectedItem = reader["InvoiceType"].ToString();
                                txtChangeID.Text = reader["EmployeeID"] != DBNull.Value ? reader["EmployeeID"].ToString() :
                                                   reader["PatientID"] != DBNull.Value ? reader["PatientID"].ToString() :
                                                   reader["SupplierID"] != DBNull.Value ? reader["SupplierID"].ToString() : "";
                                txtAmount.Text = reader["InvoiceAmount"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtDescription.Text = reader["Description"].ToString();

                                //MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Invoice ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                txtSearch.Clear();
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
            // Validate inputs
            if (!ValidateInputs()) return;

            // Check if the entered ID exists in the respective table
            if (!ValidateID())
            {
                MessageBox.Show($"Invalid {lblChangeID.Text}. Please ensure the ID exists.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Insert data into tblManageInvoice
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageInvoice (Name, InvoiceType, EmployeeID, PatientID, SupplierID, InvoiceAmount, Date, Description) " +
                                   "VALUES (@Name, @InvoiceType, @EmployeeID, @PatientID, @SupplierID, @InvoiceAmount, @Date, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@InvoiceType", txtType.SelectedItem.ToString());

                        // Assign correct ID based on Invoice Type
                        cmd.Parameters.AddWithValue("@EmployeeID", lblChangeID.Text == "Employee ID" ? (object)txtChangeID.Text : 0);
                        cmd.Parameters.AddWithValue("@PatientID", lblChangeID.Text == "Patient ID" ? (object)txtChangeID.Text : 0);
                        cmd.Parameters.AddWithValue("@SupplierID", lblChangeID.Text == "Supplier ID" ? (object)txtChangeID.Text : 0);

                        // Handle other fields
                        cmd.Parameters.AddWithValue("@InvoiceAmount", decimal.Parse(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", string.IsNullOrWhiteSpace(txtDescription.Text) ? "-" : txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Invoice added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceID.Text))
            {
                MessageBox.Show("Search the Invoice ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs
            if (!ValidateInputs()) return;

            // Update data in tblManageInvoice
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageInvoice SET Name = @Name, InvoiceType = @InvoiceType, EmployeeID = @EmployeeID, " +
                                   "PatientID = @PatientID, SupplierID = @SupplierID, InvoiceAmount = @InvoiceAmount, Date = @Date, Description = @Description " +
                                   "WHERE InvoiceID = @InvoiceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceID", txtInvoiceID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@InvoiceType", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@EmployeeID", lblChangeID.Text == "Employee ID" ? (object)txtChangeID.Text : DBNull.Value);
                        cmd.Parameters.AddWithValue("@PatientID", lblChangeID.Text == "Patient ID" ? (object)txtChangeID.Text : DBNull.Value);
                        cmd.Parameters.AddWithValue("@SupplierID", lblChangeID.Text == "Supplier ID" ? (object)txtChangeID.Text : DBNull.Value);
                        cmd.Parameters.AddWithValue("@InvoiceAmount", decimal.Parse(txtAmount.Text));
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Invoice updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GenerateNextInvoiceID();

                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInvoiceID.Text))
            {
                MessageBox.Show("Search the Invoice ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Delete data from tblManageInvoice
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageInvoice WHERE InvoiceID = @InvoiceID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@InvoiceID", txtInvoiceID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Invoice deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GenerateNextInvoiceID();

                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private bool ValidateInputs()
        {
            if (txtType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Invoice Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtChangeID.Text))
            {
                MessageBox.Show($"{lblChangeID.Text} is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangeID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text) || !decimal.TryParse(txtAmount.Text, out _))
            {
                MessageBox.Show("Invoice Amount must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateID()
        {
            string tableName = string.Empty;
            string columnName = string.Empty;

            switch (txtType.SelectedItem.ToString())
            {
                case "Employee Invoice":
                    tableName = "tblManageEmployee";
                    columnName = "EmployeeID";
                    break;
                case "Patient Invoice":
                    tableName = "tblManagePatient";
                    columnName = "PatientID";
                    break;
                case "Supplier Invoice":
                    tableName = "tblManageSuppliers";
                    columnName = "SupplierID";
                    break;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = $"SELECT COUNT(*) FROM {tableName} WHERE {columnName} = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtChangeID.Text);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating {lblChangeID.Text}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void ClearFields()
        {
            txtType.SelectedIndexChanged -= TxtType_SelectedIndexChanged; // Unsubscribe
            txtInvoiceID.Clear();
            GenerateNextInvoiceID();
            txtName.Clear();
            txtType.SelectedIndex = -1; // Reset selection
            txtChangeID.Clear();
            txtAmount.Clear();
            txtDate.Value = DateTime.Now;
            txtDescription.Clear();
            txtType.SelectedIndexChanged += TxtType_SelectedIndexChanged; // Resubscribe
        }


        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChangeID.Text) || !int.TryParse(txtChangeID.Text, out _))
            {
                MessageBox.Show($"Please enter a valid {lblChangeID.Text}.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChangeID.Focus();
                return;
            }

            string tableName = string.Empty;
            string columnName = string.Empty;
            string nameColumn = "Name"; // Default column for name retrieval

            // Determine the table and column based on Invoice Type
            switch (txtType.SelectedItem?.ToString())
            {
                case "Employee Invoice":
                    tableName = "tblManageEmployee";
                    columnName = "EmployeeID";
                    nameColumn = "Name"; // Update column name if different
                    break;
                case "Patient Invoice":
                    tableName = "tblManagePatient";
                    columnName = "PatientID";
                    nameColumn = "Name"; // Update column name if different
                    break;
                case "Supplier Invoice":
                    tableName = "tblManageSuppliers";
                    columnName = "SupplierID";
                    nameColumn = "SupplierName"; // Update column name if different
                    break;
                default:
                    MessageBox.Show("Please select a valid Invoice Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
            }

            // Validate the ID exists in the selected table and fetch the name
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = $"SELECT {nameColumn} FROM {tableName} WHERE {columnName} = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtChangeID.Text);
                        var name = cmd.ExecuteScalar();

                        if (name != null)
                        {
                            MessageBox.Show($"{lblChangeID.Text} found! Name: {name.ToString()}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"No record found with the given {lblChangeID.Text}.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding {lblChangeID.Text}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
