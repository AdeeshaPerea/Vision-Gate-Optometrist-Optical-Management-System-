using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewOrders : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private int selectedOrderId = 0; // Store the selected OrderID

        public frmViewOrders()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmViewOrders_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            LoadOrders();
            LoadOrders(); // Refresh grid after update
        }

        private void LoadOrders()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT OrderID, ProductID, ProductPrice, Quantity, Status, Date FROM tblManageOrders";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable orderTable = new DataTable();
                            adapter.Fill(orderTable);
                            dataGridView1.DataSource = orderTable;
                            CustomizeGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the click is not on the header row
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                selectedOrderId = Convert.ToInt32(row.Cells["OrderID"].Value); // Store the OrderID of the selected row
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            UpdateOrderStatus("Approved");
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            UpdateOrderStatus("Declined");
        }

        private void UpdateOrderStatus(string status)
        {
            if (selectedOrderId == 0)
            {
                MessageBox.Show("Please select a row first.", "No Row Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageOrders SET Status = @Status WHERE OrderID = @OrderID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@OrderID", selectedOrderId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Order status updated to {status} successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrders(); // Refresh grid after update
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating order status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
