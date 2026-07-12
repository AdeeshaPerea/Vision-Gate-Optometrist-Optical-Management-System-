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
    public partial class frmProductDetails : Form
    {

        private int productId; // Product ID passed from Form1
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        public frmProductDetails(int productId)
        {
            InitializeComponent();
            this.productId = productId; // Store the product ID
        }

        private void frmProductDetails_Load(object sender, EventArgs e)
        {
            LoadProductDetails();
        }

        private void LoadProductDetails()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Product_ID, Product_Name, Product_Price, Product_Description, Product_Quantity, Product_Image FROM tblProducts WHERE Product_ID = @ProductID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ProductID", productId);

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
                            picProduct.Image = ByteArrayToImage(imageBytes);
                        }
                        else
                        {
                            picProduct.Image = Properties.Resources.no_image_icon_512x512_lfoanl0w; // Replace with your default image resource
                        }
                    }
                    else
                    {
                        MessageBox.Show("Product details could not be loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close(); // Close the form if product details are not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading product details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close(); // Close the form if there's an error
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        // Close the form when clicking outside
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            this.Close(); // Close the form when it loses focus
        }
    }
}
