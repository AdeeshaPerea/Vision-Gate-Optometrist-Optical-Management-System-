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
    public partial class frmProductCatelog : Form
    {
        private frmProductDetails currentProductDetailsForm = null;

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmProductCatelog()
        {
            InitializeComponent();
        }

        private void btnManage_Click_1(object sender, EventArgs e)
        {
            frmManageProducts manageProducts = new frmManageProducts();
            manageProducts.Show();
        }

        private void frmProductCatelog_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            flowProducts.Controls.Clear(); // Clear existing controls

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Product_ID, Product_Name, Product_Image, Product_Quantity FROM tblProducts";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(0); // Product_ID
                        string productName = reader.GetString(1); // Product_Name
                        byte[] productImageBytes = reader["Product_Image"] as byte[]; // Product_Image (binary data)
                        int productQuantity = reader.GetInt32(3); // Product_Quantity

                        // Create the product panel
                        Panel productPanel = new Panel
                        {
                            Width = 260,
                            Height = 260,
                            Margin = new Padding(40),
                            BackColor = Color.White,
                            BorderStyle = BorderStyle.FixedSingle,
                            Tag = productId // Use Product_ID as a tag to identify the product
                        };

                        // Create the PictureBox for the product image
                        PictureBox productPictureBox = new PictureBox
                        {
                            Width = 240,
                            Height = 180,
                            Top = 10,
                            Left = 10,
                            SizeMode = PictureBoxSizeMode.StretchImage,
                            Tag = productId
                        };

                        // If an image exists, set it; otherwise, use a default image
                        if (productImageBytes != null && productImageBytes.Length > 0)
                        {
                            productPictureBox.Image = ByteArrayToImage(productImageBytes);
                        }
                        else
                        {
                            productPictureBox.Image = Properties.Resources.no_image_icon_512x512_lfoanl0w; // Replace with your default image
                        }

                        // Create the label for the product name
                        Label productNameLabel = new Label
                        {
                            AutoSize = false,
                            Width = 240,
                            Height = 30,
                            Top = 200,
                            Left = 10,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = productName,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Tag = productId
                        };

                        // Create the label for the product quantity
                        Label productQuantityLabel = new Label
                        {
                            AutoSize = false,
                            Width = 240,
                            Height = 30,
                            Top = 230,
                            Left = 10,
                            TextAlign = ContentAlignment.MiddleCenter,
                            Text = "+" + productQuantity.ToString(),
                            Font = new Font("Segoe UI", 9, FontStyle.Regular),
                            ForeColor = Color.Green,
                            Tag = productId
                        };

                        // Add click event handlers
                        productPictureBox.Click += Product_Click;
                        productNameLabel.Click += Product_Click;
                        productQuantityLabel.Click += Product_Click;
                        productPanel.Click += Product_Click;

                        // Add controls to the panel
                        productPanel.Controls.Add(productPictureBox);
                        productPanel.Controls.Add(productNameLabel);
                        productPanel.Controls.Add(productQuantityLabel);

                        // Add the panel to the FlowLayoutPanel
                        flowProducts.Controls.Add(productPanel);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Product_Click(object sender, EventArgs e)
        {
            // Determine the source of the click and get the associated Panel
            Panel clickedPanel = sender as Panel ?? ((Control)sender).Parent as Panel;

            if (clickedPanel != null)
            {
                int productId = (int)clickedPanel.Tag;

                // Close the current form if it's already open
                if (currentProductDetailsForm != null)
                {
                    currentProductDetailsForm.Close();
                }

                // Open a new product details form
                currentProductDetailsForm = new frmProductDetails(productId);
                currentProductDetailsForm.FormClosed += CurrentProductDetailsForm_FormClosed; // Handle form close
                currentProductDetailsForm.Show();

                // Register a global click event to detect clicks outside
                this.MouseClick += Form_MouseClick;
            }
        }

        private void CurrentProductDetailsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            currentProductDetailsForm = null;
            this.MouseClick -= Form_MouseClick; // Remove the global click event
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (currentProductDetailsForm != null && !currentProductDetailsForm.Bounds.Contains(e.Location))
            {
                currentProductDetailsForm.Close();
                currentProductDetailsForm = null;
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void picMore_Click(object sender, EventArgs e)
        {
            frmLoginPage go = new frmLoginPage();
            this.Hide();
            go.Show();

        }

        private void picCalender_Click(object sender, EventArgs e)
        {

        }
    }
}
