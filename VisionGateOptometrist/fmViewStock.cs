using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class fmViewStock : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public fmViewStock()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void fmViewStock_Load(object sender, EventArgs e)
        {
            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load stock data into the DataGridView
            LoadStockData();
        }

        private void LoadStockData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ProductID, ProductName, Price, Quantity, Availability, ExpireDate, Description FROM tblManageProduct";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable productTable = new DataTable();
                            adapter.Fill(productTable);

                            dataGridView1.DataSource = productTable;
                            CustomizeGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading stock data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeGrid()
        {
            // Hide row headers
            dataGridView1.RowHeadersVisible = false;

            // Set column headers appearance
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.EnableHeadersVisualStyles = false;

            // Adjust column width mode
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set alternating row styles
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            // Set font for rows
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Set consistent row height
            dataGridView1.RowTemplate.Height = 50;

            // Set grid background color
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");

            // Adjust grid line color
            dataGridView1.GridColor = Color.LightGray;

            // Set alignment for all cells
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Set specific column alignment (if needed)
            if (dataGridView1.Columns.Contains("Price") || dataGridView1.Columns.Contains("Quantity"))
            {
                dataGridView1.Columns["Price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["Quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Apply bold to specific column headers (if required)
            if (dataGridView1.Columns.Contains("ProductName"))
            {
                dataGridView1.Columns["ProductName"].HeaderCell.Style.Font = new Font("Arial", 14, FontStyle.Bold);
            }
        }


        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            // If this method is not used, don't remove. Keep it as it is.
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // If this method is not used, don't remove. Keep it as it is.
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmOwnerDashboard go = new frmOwnerDashboard();
            this.Hide();
            go.Show();
        }

        private void picMore_Click(object sender, EventArgs e)
        {

        }
    }
}
