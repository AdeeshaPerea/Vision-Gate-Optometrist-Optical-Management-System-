using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmManageUserAccounts : Form
    {
        // Connection string for the database
        string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Boolean flag to track if search was performed
        private bool isSearchPerformed = false;

        public frmManageUserAccounts()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageUserAccounts_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            lblAdminToken.Visible = false;
            txtAdminToken.Visible = false;

            try
            {
                LoadNextId();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadNextId()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT ISNULL(MAX(id), 0) + 1 FROM tblManageUserAccount";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtid.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating next ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearTextBoxes()
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtdate.Text = DateTime.Now.ToShortDateString(); // Reset to current date
            txtTime.Text = DateTime.Now.ToShortTimeString(); // Reset to current time
            txtSearch.Clear(); // Clear the search box
            btnActivate.Visible = false; // Hide the activate button
            lblAdminToken.Visible = false; // Hide admin token field
            txtAdminToken.Visible = false;
        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "INSERT INTO tblManageUserAccount (username, password, loginDate, adminToken) " +
                                   "VALUES (@username, @password, @loginDate, @adminToken)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", txtusername.Text);
                        cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                        cmd.Parameters.AddWithValue("@loginDate", Convert.ToDateTime(txtdate.Text));
                        cmd.Parameters.AddWithValue("@adminToken", 0);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User account added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTextBoxes();
                        LoadNextId();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid User Account ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT id, username, password, loginDate, adminToken, isActive FROM tblManageUserAccount WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtid.Text = reader["id"].ToString();
                                txtusername.Text = reader["username"].ToString();
                                txtpassword.Text = reader["password"].ToString();
                                txtdate.Text = Convert.ToDateTime(reader["loginDate"]).ToShortDateString();
                                txtAdminToken.Text = reader["adminToken"].ToString();

                                lblAdminToken.Visible = txtid.Text == "1";
                                txtAdminToken.Visible = txtid.Text == "1";

                                bool isActive = Convert.ToBoolean(reader["isActive"]);
                                btnActivate.Visible = !isActive;

                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No user found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearTextBoxes();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't clickable. Search the User Account ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE tblManageUserAccount SET username = @username, password = @password, loginDate = @loginDate, adminToken = @adminToken WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));
                        cmd.Parameters.AddWithValue("@username", txtusername.Text);
                        cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                        cmd.Parameters.AddWithValue("@loginDate", Convert.ToDateTime(txtdate.Text));
                        cmd.Parameters.AddWithValue("@adminToken", Convert.ToInt32(txtAdminToken.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTextBoxes();
                        LoadNextId();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user account: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btndeactivate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Deactivate button not responding. Please search the User Account ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE tblManageUserAccount SET isActive = 0 WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User account deactivated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTextBoxes();
                        LoadNextId();
                        isSearchPerformed = false; // Reset the flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deactivating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtusername.Clear();
            txtpassword.Clear();
            txtdate.Text = DateTime.Now.ToShortDateString(); // Reset to current date
            txtTime.Text = DateTime.Now.ToShortTimeString(); // Reset to current time
            txtSearch.Clear(); // Clear the search box if necessary

            LoadNextId(); // Generate and display the next ID
            isSearchPerformed = false; // Reset the search flag
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Activate button not responding. Please search the User Account ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "UPDATE tblManageUserAccount SET isActive = 1 WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtid.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("User account activated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnActivate.Visible = false; // Hide the button after activation
                        isSearchPerformed = false; // Reset the flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error activating user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtusername.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
