using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewEyeGuidance : Form
    {
        // Connection string to connect to your database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewEyeGuidance()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewEyeGuidance_Load(object sender, EventArgs e)
        {
            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            // Load data into DataGridView
            LoadEyeGuidanceData();
        }

        private void LoadEyeGuidanceData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    // Query to fetch data from the Eye Guidance table
                    string query = @"SELECT 
                                        PatientID, 
                                        PatientName, 
                                        Category, 
                                        Date, 
                                        Description 
                                     FROM tblManageEyeGuidance";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable eyeGuidanceTable = new DataTable();
                            adapter.Fill(eyeGuidanceTable); // Fill the DataTable with the fetched data

                            dataGridView1.DataSource = eyeGuidanceTable; // Bind the data to the DataGridView
                        }
                    }
                }

                // Customize DataGridView appearance
                CustomizeDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading eye guidance data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            // Remove unnecessary elements
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Adjust column properties
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");

            // Adjust row height
            dataGridView1.RowTemplate.Height = 35;

            // Set header appearance
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmLoginPage go = new frmLoginPage();
            this.Hide();
            go.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
