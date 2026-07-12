using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewCharitySchedule : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewCharitySchedule()
        {
            InitializeComponent();
        }

        private void frmViewCharitySchedule_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load data into DataGridView
            LoadCharitySchedule();

            // Customize DataGridView appearance
            CustomizeDataGridView();
        }

        private void LoadCharitySchedule()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT CharityID, Name, Date, Location, Description FROM tblManageCharitySchedule";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable charityTable = new DataTable();
                            adapter.Fill(charityTable);

                            dataGridView1.DataSource = charityTable; // Bind data to DataGridView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading charity schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomizeDataGridView()
        {
            dataGridView1.RowHeadersVisible = false; // Remove row headers
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Fill columns

            // Header style
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#D1E8F2");
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Rows style
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F6F9FC");
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#A2C1D6");
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Grid line color and row height
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowTemplate.Height = 35;

            // Center-align columns
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            frmOwnerDashboard go = new frmOwnerDashboard();
            this.Hide();
            go.Show();
        }

        private void picCalender_Click(object sender, EventArgs e)
        {
            frmViewCalender calendarForm = new frmViewCalender(this); // Pass the current form as the callingForm
            calendarForm.Show();
            this.Hide(); // Optional: Hide the current form

        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmOwnerDashboard go = new frmOwnerDashboard();
            this.Hide();
            go.Show();
        }
    }
}
