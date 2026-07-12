using System;
using System.Globalization;
using System.Windows.Forms;

namespace VisionGateOptometrist
{
    public partial class frmViewCalender : Form
    {
        private Form previousForm; // Reference to the previous form
        private string role; // Add a field for the parameter

        public static int _year, _month;

        public frmViewCalender(Form callingForm)
        {
            InitializeComponent();
            previousForm = callingForm; // Store the reference to the previous form
        }

        private void frmViewCalender_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;

            // Show the current month and year initially
            showDays(DateTime.Now.Year, DateTime.Now.Month);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            // Keep this event as it is if not in use.
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _month--;
            if (_month < 1)
            {
                _month = 12;
                _year--;
            }
            showDays(_year, _month); // Refresh calendar for the previous month
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _month++;
            if (_month > 12)
            {
                _month = 1;
                _year++;
            }
            showDays(_year, _month); // Refresh calendar for the next month
        }

        private void picMore_Click(object sender, EventArgs e)
        {
            frmMenuPage_All_in_One go = new frmMenuPage_All_in_One();
            this.Hide();
            go.Show();
        }

        private void picHome_Click(object sender, EventArgs e)
        {
            if (previousForm != null)
            {
                previousForm.Show(); // Show the previous form
                this.Close();        // Close the current form
            }
        }

        private void showDays(int year, int month)
        {
            flowLayoutPanel1.Controls.Clear();

            // Set global variables
            _year = year;
            _month = month;

            // Display the month and year
            lblMonth.Text = $"{new DateTime(year, month, 1):MMMM yyyy}".ToUpper();

            // Get first day and days in the month
            DateTime firstDay = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int firstDayOfWeek = (int)firstDay.DayOfWeek;

            // Add blank boxes for days before the first day of the month
            for (int i = 0; i < firstDayOfWeek; i++)
            {
                ucDays blankDay = new ucDays("");
                flowLayoutPanel1.Controls.Add(blankDay);
            }

            // Add actual days of the month
            for (int i = 1; i <= daysInMonth; i++)
            {
                ucDays day = new ucDays(i.ToString());
                flowLayoutPanel1.Controls.Add(day);
            }

            // Fill remaining blank boxes to complete a 6x7 grid
            int totalBoxesUsed = firstDayOfWeek + daysInMonth;
            int remainingBoxes = 42 - totalBoxesUsed;
            for (int i = 0; i < remainingBoxes; i++)
            {
                ucDays blankDay = new ucDays("");
                flowLayoutPanel1.Controls.Add(blankDay);
            }
        }




    }
}
