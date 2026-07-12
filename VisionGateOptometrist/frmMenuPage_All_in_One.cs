using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Data.SqlClient;

namespace VisionGateOptometrist
{
    public partial class frmMenuPage_All_in_One : Form
    {

        // Connection string should be inside the class
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";


        public frmMenuPage_All_in_One()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(frmMenuPage_All_in_One_Paint);

            // Set the background color of the form
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5"); // Background color

            

        }

        // New method for painting the form with a gradient background
        private void frmMenuPage_All_in_One_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmMenuPage_All_in_One_Load(object sender, EventArgs e)
        {

            // Set the background color of the panel
            pnlAllInOne.BackColor = ColorTranslator.FromHtml("#F0F0F0"); // Panel color
            pnlAllInOne2.BackColor = ColorTranslator.FromHtml("#F0F0F0"); // Panel color

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;
            pnlAllInOne2.Visible = false;
            pnlPassword.Visible = false;


            // Set the form to preview keypress events
            this.KeyPreview = true;

        }

        private void picEmployee_Click(object sender, EventArgs e)
        {

        }

        private void pnlMenuPage_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picCreateProfele_Click(object sender, EventArgs e)
        {
            
        }

        private void lblCreateProfile_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for the Owner
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Add Employee form
                                frmAddEmployee addEmployeeForm = new frmAddEmployee();
                                addEmployeeForm.Show();
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
            };
        } 
        private void picCustomerInq_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 5 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Patient Inquiries form
                                frmManagePatientInquiries patientInquiriesForm = new frmManagePatientInquiries();
                                patientInquiriesForm.Show();
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
            };
        }

        private void lblCustomerInq_Click(object sender, EventArgs e)
        {

            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 5 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Patient Inquiries form
                                frmManagePatientInquiries go = new frmManagePatientInquiries();
                                go.Show();
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
            };
        }

        private void picDoctorChanneling_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 9 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Channelling form
                                frmManageChannelling manageChannellingForm = new frmManageChannelling();
                                manageChannellingForm.Show();
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
            };

        }

        private void lblDoctorChanneling_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 9 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Channelling form
                                frmManageChannelling manageChannellingForm = new frmManageChannelling();
                                manageChannellingForm.Show();
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
            };
        }

        private void picSupplierDetails_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 5 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Supplier Information form
                                frmManageSupplierInformation manageSupplierInfoForm = new frmManageSupplierInformation();
                                manageSupplierInfoForm.Show();
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
            };
        }

        private void lblSupplierDetails_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 5 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Supplier Information form
                                frmManageSupplierInformation manageSupplierInfoForm = new frmManageSupplierInformation();
                                manageSupplierInfoForm.Show();
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
            };
        }

        private void picProductandRefundExchange_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Charity Schedule form
                                frmManageCharitySchedule charityScheduleForm = new frmManageCharitySchedule();
                                charityScheduleForm.Show();
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
            };
        }

        private void lblProductExchange_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Charity Schedule form
                                frmManageCharitySchedule charityScheduleForm = new frmManageCharitySchedule();
                                charityScheduleForm.Show();
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
            };
        }

        private void lblProductExchange_2_Click(object sender, EventArgs e)
        {
            //
        }

        private void picAssesmens_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for the Owner
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Add Employee form
                                frmAddEmployee addEmployeeForm = new frmAddEmployee();
                                addEmployeeForm.Show();
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
            };
        }

        private void lblHearingMaintennace_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Maintenance Details form
                                frmManageHearingMaintenenanceDetails hearingMaintenanceForm = new frmManageHearingMaintenenanceDetails();
                                hearingMaintenanceForm.Show();
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
            };
        }

        private void picHearingMaintenance_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Maintenance Details form
                                frmManageHearingMaintenenanceDetails hearingMaintenanceForm = new frmManageHearingMaintenenanceDetails();
                                hearingMaintenanceForm.Show();
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
            };
        }

        private void picEmpDetails_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 1 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage User Accounts form
                                frmManageUserAccounts userAccountsForm = new frmManageUserAccounts();
                                userAccountsForm.Show();
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
            };
        }

        private void lblEmpDetails_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 1 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage User Accounts form
                                frmManageUserAccounts userAccountsForm = new frmManageUserAccounts();
                                userAccountsForm.Show();
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
            };
        }

        

        private void picHearingMaintenance_2_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Maintenance Details form
                                frmManageHearingMaintenenanceDetails hearingMaintenanceForm = new frmManageHearingMaintenenanceDetails();
                                hearingMaintenanceForm.Show();
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
            };
        }

        private void picHearingTestResults_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Test Results form
                                frmManageHearingTestResults hearingTestResultsForm = new frmManageHearingTestResults();
                                hearingTestResultsForm.Show();
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
            };
        }

        private void picHearingTestResults_2_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Test Results form
                                frmManageHearingTestResults hearingTestResultsForm = new frmManageHearingTestResults();
                                hearingTestResultsForm.Show();
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
            };
        }

        private void lblHearingTestResults_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Hearing Test Results form
                                frmManageHearingTestResults hearingTestResultsForm = new frmManageHearingTestResults();
                                hearingTestResultsForm.Show();
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
            };
        }

        private void picEarGuidance_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Ear Guidance form
                                frmManageEarGuidance earGuidanceForm = new frmManageEarGuidance();
                                earGuidanceForm.Show();
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
            };
        }

        private void picEarGuidance_2_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Ear Guidance form
                                frmManageEarGuidance earGuidanceForm = new frmManageEarGuidance();
                                earGuidanceForm.Show();
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
            };
        }

        private void lblEarGuidance_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 7 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Ear Guidance form
                                frmManageEarGuidance earGuidanceForm = new frmManageEarGuidance();
                                earGuidanceForm.Show();
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
            };
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 6 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Eye Guidance form
                                frmManageEyeGuidance eyeGuidanceForm = new frmManageEyeGuidance();
                                eyeGuidanceForm.Show();
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
            };
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 6 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Eye Guidance form
                                frmManageEyeGuidance eyeGuidanceForm = new frmManageEyeGuidance();
                                eyeGuidanceForm.Show();
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
            };
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 6 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Eye Guidance form
                                frmManageEyeGuidance eyeGuidanceForm = new frmManageEyeGuidance();
                                eyeGuidanceForm.Show();
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
            };
        }

        private void picVisionTestResults_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 6 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Vision Test Results form
                                frmManageVisionTestResults visionTestResultsForm = new frmManageVisionTestResults();
                                visionTestResultsForm.Show();
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
            };
        }

        private void lblVisionTestResults_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 6 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Vision Test Results form
                                frmManageVisionTestResults visionTestResultsForm = new frmManageVisionTestResults();
                                visionTestResultsForm.Show();
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
            };
        }

        
        private void picPrescription_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 9 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Invoice form
                                frmManageInvoice manageInvoiceForm = new frmManageInvoice();
                                manageInvoiceForm.Show();
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
            };
        }

        private void lblPresriptiion_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 9 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Invoice form
                                frmManageInvoice manageInvoiceForm = new frmManageInvoice();
                                manageInvoiceForm.Show();
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
            };
        }

        private void picMakePayments_Click(object sender, EventArgs e)
        {
            frmManagePatientPayment go = new frmManagePatientPayment();
            this.Hide();
            go.Show();

            //------------------------------------------------------------------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        private void lblMakePayments_Click(object sender, EventArgs e)
        {
            frmManagePatientPayment go = new frmManagePatientPayment();
            this.Hide();
            go.Show();

            //------------------------------------------------------------------------<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Employee Payment form
                                frmManageEmployeePayment employeePaymentForm = new frmManageEmployeePayment();
                                employeePaymentForm.Show();
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
            };
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = "SELECT COUNT(*) FROM tblManageUserAccount WHERE id = 2 AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Employee Payment form
                                frmManageEmployeePayment employeePaymentForm = new frmManageEmployeePayment();
                                employeePaymentForm.Show();
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
            };
        }

        private void picOrderRecords_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id IN (1, 2, 3, 4, 5, 6, 7, 8, 9) 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Orders form
                                frmManageOrders ordersForm = new frmManageOrders();
                                ordersForm.Show();
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
            };
        }

        private void lblOrderRecords_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id IN (1, 2, 3, 4, 5, 6, 7, 8, 9) 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Orders form
                                frmManageOrders ordersForm = new frmManageOrders();
                                ordersForm.Show();
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
            };
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id IN (1, 2, 3, 4, 5, 6, 7, 8, 9) 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Orders form
                                frmManageOrders ordersForm = new frmManageOrders();
                                ordersForm.Show();
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
            };
        }

        private void label18_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id IN (3, 4) 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Report form
                                frmManageReport reportForm = new frmManageReport();
                                reportForm.Show();
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
            };
        }

        private void picPromotions_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 2 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Promotion form
                                frmManagePromotion promotionForm = new frmManagePromotion();
                                promotionForm.Show();
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
            };
        }

        private void lblPromotions_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 2 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Promotion form
                                frmManagePromotion promotionForm = new frmManagePromotion();
                                promotionForm.Show();
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
            };
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 4 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Supplier Payment form
                                frmManageSupplierPayment supplierPaymentForm = new frmManageSupplierPayment();
                                supplierPaymentForm.Show();
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
            };
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 4 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Supplier Payment form
                                frmManageSupplierPayment supplierPaymentForm = new frmManageSupplierPayment();
                                supplierPaymentForm.Show();
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
            };
        }

        private void picPickupOrders_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 5 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Stock form
                                frmManageStock stockForm = new frmManageStock();
                                stockForm.Show();
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
            };
        }

        private void lblPickOrders_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 5 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Stock form
                                frmManageStock stockForm = new frmManageStock();
                                stockForm.Show();
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
            };
        }

        private void picSalesReports_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 3 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Revenue form
                                frmManageRevenue revenueForm = new frmManageRevenue();
                                revenueForm.Show();
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
            };
        }

        private void lblSalesReports_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 3 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Revenue form
                                frmManageRevenue revenueForm = new frmManageRevenue();
                                revenueForm.Show();
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
            };
        }

        private void pictureBox20_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 1 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the User Forgot Password form
                                frmManageUserForgotPassword forgotPasswordForm = new frmManageUserForgotPassword();
                                forgotPasswordForm.Show();
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
            };
        }

        private void label23_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 1 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the User Forgot Password form
                                frmManageUserForgotPassword forgotPasswordForm = new frmManageUserForgotPassword();
                                forgotPasswordForm.Show();
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
            };
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 4 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Branch Tasks form
                                frmManageBranchTasks branchTasksForm = new frmManageBranchTasks();
                                branchTasksForm.Show();
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
            };
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 4 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Manage Branch Tasks form
                                frmManageBranchTasks branchTasksForm = new frmManageBranchTasks();
                                branchTasksForm.Show();
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
            }; 
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 5 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Product Catalog form
                                frmProductCatelog productCatelogForm = new frmProductCatelog();
                                productCatelogForm.Show();
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
            };
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id = 5 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Product Catalog form
                                frmProductCatelog productCatelogForm = new frmProductCatelog();
                                productCatelogForm.Show();
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
            };
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            pnlAllInOne.Visible = false;
            pnlAllInOne2.Visible = true;
            guna2Button1.Visible = false;
            btnBack.Visible = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlAllInOne.Visible = true;
            pnlAllInOne2.Visible = false;
            guna2Button1.Visible = true;
            btnBack.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id BETWEEN 1 AND 9 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the System Feedback form
                                frmManageSystemFeedback feedbackForm = new frmManageSystemFeedback();
                                feedbackForm.Show();
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
            };
        }

        private void label7_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id BETWEEN 1 AND 9 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the System Feedback form
                                frmManageSystemFeedback feedbackForm = new frmManageSystemFeedback();
                                feedbackForm.Show();
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
            };
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id BETWEEN 1 AND 9 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Send Notification form
                                frmSendNotification notificationForm = new frmSendNotification();
                                notificationForm.Show();
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
            };
        }

        private void label6_Click(object sender, EventArgs e)
        {
            // Ensure the password panel is visible for validation
            pnlPassword.Visible = true;

            // Clear any previous password entry
            txtPassword.Clear();

            // Set focus to the password textbox
            txtPassword.Focus();

            // Attach event handler to the password button for this scenario
            btnPassword.Click += (s, args) =>
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
                        string query = @"
                    SELECT COUNT(*) 
                    FROM tblManageUserAccount 
                    WHERE id BETWEEN 1 AND 9 
                      AND [password] = @password";
                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                            int result = (int)cmd.ExecuteScalar();
                            if (result > 0)
                            {
                                // Password matched; open the Send Notification form
                                frmSendNotification notificationForm = new frmSendNotification();
                                notificationForm.Show();
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
            };
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            pnlPassword.Visible = false;
        }

        private void frmMenuPage_All_in_One_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back) // Check if the Backspace key is pressed
            {
                pnlPassword.Visible = false; // Hide the password panel
                e.Handled = true; // Mark the event as handled to prevent further processing
            }
        }

        private void pictreatmentsPlan_Click(object sender, EventArgs e)
        {
            // keep this as it is. dont remove this event

            //Treatment Schedule
        }

        private void lblTreatmentPlans_Click(object sender, EventArgs e)
        {
            // keep this as it is. dont remove this event

            //Treatment Schedule
        }

        private void pnlAllInOne2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void picFeedback_Click(object sender, EventArgs e)
        {
            frmProductCatelog go = new frmProductCatelog();
            this.Hide();
            go.Show();
            
        }
    }
}
