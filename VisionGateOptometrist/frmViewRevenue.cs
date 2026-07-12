using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewRevenue : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewRevenue()
        {
            InitializeComponent();
            // Set the background color
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewRevenue_Load(object sender, EventArgs e)
        {
            // Remove title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load revenue records into DataGridView
            LoadRevenueRecords();

            // Customize the DataGridView
            CustomizeDataGridView();
        }

        private void LoadRevenueRecords()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch all revenue records
                    string query = "SELECT RevenueID, Amount, CONVERT(VARCHAR(10), Date, 103) AS Date, Description FROM tblManageRevenue";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading revenue records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Remove row headers
            dataGridView1.RowHeadersVisible = false;

            // Adjust column widths to fill the DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set alternating row colors
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");

            // Adjust row heights for better spacing
            dataGridView1.RowTemplate.Height = 40;

            // Center-align header text for better presentation
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set header font to bold for readability
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");

            // Set default cell style for better UI
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Disable visual styles for headers
            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmOwnerAssistantDashboard go = new frmOwnerAssistantDashboard();
            this.Hide();
            go.Show();
        }

        private void pnlLeftNavi_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
