using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewEmployee : Form
    {
        private Form previousForm; // Reference to the previous form (Owner Dashboard)
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmViewEmployee(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm; // Store the reference to the previous form
        }

        private void frmViewEmployee_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Load employee data into the DataGridView
            LoadEmployeeData();

            // Customize the DataGridView appearance
            CustomizeDataGridView();
        }

        private void LoadEmployeeData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT EmployeeID, Name, Type, Branch, ContactNo, Qualification, Position FROM tblManageEmployee";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable employeeTable = new DataTable();
                            adapter.Fill(employeeTable);

                            // Bind the data to the DataGridView
                            dataGridView1.DataSource = employeeTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employee data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

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

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            // Show the previous form when Back button is clicked
            if (previousForm != null)
            {
                previousForm.Show();
                this.Close(); // Close the current form
            }
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            frmOwnerDashboard go = new frmOwnerDashboard();
            this.Hide();
            go.Show();
        }
    }
}
