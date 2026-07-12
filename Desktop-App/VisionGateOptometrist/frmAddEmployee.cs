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
    public partial class frmAddEmployee : Form
    {

        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";
        private bool isSearchPerformed = false;


        public frmAddEmployee()
        {
            InitializeComponent();

            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

        }

        private void FrmAddEmployee_Load(object sender, EventArgs e)
        {
            pnlManageEmployee.BackColor = Color.FromArgb(200, 240, 240, 240);

            // Remove the title bar and borders
            this.FormBorderStyle = FormBorderStyle.None;

            GenerateNextEmployeeID();

            // Attach the ValueChanged event
            txtDOB.ValueChanged += txtDOB_ValueChanged;


        }

        private void GenerateNextEmployeeID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(EmployeeID), 0) + 1 FROM tblManageEmployee";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtEmployeeID.Text = nextId.ToString(); // Display next EmployeeID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating EmployeeID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picBoxBackButton_Click(object sender, EventArgs e)
        {
            
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void picBoxSupplierDetails_Click(object sender, EventArgs e)
        {

        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBranch.Text))
            {
                MessageBox.Show("Branch is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBranch.Focus();
                return false;
            }
            if (!int.TryParse(txtContactNumber.Text, out _) || txtContactNumber.Text.Length < 10)
            {
                MessageBox.Show("Please enter a valid Contact Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactNumber.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid Email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            if (!int.TryParse(txtAge.Text, out int age) || age <= 0)
            {
                MessageBox.Show("Please enter a valid Age.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAge.Focus();
                return false;
            }
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Validate Employee ID input using txtSearch
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid numeric Employee ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT * FROM tblManageEmployee WHERE EmployeeID = @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Use txtSearch to get the EmployeeID
                        cmd.Parameters.AddWithValue("@EmployeeID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate fields with data from the database
                                txtEmployeeID.Text = reader["EmployeeID"].ToString();
                                txtType.Text = reader["Type"].ToString();
                                txtName.Text = reader["Name"].ToString();
                                txtBranch.Text = reader["Branch"].ToString();
                                txtContactNumber.Text = reader["ContactNo"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["AddedDate"]);
                                txtEmail.Text = reader["Email"].ToString();
                                txtPosition.Text = reader["Position"].ToString();
                                txtQualification.Text = reader["Qualification"].ToString();
                                txtDOB.Value = Convert.ToDateTime(reader["DOB"]);
                                txtAge.Text = reader["Age"].ToString();
                                txtDescription.Text = reader["Description"].ToString();

                                isSearchPerformed = true;
                                //MessageBox.Show("Employee data loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtSearch.Clear();
                            }
                            else
                            {
                                MessageBox.Show("No employee found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageEmployee (Type, Name, Branch, ContactNo, AddedDate, Email, Position, Qualification, DOB, Age, Description) " +
                                   "VALUES (@Type, @Name, @Branch, @ContactNo, @AddedDate, @Email, @Position, @Qualification, @DOB, @Age, @Description)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Type", txtType.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Branch", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNumber.Text);
                        cmd.Parameters.AddWithValue("@AddedDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Value);
                        cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextEmployeeID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Employee ID first before updating.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs()) return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageEmployee SET Type = @Type, Name = @Name, Branch = @Branch, ContactNo = @ContactNo, " +
                                   "AddedDate = @AddedDate, Email = @Email, Position = @Position, Qualification = @Qualification, " +
                                   "DOB = @DOB, Age = @Age, Description = @Description WHERE EmployeeID = @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
                        cmd.Parameters.AddWithValue("@Type", txtType.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@Branch", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@ContactNo", txtContactNumber.Text);
                        cmd.Parameters.AddWithValue("@AddedDate", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@Position", txtPosition.Text);
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                        cmd.Parameters.AddWithValue("@DOB", txtDOB.Value);
                        cmd.Parameters.AddWithValue("@Age", txtAge.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextEmployeeID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Search the Employee ID first before deleting.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageEmployee WHERE EmployeeID = @EmployeeID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@EmployeeID", txtEmployeeID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextEmployeeID();
                        isSearchPerformed = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            
            txtType.SelectedIndex = -1; // Reset ComboBox
            txtName.Clear();
            txtBranch.SelectedIndex = -1; // Reset ComboBox
            txtContactNumber.Clear();
            txtDate.Value = DateTime.Now;
            txtEmail.Clear();
            txtPosition.SelectedIndex = -1; // Reset ComboBox
            txtQualification.SelectedIndex = -1; // Reset ComboBox
            txtDOB.Value = DateTime.Now;
            txtAge.Clear();
            txtDescription.Clear();

            // Generate the next Employee ID
            GenerateNextEmployeeID();

        }

        private void txtQualification_TextChanged(object sender, EventArgs e)
        {

        }
        private void txtDOB_ValueChanged(object sender, EventArgs e)
        {
            CalculateAge();
        }

        private void CalculateAge()
        {
            DateTime selectedDate = txtDOB.Value;
            DateTime today = DateTime.Today;

            // Calculate the age
            int age = today.Year - selectedDate.Year;
            if (selectedDate > today.AddYears(-age)) age--;

            // Set the calculated age to the Age text box
            txtAge.Text = age.ToString();
        }

    }
}
