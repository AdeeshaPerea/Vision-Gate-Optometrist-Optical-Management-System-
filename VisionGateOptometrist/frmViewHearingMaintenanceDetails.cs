using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewHearingMaintenanceDetails : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewHearingMaintenanceDetails()
        {
            InitializeComponent();
        }

        private void frmViewHearingMaintenanceDetails_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            LoadHearingMaintenanceDetails();
        }

        private void LoadHearingMaintenanceDetails()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageHearingMaintenance";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable hearingTable = new DataTable();
                            adapter.Fill(hearingTable);

                            dataGridView1.DataSource = hearingTable;

                            CustomizeDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading hearing maintenance details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.RowTemplate.Height = 50;
            dataGridView1.BackgroundColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmBranchManagerDashBoard go = new frmBranchManagerDashBoard();
            this.Hide();
            go.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
