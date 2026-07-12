using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewPatientPayment : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewPatientPayment()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewPatientPayment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            LoadPatientPayments();
        }

        private void LoadPatientPayments()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT PatientPaymentID, Date, Amount, Method, Status FROM tblManagePatientPayment";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable patientPaymentTable = new DataTable();
                            adapter.Fill(patientPaymentTable);

                            dataGridView1.DataSource = patientPaymentTable;
                            CustomizeGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient payments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeGrid()
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
            frmCashierDashBoard go = new frmCashierDashBoard();
            this.Hide();
            go.Show();
        }
    }
}
