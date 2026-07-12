using Guna.UI2.WinForms.Suite;
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
    public partial class frmManageBranchTasks : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManageBranchTasks()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageBranchTasks_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Generate the next TaskID
            GenerateNextTaskID();
        }

        private void GenerateNextTaskID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to fetch the next TaskID
                    string query = "SELECT ISNULL(MAX(TaskID), 0) + 1 FROM tblBranchTask";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtTaskID.Text = nextId.ToString(); // Display next TaskID
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating TaskID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid Task ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to search for the task by TaskID
                    string query = "SELECT TaskID, TaskName, Description FROM tblBranchTask WHERE TaskID = @TaskID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TaskID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate text boxes
                                txtTaskID.Text = reader["TaskID"].ToString();
                                txtTaskName.Text = reader["TaskName"].ToString();
                                txtDes.Text = reader["Description"].ToString();

                                // Mark search as successful
                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No task found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false; // Reset flag
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert a new task
                    string query = "INSERT INTO tblBranchTask (TaskName, Description) VALUES (@TaskName, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDes.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Task scheduled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextTaskID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error scheduling task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't clickable. Search the Task ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Update the task
                    string query = "UPDATE tblBranchTask SET TaskName = @TaskName, Description = @Description WHERE TaskID = @TaskID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TaskID", Convert.ToInt32(txtTaskID.Text));
                        cmd.Parameters.AddWithValue("@TaskName", txtTaskName.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDes.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Task updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextTaskID();
                        isSearchPerformed = false; // Reset flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't clickable. Search the Task ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Delete the task
                    string query = "DELETE FROM tblBranchTask WHERE TaskID = @TaskID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@TaskID", Convert.ToInt32(txtTaskID.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Task deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextTaskID();
                        isSearchPerformed = false; // Reset flag
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting task: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            // Clear all input fields except TaskID
            txtTaskName.Clear();
            txtDes.Clear();
            txtSearch.Clear();

            // Reset search flag
            isSearchPerformed = false;

            // Regenerate next TaskID
            GenerateNextTaskID();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Task Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDes.Text))
            {
                MessageBox.Show("Description cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

    }
}
