using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmLoginPage : Form
    {
        // Connection string to the database
        string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Property to set description text dynamically
        public string DescriptionText { get; set; }
        public bool ShowAdminTokenFields { get; set; } // Flag to show admin token fields
        public bool HideForgotPasswordLabel { get; set; } // Property to control visibility of lblForgotPassword



        public frmLoginPage()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(frmLoginPage_Paint);
        }

        // Paint event for gradient background
        private void frmLoginPage_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                ColorTranslator.FromHtml("#A2C1D6"), // Starting color
                ColorTranslator.FromHtml("#77A5FF"), // Ending color
                LinearGradientMode.Vertical)) // Gradient direction
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }
        }

        private void frmLoginPage_Load(object sender, EventArgs e)
        {
            // Hide the forgot password label if specified
            lblForgotPassword.Visible = !HideForgotPasswordLabel;

            pnlFotgotPassword.Visible = false;

            lbl_Info.Text = "Please click Inform if you’ve forgotten your\n password! You'll be contacted sooner.";
            
            // Set description text dynamically
            if (!string.IsNullOrWhiteSpace(DescriptionText))
            {
                lblDescription.Text = DescriptionText;
            }

            // Check if admin-specific fields should be visible
            lblAdminToken.Visible = ShowAdminTokenFields;
            txtAdminToken.Visible = ShowAdminTokenFields;

            this.FormBorderStyle = FormBorderStyle.None;

            pnlLoginform.BackColor = Color.FromArgb(120, 255, 255, 255);
            pnlLoginPageDescription.BackColor = Color.FromArgb(50, 255, 255, 255);
        }

        

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Go back to the open page
            frmOpenPage openPage = new frmOpenPage();
            openPage.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Don't remove this event
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Don't remove this event
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Don't remove this event
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Don't remove this event
        }

        private void btnNewLogin_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Validate admin token if the admin fields are visible
            if (ShowAdminTokenFields && string.IsNullOrWhiteSpace(txtAdminToken.Text))
            {
                MessageBox.Show("Admin Token cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAdminToken.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query;

                    // Query for regular user or admin based on admin field visibility
                    if (ShowAdminTokenFields)
                    {
                        query = "SELECT username FROM tblManageUserAccount WHERE username = @username AND password = @password AND adminToken = @adminToken";
                    }
                    else
                    {
                        query = "SELECT username FROM tblManageUserAccount WHERE username = @username AND password = @password";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                        if (ShowAdminTokenFields)
                        {
                            cmd.Parameters.AddWithValue("@adminToken", txtAdminToken.Text.Trim());
                        }

                        string role = Convert.ToString(cmd.ExecuteScalar());

                        if (!string.IsNullOrEmpty(role))
                        {
                            // Successful login
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Redirect based on role
                            if (ShowAdminTokenFields)
                            {
                                // Open Admin Dashboard
                                frmAdminDashboard adminDashBoard = new frmAdminDashboard();
                                adminDashBoard.Show();
                            }
                            else
                            {
                                // Redirect based on the user role
                                switch (role.ToLower())
                                {
                                    case "owner":
                                        new frmOwnerDashboard().Show();
                                        break;
                                    case "ownerassis":
                                        new frmOwnerAssistantDashboard().Show();
                                        break;
                                    case "bmanager":
                                        new frmBranchManagerDashBoard().Show();
                                        break;
                                    case "bassis":
                                        new frmBranchAssistantDashBoard().Show();
                                        break;
                                    case "opto":
                                        new frmOptometristDashBoard().Show();
                                        break;
                                    case "audio":
                                        new frmAudiologistDashBoard().Show();
                                        break;
                                    case "optician":
                                        new frmOpticianDashBoard().Show();
                                        break;
                                    case "cashier":
                                        new frmCashierDashBoard().Show();
                                        break;
                                    default:
                                        MessageBox.Show("Role not recognized. Please contact support.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        break;
                                }
                            }

                            this.Hide(); // Hide the login form after successful login
                        }
                        else
                        {
                            // Invalid credentials
                            MessageBox.Show("Invalid username, password, or admin token.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblForgotPassword_Click(object sender, EventArgs e)
        {
            pnlFotgotPassword.Visible = true;
            pnlFotgotPassword.BringToFront(); // Ensure the panel is displayed above all other controls
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlFotgotPassword.Visible = false;
        }

        private void btnInform_Click(object sender, EventArgs e)
        {
            // Validate if the name field is empty
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name cannot be empty. Please enter your name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            // Validate if the telephone field is empty or not numeric
            if (string.IsNullOrWhiteSpace(txtNumber.Text) || !long.TryParse(txtNumber.Text.Trim(), out _))
            {
                MessageBox.Show("Please enter a valid telephone number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumber.Focus();
                return;
            }

            try
            {
                // Connection string
                string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // SQL query to insert the name and telephone into the table
                    string query = "INSERT INTO tblRequestForgotPassword ([Name], [Telephone]) VALUES (@Name, @Telephone)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telephone", txtNumber.Text.Trim());

                        // Execute the query
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Your request has been submitted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear the text boxes after submission
                        txtName.Clear();
                        txtNumber.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
