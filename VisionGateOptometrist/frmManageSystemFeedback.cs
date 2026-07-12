using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageSystemFeedback : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSearchPerformed = false;

        public frmManageSystemFeedback()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageSystemFeedback_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Initialize ComboBox values
            txtStatus.Items.Add("Open");
            txtStatus.Items.Add("In Progress");
            txtStatus.Items.Add("Resolved");
            txtStatus.Items.Add("Closed");

            // Set default values
            txtDate.Value = DateTime.Now;

            // Generate the next SystemBugID
            GenerateNextSystemBugID();
        }

        private void pnlUserAcconts_Paint(object sender, PaintEventArgs e)
        {

        }

        private void GenerateNextSystemBugID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(SystemBugID), 0) + 1 FROM tblManageSystemFeedback";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtBugID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating System Bug ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid System Bug ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageSystemFeedback WHERE SystemBugID = @SystemBugID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SystemBugID", txtSearch.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtBugID.Text = reader["SystemBugID"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["LodgeDate"]);
                                txtStatus.SelectedItem = reader["Status"].ToString();
                                txtReason.Text = reader["Reason"].ToString();

                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given System Bug ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageSystemFeedback (LodgeDate, Status, Reason) VALUES (@LodgeDate, @Status, @Reason)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@LodgeDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Reason", txtReason.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Feedback submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSystemBugID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the System Bug ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageSystemFeedback SET LodgeDate = @LodgeDate, Status = @Status, Reason = @Reason WHERE SystemBugID = @SystemBugID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SystemBugID", txtBugID.Text);
                        cmd.Parameters.AddWithValue("@LodgeDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Status", txtStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Reason", txtReason.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Feedback updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the System Bug ID first before removing.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageSystemFeedback WHERE SystemBugID = @SystemBugID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@SystemBugID", txtBugID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Feedback removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextSystemBugID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing feedback: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtSearch.Clear();
            //txtBugID.Clear();
            txtDate.Value = DateTime.Now;
            txtStatus.SelectedIndex = -1;
            txtReason.Clear();
            isSearchPerformed = false;
        }

        private bool ValidateInputs()
        {
            if (txtStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStatus.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Reason is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReason.Focus();
                return false;
            }

            return true;
        }
    }
}
