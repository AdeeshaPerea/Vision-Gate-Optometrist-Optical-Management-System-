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
using System.Xml.Linq;

namespace VisionGateOptometrist
{
    public partial class frmManageCharitySchedule : Form
    {

        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        public frmManageCharitySchedule()
        {
            InitializeComponent();
            // Set the background color using the HEX code
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        private void frmManageCharitySchedule_Load(object sender, EventArgs e)
        {
            pnlUserAcconts.BackColor = Color.FromArgb(200, 240, 240, 240);
            this.FormBorderStyle = FormBorderStyle.None;

            // Generate the next CharityID
            GenerateNextCharityID();
        }

        private void GenerateNextCharityID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Get the next CharityID
                    string query = "SELECT ISNULL(MAX(CharityID), 0) + 1 FROM tblManageCharitySchedule";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtCharityScheduleID.Text = nextId.ToString(); // Set CharityID TextBox
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating CharityID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
                return; // Stop if validation fails
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Insert a new charity schedule
                    string query = "INSERT INTO tblManageCharitySchedule (Name, Date, Location, Description) " +
                                   "VALUES (@Name, @Date, @Location, @Description)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", txtCharityScheduleID.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDes.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Charity schedule added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextCharityID();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding charity schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || !int.TryParse(txtSearch.Text, out _))
            {
                MessageBox.Show("Please enter a valid Charity ID to search.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Search for a charity schedule by CharityID
                    string query = "SELECT CharityID, Name, Date, Location, Description FROM tblManageCharitySchedule WHERE CharityID = @CharityID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CharityID", Convert.ToInt32(txtSearch.Text));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Populate text boxes with the retrieved data
                                txtCharityScheduleID.Text = reader["CharityID"].ToString();
                                txtEventName.Text = reader["Name"].ToString();
                                txtDate.Value = Convert.ToDateTime(reader["Date"]);
                                txtLocation.Text = reader["Location"].ToString();
                                txtDes.Text = reader["Description"].ToString();

                                isSearchPerformed = true; // Mark that a successful search was performed
                            }
                            else
                            {
                                MessageBox.Show("No charity schedule found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false; // Reset the flag if search fails
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching for charity schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't clickable. Search the Charity ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
            {
                return; // Stop if validation fails
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Update the charity schedule
                    string query = "UPDATE tblManageCharitySchedule SET Name = @Name, Date = @Date, Location = @Location, Description = @Description " +
                                   "WHERE CharityID = @CharityID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CharityID", Convert.ToInt32(txtCharityScheduleID.Text));
                        cmd.Parameters.AddWithValue("@Name", txtEventName.Text);
                        cmd.Parameters.AddWithValue("@Date", txtDate.Value);
                        cmd.Parameters.AddWithValue("@Location", txtLocation.Text);
                        cmd.Parameters.AddWithValue("@Description", txtDes.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Charity schedule updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextCharityID();
                        isSearchPerformed = false; // Reset the flag after update
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating charity schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't clickable. Search the Charity ID first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Delete the charity schedule
                    string query = "DELETE FROM tblManageCharitySchedule WHERE CharityID = @CharityID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@CharityID", Convert.ToInt32(txtCharityScheduleID.Text));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Charity schedule deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        GenerateNextCharityID();
                        isSearchPerformed = false; // Reset the flag after deletion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting charity schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            // Clear all fields except CharityID
            txtEventName.Clear();
            txtLocation.Clear();
            txtDes.Clear();
            txtSearch.Clear();
            txtDate.Value = DateTime.Now;

            // Reset search flag
            isSearchPerformed = false;

            // Regenerate the next CharityID
            GenerateNextCharityID();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtEventName.Text))
            {
                MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtLocation.Text))
            {
                MessageBox.Show("Location cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
