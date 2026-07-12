using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageProducts : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        public frmManageProducts()
        {
            InitializeComponent();
        }

        private void frmManageProducts_Load(object sender, EventArgs e)
        {
            GenerateNextProductID();
        }

        private void GenerateNextProductID()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT ISNULL(MAX(Product_ID), 0) + 1 FROM tblProducts";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    int nextId = (int)cmd.ExecuteScalar();
                    txtID.Text = nextId.ToString();
                    txtID.ReadOnly = true; // Make ID field read-only
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error generating Product ID: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO tblProducts (Product_ID, Product_Name, Product_Description, Product_Price, Product_Quantity, Product_Image) 
                         VALUES (@ID, @Name, @Description, @Price, @Quantity, @Image)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);

                // Handle the Product_Image column
                if (btnUpload.Tag != null) // If an image is selected
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Image image = (Image)btnUpload.Tag;
                        image.Save(ms, image.RawFormat);
                        cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = ms.ToArray();
                    }
                }
                else // If no image is selected
                {
                    cmd.Parameters.Add("@Image", SqlDbType.VarBinary).Value = DBNull.Value;
                }

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear fields and regenerate Product ID
                    ClearFields();
                    GenerateNextProductID();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE tblProducts 
                                 SET Product_Name = @Name, Product_Description = @Description, Product_Price = @Price, 
                                     Product_Quantity = @Quantity, Product_Image = @Image 
                                 WHERE Product_ID = @ID";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@ID", txtID.Text);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQty.Text);

                if (btnUpload.Tag != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Image image = new Bitmap((Image)btnUpload.Tag); // Create a copy of the image
                        image.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Explicitly set the format
                        cmd.Parameters.AddWithValue("@Image", ms.ToArray());
                    }

                }
                else
                {
                    cmd.Parameters.AddWithValue("@Image", DBNull.Value);
                }


                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                    GenerateNextProductID();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Product Image",
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openFileDialog.FileName);
                btnUpload.Image = image;
                btnUpload.Tag = image; // Store the image in the Tag property
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchProduct(txtSearch.Text.Trim());
            }
        }

        private void SearchProduct(string searchText)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM tblProducts 
                                 WHERE Product_ID = @Search OR Product_Name LIKE '%' + @Search + '%'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Search", searchText);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtID.Text = reader["Product_ID"].ToString();
                        txtName.Text = reader["Product_Name"].ToString();
                        txtPrice.Text = reader["Product_Price"].ToString();
                        txtDescription.Text = reader["Product_Description"].ToString();
                        txtQty.Text = reader["Product_Quantity"].ToString();

                        if (reader["Product_Image"] != DBNull.Value)
                        {
                            byte[] imageBytes = (byte[])reader["Product_Image"];
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                Image image = Image.FromStream(ms);
                                btnUpload.Image = image;
                                btnUpload.Tag = image; // Store the image in the Tag property
                            }
                        }
                        else
                        {
                            btnUpload.Image = Properties.Resources.uploadsymbol_105008_ezgif_com_webp_to_png_converter; ;
                            btnUpload.Tag = null;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No product found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ClearFields();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error searching product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtQty.Clear();
            btnUpload.Image = Properties.Resources.uploadsymbol_105008_ezgif_com_webp_to_png_converter; // Replace 'DefaultImage' with the actual name of your default image resource
            btnUpload.Tag = null; // Clear the Tag property
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all input fields
            txtID.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtQty.Clear();

            // Reset the image on btnUpload to its default
            btnUpload.Image = Properties.Resources.uploadsymbol_105008_ezgif_com_webp_to_png_converter; // Replace with the correct default image resource name
            btnUpload.Tag = null; // Clear any stored image data in the Tag property

            // Regenerate the next Product ID
            GenerateNextProductID();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validate that at least two fields (including ID) are not empty
            int nonEmptyFieldsCount = 0;
            if (!string.IsNullOrEmpty(txtID.Text)) nonEmptyFieldsCount++; // ID must not be empty
            if (!string.IsNullOrEmpty(txtName.Text)) nonEmptyFieldsCount++;
            if (!string.IsNullOrEmpty(txtPrice.Text)) nonEmptyFieldsCount++;
            if (!string.IsNullOrEmpty(txtQty.Text)) nonEmptyFieldsCount++;
            if (!string.IsNullOrEmpty(txtDescription.Text)) nonEmptyFieldsCount++;

            if (nonEmptyFieldsCount < 2)
            {
                MessageBox.Show("Please search for a product first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirm deletion
            DialogResult confirmResult = MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM tblProducts WHERE Product_ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", txtID.Text);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Product deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Clear fields after deletion
                            ClearFields();

                            // Regenerate the next Product ID
                            GenerateNextProductID();
                        }
                        else
                        {
                            MessageBox.Show("No product found with the given ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

    }
}
