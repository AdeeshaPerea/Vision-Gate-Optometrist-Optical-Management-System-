using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class ucDays : UserControl
    {
        private string _day, date;

        public ucDays(string day)
        {
            InitializeComponent();
            _day = day;
            label1.Text = day;

            // Hide the checkbox by default
            checkBox1.Hide();

            // Avoid setting an invalid date
            if (!string.IsNullOrEmpty(day) && int.TryParse(day, out int dayNumber))
            {
                try
                {
                    date = new DateTime(frmViewCalender._year, frmViewCalender._month, dayNumber).ToString("MM/dd/yyyy");
                    

                }
                catch (Exception)
                {
                    // Handle invalid date cases, if any
                }
            }
            // Call HighlightToday to style today's box
            HighlightToday();
        }

        private void HighlightToday()
        {
            try
            {
                // Compare the date of the current box with today's date
                if (!string.IsNullOrEmpty(date) && date == DateTime.Now.ToString("MM/dd/yyyy"))
                {
                    // Highlight the entire box with a light blue background
                    panel1.BackColor = Color.LightBlue;

                    // Optionally, add a border for extra visibility
                    this.BorderStyle = BorderStyle.FixedSingle;

                    // Make the label bold for emphasis
                    label1.Font = new Font(label1.Font, FontStyle.Bold);
                    label1.ForeColor = Color.Black; // Change text color for contrast
                }
                else
                {
                    // Reset to default if not today's date
                    this.BackColor = Color.White;
                    this.BorderStyle = BorderStyle.None;
                    label1.Font = new Font(label1.Font, FontStyle.Regular);
                    label1.ForeColor = Color.FromArgb(64, 64, 64);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions silently
            }
        }




        private void Sundays()
        {
            try
            {
                if (!string.IsNullOrEmpty(date)) // Only proceed if a valid date is set
                {
                    DateTime parsedDate = DateTime.Parse(date);
                    string weekday = parsedDate.ToString("ddd");

                    if (weekday == "Sun")
                    {
                        label1.ForeColor = Color.FromArgb(255, 128, 128); // Highlight Sundays
                    }
                    else
                    {
                        label1.ForeColor = Color.FromArgb(64, 64, 64); // Default color for weekdays
                    }
                }
            }
            catch (Exception)
            {
                // Log exception or notify if necessary
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            // Don't remove, keep this event as it is...
            checkBox1.Checked = !checkBox1.Checked;
            this.BackColor = checkBox1.Checked ? Color.FromArgb(255, 179, 79) : Color.White;
        }

        private void ucDays_Load(object sender, EventArgs e)
        {
            // Don't remove, keep this event as it is...
            Sundays();
        }
    }
}
