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
    public partial class frmManageOrders : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmManageOrders()
        {
            InitializeComponent();
        }

        private void frmManageOrders_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Populate Dropdowns
            PopulateQuantityDropdown();
            PopulateOrderStatusDropdown();

            // Generate Next Order ID
            GenerateNextOrderID();
        }

        private void PopulateQuantityDropdown()
        {
            for (int i = 1; i <= 20; i++)
            {
                txtQuantity.Items.Add(i.ToString());
            }
        }

        private void PopulateOrderStatusDropdown()
        {
            txtStatus.Items.Add("Pending");
            txtStatus.Items.Add("Completed");
            txtStatus.Items.Add("Cancelled");
        }

        private void GenerateNextOrderID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(OrderID), 0) + 1 FROM tblManageOrders";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtOrderID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Order ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text) || !int.TryParse(txtProductID.Text, out _))
            {
                MessageBox.Show("Please enter a valid Product ID.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT Price FROM tblManageProduct WHERE ProductID = @ProductID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                        var price = cmd.ExecuteScalar();

                        if (price != null)
                        {
                            txtProductPrice.Text = price.ToString();
                            MessageBox.Show($"Product ID found! Price: {price}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No product found with the given Product ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtProductPrice.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    string query = "INSERT INTO tblManageOrders (ProductID, ProductPrice, Quantity, Status, Date) VALUES (@ProductID, @ProductPrice, @Quantity, @Status, @Date)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                        cmd.Parameters.AddWithValue("@ProductPrice", txtProductPrice.Text);
                        cmd.Parameters.AddWithValue("@Quantity", txtQuantity.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextOrderID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderID.Text) || string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Update button not responding. Please search the Order ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageOrders SET ProductID = @ProductID, ProductPrice = @ProductPrice, " +
                                   "Quantity = @Quantity, Status = @Status, Date = @Date WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        cmd.Parameters.AddWithValue("@ProductID", txtProductID.Text);
                        cmd.Parameters.AddWithValue("@ProductPrice", txtProductPrice.Text);
                        cmd.Parameters.AddWithValue("@Quantity", txtQuantity.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtOrderID.Text) || string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Delete button not responding. Please search the Order ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageOrders WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", txtOrderID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextOrderID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtOrderID.Clear();
            txtProductID.Clear();
            txtProductPrice.Clear();
            txtQuantity.SelectedIndex = -1;
            txtStatus.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            GenerateNextOrderID();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtProductID.Text))
            {
                MessageBox.Show("Product ID is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtProductPrice.Text) || !decimal.TryParse(txtProductPrice.Text, out _))
            {
                MessageBox.Show("Product Price must be a valid number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProductPrice.Focus();
                return false;
            }

            if (txtQuantity.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Quantity.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            if (txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an Order Status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStatus.Focus();
                return false;
            }

            return true;
        }

        private void pnlStock_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid Order ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT OrderID, ProductID, ProductPrice, Quantity, Status, Date FROM tblManageOrders WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load data into respective fields
                                txtOrderID.Text = reader["OrderID"].ToString();
                                txtProductID.Text = reader["ProductID"].ToString();
                                txtProductPrice.Text = reader["ProductPrice"].ToString();
                                txtQuantity.SelectedItem = reader["Quantity"].ToString();
                                txtStatus.SelectedItem = reader["Status"].ToString();
                                txtDate.Value = reader["Date"] != DBNull.Value ? Convert.ToDateTime(reader["Date"]) : DateTime.Now;

                                //MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Order ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
