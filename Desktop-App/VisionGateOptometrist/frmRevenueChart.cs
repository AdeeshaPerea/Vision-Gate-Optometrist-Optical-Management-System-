using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace VisionGateOptometrist
{
    public partial class frmRevenueChart : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmRevenueChart()
        {
            InitializeComponent();
        }

        private void frmRevenueChart_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            LoadRevenueChart();
            pnlPassword.Visible = false;
        }

        private void LoadRevenueChart()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = @"
                        SELECT FORMAT([Date], 'MMMM') AS [Month], SUM([Amount]) AS TotalRevenue
                        FROM tblManageRevenue
                        GROUP BY FORMAT([Date], 'MMMM'), DATEPART(MONTH, [Date])
                        ORDER BY DATEPART(MONTH, [Date])";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        chartMonthlyRevenue.Series.Clear();
                        Series revenueSeries = new Series("RevenueSeries")
                        {
                            ChartType = SeriesChartType.Column,
                            IsValueShownAsLabel = true
                        };

                        chartMonthlyRevenue.Series.Add(revenueSeries);
                        while (reader.Read())
                        {
                            string month = reader["Month"].ToString();
                            decimal totalRevenue = Convert.ToDecimal(reader["TotalRevenue"]);
                            revenueSeries.Points.AddXY(month, totalRevenue);
                        }

                        // Adjust Axis Font Size and Style
                        chartMonthlyRevenue.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                        chartMonthlyRevenue.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", 12, FontStyle.Bold);
                        chartMonthlyRevenue.ChartAreas[0].AxisX.Title = "Month";
                        chartMonthlyRevenue.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 14, FontStyle.Bold);
                        chartMonthlyRevenue.ChartAreas[0].AxisY.Title = "Revenue (Rs.)";
                        chartMonthlyRevenue.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 14, FontStyle.Bold);

                        // Ensure X-axis labels are horizontal
                        chartMonthlyRevenue.ChartAreas[0].AxisX.LabelStyle.Angle = 0;

                        // Reduce Column Width and Align Left
                        revenueSeries["PixelPointWidth"] = "50"; // Adjust the width of each bar
                        chartMonthlyRevenue.ChartAreas[0].AxisX.ScaleView.Size = revenueSeries.Points.Count + 3;
                        chartMonthlyRevenue.ChartAreas[0].AxisX.Minimum = 0;
                        chartMonthlyRevenue.ChartAreas[0].AxisX.Maximum = revenueSeries.Points.Count + 3;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading revenue data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRevenue_Click(object sender, EventArgs e)
        {
            pnlPassword.Visible = true;
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 3 AND [password] = @password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        int result = (int)cmd.ExecuteScalar();
                        if (result > 0)
                        {
                            // Password matched; open the Manage Revenue form
                            frmManageRevenue manageRevenueForm = new frmManageRevenue();
                            manageRevenueForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtPassword.Clear();
                            txtPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while validating the password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            pnlPassword.Visible = false;
        }
    }
}
