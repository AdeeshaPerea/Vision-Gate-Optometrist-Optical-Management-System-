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
    public partial class frmManageChannelling : Form
    {
        // Connection string for the database
        private string connectionString = @"Data Source=LAPTOP-S6UOBFRN\SQLEXPRESS; Initial Catalog=OSMS; Integrated Security=True";

        // Flag to track if a valid search was performed
        private bool isSearchPerformed = false;

        private bool isUpdatingDropdowns = false; // Prevent recursive updates

        public frmManageChannelling()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#EBF1F5");
        }

        // Method 1: frmManageChannelling_Load
        private void frmManageChannelling_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            //this.BackColor = ColorTranslator.FromHtml("#EBF1F5");

            // Keep buttons enabled but with validation
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;

            GenerateNextChannellingID();
            LoadChannelTypeAndDoctorData();
            LoadLatestChannellingNumber(); // Load the current Channelling Number
        }

        // Method 2: LoadLatestChannellingNumber
        private void LoadLatestChannellingNumber()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Query to get the latest ChannellingNo based on the most recent entry
                    string query = "SELECT TOP 1 ChannellingNo FROM tblManageChannelling ORDER BY ChannellingID DESC";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            lblCurrentNumber.Text = result.ToString(); // Set the label to the last ChannellingNo
                        }
                        else
                        {
                            lblCurrentNumber.Text = "0"; // Default value if the table is empty
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading the latest Channelling Number: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // Method 3: GenerateNextChannellingID
        private void GenerateNextChannellingID()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(ChannellingID), 0) + 1 FROM tblManageChannelling";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        int nextId = Convert.ToInt32(cmd.ExecuteScalar());
                        txtChannelID.Text = nextId.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Channelling ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method 4: LoadChannelTypeAndDoctorData
        private void LoadChannelTypeAndDoctorData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Load distinct Channeling Types
                    string query = "SELECT DISTINCT ChannelingType FROM tblDoctorChannelMapping";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            txtChannelType.Items.Clear();
                            while (reader.Read())
                            {
                                txtChannelType.Items.Add(reader["ChannelingType"].ToString());
                            }
                        }
                    }

                    // Load distinct Doctor Names
                    query = "SELECT DISTINCT DoctorName FROM tblDoctorChannelMapping";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            txtDoctorSelected.Items.Clear();
                            while (reader.Read())
                            {
                                txtDoctorSelected.Items.Add(reader["DoctorName"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dropdown data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method 5: ValidateInputs
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtChannelType.Text))
            {
                MessageBox.Show("Channelling Type is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChannelType.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDoctorSelected.Text))
            {
                MessageBox.Show("Selected Doctor is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDoctorSelected.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBranch.Text))
            {
                MessageBox.Show("Branch is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBranch.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChannelStatus.Text))
            {
                MessageBox.Show("Channelling Status is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChannelStatus.Focus();
                return false;
            }
            return true;
        }

        // Method 6: btnAdd_Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "INSERT INTO tblManageChannelling (Type, Doctor, Branch, Date, ChannellingNo, Status, Time) " +
                                   "VALUES (@Type, @Doctor, @Branch, @Date, @ChannellingNo, @Status, @Time)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Type", txtChannelType.Text);
                        cmd.Parameters.AddWithValue("@Doctor", txtDoctorSelected.Text);
                        cmd.Parameters.AddWithValue("@Branch", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@Date", txtScheduleDate.Value);

                        // Get the ChannellingNo from txtChannelNo (ensure this is populated correctly)
                        if (int.TryParse(txtChannelNo.Text, out int channelNumber))
                        {
                            cmd.Parameters.AddWithValue("@ChannellingNo", channelNumber); // Use the user-inputted value
                        }
                        else
                        {
                            MessageBox.Show("Invalid Channelling Number. Please check your input.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Exit if invalid
                        }

                        cmd.Parameters.AddWithValue("@Status", txtChannelStatus.Text);

                        // Handle Time field safely
                        if (DateTime.TryParse(txtSheduleTime.Text, out DateTime validTime))
                        {
                            cmd.Parameters.AddWithValue("@Time", validTime); // Use valid time
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Time", DBNull.Value); // If invalid, insert NULL
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Channelling added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Update the latest ChannellingNo after insertion
                        ClearFields();
                        GenerateNextChannellingID();
                        LoadLatestChannellingNumber(); // Refresh the latest number

                        // Enable Update and Delete buttons
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                        isSearchPerformed = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding Channelling: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        // Method 7: btnSearch_Click
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT ChannellingID, Type, Doctor, Branch, Date, ChannellingNo, Status, Time " +
                                   "FROM tblManageChannelling WHERE ChannellingID = @ChannellingID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChannellingID", txtSearch.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtChannelID.Text = reader["ChannellingID"].ToString();
                                txtChannelType.Text = reader["Type"].ToString();
                                txtDoctorSelected.Text = reader["Doctor"].ToString();
                                txtBranch.Text = reader["Branch"].ToString();
                                txtChannelNo.Text = reader["ChannellingNo"].ToString();
                                txtChannelStatus.Text = reader["Status"].ToString();

                                // Handle Date field safely
                                if (reader["Date"] != DBNull.Value)
                                {
                                    txtScheduleDate.Value = Convert.ToDateTime(reader["Date"]);
                                }

                                // Handle Time field safely
                                if (reader["Time"] != DBNull.Value)
                                {
                                    var timeValue = reader["Time"].ToString();  // Get time as string
                                    DateTime parsedTime;
                                    if (DateTime.TryParse(timeValue, out parsedTime))
                                    {
                                        txtSheduleTime.Value = parsedTime;  // Set time correctly
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid time format in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }

                                btnUpdate.Enabled = true;
                                btnDelete.Enabled = true;
                                isSearchPerformed = true;
                            }
                            else
                            {
                                MessageBox.Show("No Channelling found with the given ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                ClearFields();
                                isSearchPerformed = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching Channelling: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Method 8: btnUpdate_Click
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Update button isn't responding. Please search for the Channelling ID first!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE tblManageChannelling SET Type = @Type, Doctor = @Doctor, Branch = @Branch, " +
                                   "Date = @Date, ChannellingNo = @ChannellingNo, Status = @Status, Time = @Time WHERE ChannellingID = @ChannellingID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChannellingID", txtChannelID.Text);
                        cmd.Parameters.AddWithValue("@Type", txtChannelType.Text);
                        cmd.Parameters.AddWithValue("@Doctor", txtDoctorSelected.Text);
                        cmd.Parameters.AddWithValue("@Branch", txtBranch.Text);
                        cmd.Parameters.AddWithValue("@Date", txtScheduleDate.Value);
                        cmd.Parameters.AddWithValue("@ChannellingNo", txtChannelNo.Text);
                        cmd.Parameters.AddWithValue("@Status", txtChannelStatus.Text);

                        // Handle Time field safely
                        if (DateTime.TryParse(txtSheduleTime.Text, out DateTime validTime))
                        {
                            cmd.Parameters.AddWithValue("@Time", validTime); // Use valid time
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Time", DBNull.Value); // If invalid, insert NULL
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Channelling updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextChannellingID();
                        isSearchPerformed = false;
                        LoadLatestChannellingNumber(); // Refresh latest number here

                        // Enable Update and Delete buttons
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating Channelling: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Method 9: btnDelete_Click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!isSearchPerformed)
            {
                MessageBox.Show("Delete button isn't responding. Please search for the Channelling ID first!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this Channelling?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "DELETE FROM tblManageChannelling WHERE ChannellingID = @ChannellingID";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChannellingID", txtChannelID.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Channelling deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        GenerateNextChannellingID();
                        isSearchPerformed = false;
                        LoadLatestChannellingNumber(); // Refresh latest number here

                        // Enable Update and Delete buttons
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting Channelling: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Method 10: btnClear_Click
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            GenerateNextChannellingID();
            isSearchPerformed = false;

            LoadLatestChannellingNumber(); // Refresh latest number here
        }

        // Method 11: ClearFields
        private void ClearFields()
        {
            txtChannelID.Clear();
            txtChannelType.SelectedIndex = -1;
            txtDoctorSelected.SelectedIndex = -1;
            txtBranch.SelectedIndex = -1;
            txtScheduleDate.Value = DateTime.Now;
            txtSheduleTime.Value = DateTime.Now;
            txtChannelNo.Clear();
            txtChannelStatus.SelectedIndex = -1;
            txtSearch.Clear();
            txtChannelNo.Clear();

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        // Method 12: txtDoctorSelected_SelectedIndexChanged
        private void txtDoctorSelected_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDoctorSelected.Text) || isUpdatingDropdowns)
                return;

            try
            {
                isUpdatingDropdowns = true; // Prevent recursion

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Fetch all channeling types and relevant one for the selected doctor
                    string query = "SELECT DISTINCT ChannelingType FROM tblDoctorChannelMapping";
                    List<string> allChannelTypes = new List<string>();
                    string relevantChannelType = null;

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                allChannelTypes.Add(reader["ChannelingType"].ToString());
                            }
                        }
                    }

                    query = "SELECT ChannelingType FROM tblDoctorChannelMapping WHERE DoctorName = @DoctorName";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@DoctorName", txtDoctorSelected.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                relevantChannelType = reader["ChannelingType"].ToString();
                            }
                        }
                    }

                    // Populate Channel Type dropdown
                    txtChannelType.Items.Clear();
                    foreach (var type in allChannelTypes)
                    {
                        txtChannelType.Items.Add(type);
                    }

                    // Automatically select the relevant channeling type if available
                    if (!string.IsNullOrEmpty(relevantChannelType))
                    {
                        txtChannelType.SelectedIndex = txtChannelType.Items.IndexOf(relevantChannelType);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching channeling type for the selected doctor: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingDropdowns = false; // Reset the flag
            }
        }

        // Method 13: txtChannelType_SelectedIndexChanged
        private void txtChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChannelType.Text) || isUpdatingDropdowns)
                return;

            try
            {
                isUpdatingDropdowns = true; // Prevent recursion

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Fetch all doctor names and relevant ones for the selected channeling type
                    string query = "SELECT DISTINCT DoctorName FROM tblDoctorChannelMapping";
                    List<string> allDoctors = new List<string>();
                    List<string> relevantDoctors = new List<string>();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                allDoctors.Add(reader["DoctorName"].ToString());
                            }
                        }
                    }

                    query = "SELECT DoctorName FROM tblDoctorChannelMapping WHERE ChannelingType = @ChannelingType";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ChannelingType", txtChannelType.Text);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                relevantDoctors.Add(reader["DoctorName"].ToString());
                            }
                        }
                    }

                    // Populate Doctor dropdown
                    txtDoctorSelected.Items.Clear();
                    foreach (var doctor in allDoctors)
                    {
                        txtDoctorSelected.Items.Add(doctor);
                    }

                    // Automatically select the first relevant doctor if available
                    if (relevantDoctors.Count > 0)
                    {
                        txtDoctorSelected.SelectedIndex = txtDoctorSelected.Items.IndexOf(relevantDoctors[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching doctors for the selected channeling type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isUpdatingDropdowns = false; // Reset the flag
            }
        }

        // Method 14: comboBoxchantype_SelectedIndexChanged
        private void comboBoxchantype_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This method is already covered under 'txtChannelType_SelectedIndexChanged'
        }

        // Method 15: pnlStock_Paint
        private void pnlStock_Paint(object sender, PaintEventArgs e)
        {
            // Custom paint code can go here if needed
        }

        // Method 16: label7_Click
        private void label7_Click(object sender, EventArgs e)
        {
            // Handle any label click logic here
        }

        // Method 17: picBoxSupplierDetails_Click
        private void picBoxSupplierDetails_Click(object sender, EventArgs e)
        {
            // Handle picture box click logic here
        }

        // Method 18: pictureBox5_Click
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            // Handle another picture box click logic here
        }

        // Method 19: lblCurrentNumber_Click
        private void lblCurrentNumber_Click(object sender, EventArgs e)
        {
            // Handle label click logic here if needed
        }
    }
}
