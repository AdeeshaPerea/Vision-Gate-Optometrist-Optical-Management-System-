using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmPatientInquries : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmPatientInquries()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmPatientInquries_Load(object sender, EventArgs e)
        {
            // Remove title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load Patient Inquiries into DataGridView
            LoadPatientInquiries();
        }

        private void LoadPatientInquiries()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch all Patient Inquiries
                    string query = "SELECT InquiryID, InquiryType, Description FROM tblPatientInquiries";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Bind the data to the DataGridView
                        dataGridView1.DataSource = dataTable;

                        // Apply customizations
                        CustomizeDataGridView();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient inquiries: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Remove row headers for a cleaner look
            dataGridView1.RowHeadersVisible = false;

            // Set the column widths to fill the DataGridView
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Set alternating row colors for better readability
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");

            // Adjust the row height for better visibility
            dataGridView1.RowTemplate.Height = 35;

            // Center-align header text for a professional appearance
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Set the header font to bold for emphasis
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

            // Set the header background color
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");

            // Apply default cell font and colors for readability
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            // Disable the visual styles for the header
            dataGridView1.EnableHeadersVisualStyles = false;

            // Set the background color of the DataGridView
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");
        }
    }
}
