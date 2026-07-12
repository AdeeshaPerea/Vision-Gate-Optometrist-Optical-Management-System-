using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageUserForgotPassword : Form
    {
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        public frmManageUserForgotPassword()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageUserForgotPassword_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Hide the password fields initially
            lblPassword.Visible = false;
            txtPassword.Visible = false;

            // Ensure Admin Token field is editable on load
            txtAdminToken.ReadOnly = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Please enter a valid User Account ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT id, username, password FROM tblManageUserAccount WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtSearch.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtUserAccountID.Text = reader["id"].ToString();
                                txtUsername.Text = reader["username"].ToString();
                                txtPassword.Tag = reader["password"].ToString(); // Store password in a hidden Tag property for later use

                                // Inform the admin to enter the token
                                MessageBox.Show("Enter the Admin Token and press Enter to make the password field visible.", "Admin Token Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No record found with the given User Account ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtAdminToken_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Validate the admin token
                if (txtAdminToken.Text == "1234")
                {
                    lblPassword.Visible = true;
                    txtPassword.Visible = true;

                    // Load the password stored in the Tag property
                    txtPassword.Text = txtPassword.Tag?.ToString() ?? string.Empty;

                    // Make Admin Token field read-only
                    txtAdminToken.ReadOnly = true;

                    MessageBox.Show("Admin Token validated. Password field is now visible.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblPassword.Visible = false;
                    txtPassword.Visible = false;

                    // Keep Admin Token field editable if validation fails
                    txtAdminToken.ReadOnly = false;

                    MessageBox.Show("Invalid Admin Token. Please try again.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Prevent further processing of the Enter key
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!lblPassword.Visible || !txtPassword.Visible)
            {
                MessageBox.Show("Please enter a valid Admin Token to update the password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtUserAccountID.Text) || string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields must be filled to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageUserAccount SET username = @username, password = @password WHERE id = @id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", txtUserAccountID.Text.Trim());
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User account updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtSearch.Clear();
            txtUserAccountID.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtAdminToken.Clear();

            // Hide the password fields
            lblPassword.Visible = false;
            txtPassword.Visible = false;

            // Make Admin Token field editable again
            txtAdminToken.ReadOnly = false;

            // Clear the stored password in the Tag property
            txtPassword.Tag = null;
        }
    }
}
