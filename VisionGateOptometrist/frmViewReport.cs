using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Management;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewReport : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewReport()
        {
            InitializeComponent();
        }

        private void frmViewReport_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load data into the DataGridView
            LoadReportData();
        }

        private void LoadReportData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ReportID, Type, Name, Date, Description FROM tblManageReport";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable reportTable = new DataTable();
                            adapter.Fill(reportTable);

                            dataGridView1.DataSource = reportTable;

                            CustomizeDataGridView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading report data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.EnableHeadersVisualStyles = false;
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmOwnerDashboard go  = new frmOwnerDashboard();
            this.Hide();
            go.Show();
        }
    }
}
