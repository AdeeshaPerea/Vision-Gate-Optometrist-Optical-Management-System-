using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManagePromotion : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmManagePromotion()
        {
            InitializeComponent();
        }

        private void frmManagePromotion_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Populate Promotion Type dropdown
            PopulatePromotionTypeDropdown();

            // Generate the next Promotion ID
            GenerateNextPromotionID();
        }

        private void PopulatePromotionTypeDropdown()
        {
            // Populate the promotion type dropdown (example types)
            txtType.Items.Add("Discount");
            txtType.Items.Add("Offer");
            txtType.Items.Add("Special Deal");
            txtType.SelectedIndex = -1; // Default to no selection
        }

        private void GenerateNextPromotionID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(PromotionID), 0) + 1 FROM tblManagePromotion";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtPromotionID.Text = nextId.ToString(); // Auto-generate PromotionID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Promotion ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Validate Search Input
            if (string.IsNullOrWhiteSpace(txtPromotionID.Text))
            {
                MessageBox.Show("Please enter a valid Promotion ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPromotionID.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManagePromotion WHERE PromotionID = @PromotionID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PromotionID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Load data into respective fields
                                txtPromotionID.Text = reader["PromotionID"].ToString();
                                txtType.SelectedItem = reader["Type"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtDescription.Text = reader["Description"].ToString();

                                //MessageBox.Show("Record found!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given Promotion ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validate inputs before adding
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManagePromotion (Type, Date, Description) VALUES (@Type, @Date, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Type", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Promotion added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextPromotionID(); // Regenerate PromotionID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding promotion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPromotionID.Text))
            {
                MessageBox.Show("Update button not responding. Please search the Promotion ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate inputs before updating
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManagePromotion SET Type = @Type, Date = @Date, Description = @Description WHERE PromotionID = @PromotionID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PromotionID", txtPromotionID.Text);
                        cmd.Parameters.AddWithValue("@Type", txtType.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Promotion updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating promotion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPromotionID.Text))
            {
                MessageBox.Show("Delete button not responding. Please search the Promotion ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManagePromotion WHERE PromotionID = @PromotionID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@PromotionID", txtPromotionID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Promotion deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextPromotionID(); // Regenerate PromotionID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting promotion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtPromotionID.Clear();
            txtType.SelectedIndex = -1;
            txtDate.Value = DateTime.Now;
            txtDescription.Clear();
            GenerateNextPromotionID(); // Generate the next ID
        }

        private bool ValidateInputs()
        {
            if (txtType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a Promotion Type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Description is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescription.Focus();
                return false;
            }

            return true;
        }
    }
}
