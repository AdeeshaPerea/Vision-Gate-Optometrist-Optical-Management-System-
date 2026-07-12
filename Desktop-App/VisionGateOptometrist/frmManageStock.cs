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
    public partial class frmManageStock : Form
    {

        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManageStock()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageStock_Load(object sender, EventArgs e)
        {
            
        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            
        }

        private void picBoxBackButton_Click_1(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One goBack = new frmMenuPage_All_in_One();
            goBack.Show();
            this.Close();
        }

        private void frmManageStock_Load_1(object sender, EventArgs e)
        {
            pnlStock.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Disable Update and Delete buttons initially
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            // Generate the next ProductID
            GenerateNextProductID();
        }

        private void GenerateNextProductID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(ProductID), 0) + 1 FROM tblManageProduct";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtProductID.Text = nextId.ToString(); // Display next ProductID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating ProductID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductName.Focus();
                return false;
            }
            if (!int.TryParse(txtPrice.Text, out int price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for Price.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }
            if (!int.TryParse(txtQty.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid positive number for Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Focus();
                return false;
            }
            if (txtAvailability.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an availability status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAvailability.Focus();
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ProductID, ProductName, Price, Quantity, ExpireDate, Availability, Description FROM tblManageProduct WHERE ProductID = @ProductID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtProductID.Text = reader["ProductID"].ToString();
                                txtProductName.Text = reader["ProductName"].ToString();
                                txtPrice.Text = reader["Price"].ToString();
                                txtQty.Text = reader["Quantity"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["ExpireDate"]);
                                txtAvailability.Text = reader["Availability"].ToString();
                                txtDescription.Text = reader["Description"].ToString();

                                btnUpdate.Enabled = true;
                                btnDelete.Enabled = true;

                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No product found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return; // Exit if validation fails
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageProduct (ProductName, Price, Quantity, ExpireDate, Availability, Description) VALUES (@ProductName, @Price, @Quantity, @ExpireDate, @Availability, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@Price", int.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtQty.Text));
                        cmd.Parameters.AddWithValue("@ExpireDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Availability", txtAvailability.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextProductID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't clickable. Search the Product ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
            {
                return; // Exit if validation fails
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageProduct SET ProductName = @ProductName, Price = @Price, Quantity = @Quantity, ExpireDate = @ExpireDate, Availability = @Availability, Description = @Description WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(txtProductID.Text));
                        cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                        cmd.Parameters.AddWithValue("@Price", int.Parse(txtPrice.Text));
                        cmd.Parameters.AddWithValue("@Quantity", int.Parse(txtQty.Text));
                        cmd.Parameters.AddWithValue("@ExpireDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Availability", txtAvailability.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextProductID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't clickable. Search the Product ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageProduct WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", Convert.ToInt32(txtProductID.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextProductID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtQty.Clear();
            txtAvailability.SelectedIndex = -1;
            txtDescription.Clear();
            txtSearch.Clear();

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            GenerateNextProductID();
        }
    }
}
