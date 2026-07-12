using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewBranchTasks : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewBranchTasks()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewBranchTasks_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            LoadBranchTasks();
        }

        private void LoadBranchTasks()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT TaskID, TaskName, Description FROM tblBranchTask";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                        // Adjust column widths to fill the DataGridView
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Set font size for records
                        dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
                        dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                        // Adjust row height
                        dataGridView1.RowTemplate.Height = 35;

                        // Center-align column header text and set font
                        dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

                        // Set column header color to match interface
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
                        dataGridView1.EnableHeadersVisualStyles = false; // Apply the custom header color

                        // Optionally set alternating row colors for better readability
                        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
                        dataGridView1.RowsDefaultCellStyle.BackColor = Color.White;

                        // Adjust row heights for better spacing
                        dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branch tasks: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            //keep this as it is... dont remove.
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmLoginPage loginPage = new frmLoginPage();
            this.Hide();
            loginPage.Show();
        }

        private void picCalender_Click(object sender, EventArgs e)
        {

        }
    }
}
