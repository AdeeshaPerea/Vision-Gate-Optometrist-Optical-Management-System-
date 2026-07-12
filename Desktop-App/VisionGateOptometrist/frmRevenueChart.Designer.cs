namespace VisionGateOptometrist
{
    partial class frmRevenueChart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chartMonthlyRevenue = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRevenue = new Guna.UI2.WinForms.Guna2Button();
            this.pnlPassword = new Guna.UI2.WinForms.Guna2Panel();
            this.btnPassword = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMore = new System.Windows.Forms.PictureBox();
            this.picSetings = new System.Windows.Forms.PictureBox();
            this.picFeedback = new System.Windows.Forms.PictureBox();
            this.picEmployee = new System.Windows.Forms.PictureBox();
            this.picGraph = new System.Windows.Forms.PictureBox();
            this.picReports = new System.Windows.Forms.PictureBox();
            this.picRevunue = new System.Windows.Forms.PictureBox();
            this.picProducts = new System.Windows.Forms.PictureBox();
            this.picCalender = new System.Windows.Forms.PictureBox();
            this.picHome = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlyRevenue)).BeginInit();
            this.pnlPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSetings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReports)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRevunue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProducts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCalender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.picMore);
            this.panel1.Controls.Add(this.picSetings);
            this.panel1.Controls.Add(this.picFeedback);
            this.panel1.Controls.Add(this.picEmployee);
            this.panel1.Controls.Add(this.picGraph);
            this.panel1.Controls.Add(this.picReports);
            this.panel1.Controls.Add(this.picRevunue);
            this.panel1.Controls.Add(this.picProducts);
            this.panel1.Controls.Add(this.picCalender);
            this.panel1.Controls.Add(this.picHome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(94, 1033);
            this.panel1.TabIndex = 29;
            // 
            // chartMonthlyRevenue
            // 
            chartArea1.Name = "ChartArea1";
            this.chartMonthlyRevenue.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartMonthlyRevenue.Legends.Add(legend1);
            this.chartMonthlyRevenue.Location = new System.Drawing.Point(228, 185);
            this.chartMonthlyRevenue.Name = "chartMonthlyRevenue";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartMonthlyRevenue.Series.Add(series1);
            this.chartMonthlyRevenue.Size = new System.Drawing.Size(1700, 778);
            this.chartMonthlyRevenue.TabIndex = 30;
            this.chartMonthlyRevenue.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(926, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(321, 46);
            this.label1.TabIndex = 31;
            this.label1.Text = "Monthly Revenu";
            // 
            // btnRevenue
            // 
            this.btnRevenue.AutoRoundedCorners = true;
            this.btnRevenue.BorderRadius = 21;
            this.btnRevenue.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnRevenue.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnRevenue.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnRevenue.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnRevenue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevenue.ForeColor = System.Drawing.Color.White;
            this.btnRevenue.Location = new System.Drawing.Point(1695, 980);
            this.btnRevenue.Name = "btnRevenue";
            this.btnRevenue.Size = new System.Drawing.Size(233, 45);
            this.btnRevenue.TabIndex = 32;
            this.btnRevenue.Text = "Manage Revenue";
            this.btnRevenue.Click += new System.EventHandler(this.btnRevenue_Click);
            // 
            // pnlPassword
            // 
            this.pnlPassword.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pnlPassword.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlPassword.BorderThickness = 2;
            this.pnlPassword.Controls.Add(this.picClose);
            this.pnlPassword.Controls.Add(this.btnPassword);
            this.pnlPassword.Controls.Add(this.guna2HtmlLabel2);
            this.pnlPassword.Controls.Add(this.txtPassword);
            this.pnlPassword.Controls.Add(this.guna2HtmlLabel1);
            this.pnlPassword.Location = new System.Drawing.Point(786, 459);
            this.pnlPassword.Name = "pnlPassword";
            this.pnlPassword.Size = new System.Drawing.Size(509, 248);
            this.pnlPassword.TabIndex = 33;
            // 
            // btnPassword
            // 
            this.btnPassword.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPassword.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPassword.FillColor = System.Drawing.Color.SteelBlue;
            this.btnPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPassword.ForeColor = System.Drawing.Color.White;
            this.btnPassword.Location = new System.Drawing.Point(370, 162);
            this.btnPassword.Name = "btnPassword";
            this.btnPassword.Size = new System.Drawing.Size(103, 40);
            this.btnPassword.TabIndex = 34;
            this.btnPassword.Text = "Enter";
            this.btnPassword.Click += new System.EventHandler(this.btnPassword_Click);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Microsoft Tai Le", 16.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.SteelBlue;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(55, 36);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(395, 37);
            this.guna2HtmlLabel2.TabIndex = 2;
            this.guna2HtmlLabel2.Text = "Enter your password to access";
            // 
            // txtPassword
            // 
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtPassword.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtPassword.Location = new System.Drawing.Point(36, 162);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\0';
            this.txtPassword.PlaceholderText = "";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(314, 40);
            this.txtPassword.TabIndex = 1;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.DimGray;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(36, 125);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(107, 28);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Password";
            // 
            // picClose
            // 
            this.picClose.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._31148152;
            this.picClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picClose.Location = new System.Drawing.Point(476, 3);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(30, 34);
            this.picClose.TabIndex = 34;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picMore
            // 
            this.picMore.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.icon11;
            this.picMore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picMore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMore.Location = new System.Drawing.Point(15, 913);
            this.picMore.Name = "picMore";
            this.picMore.Size = new System.Drawing.Size(70, 70);
            this.picMore.TabIndex = 9;
            this.picMore.TabStop = false;
            // 
            // picSetings
            // 
            this.picSetings.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.White_Settings_Icon_removebg_preview;
            this.picSetings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picSetings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSetings.Location = new System.Drawing.Point(15, 808);
            this.picSetings.Name = "picSetings";
            this.picSetings.Size = new System.Drawing.Size(70, 70);
            this.picSetings.TabIndex = 8;
            this.picSetings.TabStop = false;
            // 
            // picFeedback
            // 
            this.picFeedback.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.images__1__removebg_preview;
            this.picFeedback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picFeedback.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFeedback.Location = new System.Drawing.Point(15, 706);
            this.picFeedback.Name = "picFeedback";
            this.picFeedback.Size = new System.Drawing.Size(70, 70);
            this.picFeedback.TabIndex = 7;
            this.picFeedback.TabStop = false;
            // 
            // picEmployee
            // 
            this.picEmployee.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._0766d183119ff92920403eb7ae566a85_removebg_preview;
            this.picEmployee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picEmployee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picEmployee.Location = new System.Drawing.Point(14, 610);
            this.picEmployee.Name = "picEmployee";
            this.picEmployee.Size = new System.Drawing.Size(70, 70);
            this.picEmployee.TabIndex = 6;
            this.picEmployee.TabStop = false;
            // 
            // picGraph
            // 
            this.picGraph.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._96742022_declining_graph_line_icon_white_icon_with_shadow_on_transparent_background_removebg_preview;
            this.picGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picGraph.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picGraph.Location = new System.Drawing.Point(14, 509);
            this.picGraph.Name = "picGraph";
            this.picGraph.Size = new System.Drawing.Size(70, 70);
            this.picGraph.TabIndex = 5;
            this.picGraph.TabStop = false;
            // 
            // picReports
            // 
            this.picReports.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._103144696_medical_history_or_report_paper_and_medical_cross_white_icon_with_shadow_on_transparent_background_removebg_preview;
            this.picReports.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picReports.Location = new System.Drawing.Point(14, 410);
            this.picReports.Name = "picReports";
            this.picReports.Size = new System.Drawing.Size(70, 70);
            this.picReports.TabIndex = 4;
            this.picReports.TabStop = false;
            // 
            // picRevunue
            // 
            this.picRevunue.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.istockphoto_1383837060_612x612_removebg_preview;
            this.picRevunue.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picRevunue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picRevunue.Location = new System.Drawing.Point(15, 311);
            this.picRevunue.Name = "picRevunue";
            this.picRevunue.Size = new System.Drawing.Size(70, 70);
            this.picRevunue.TabIndex = 3;
            this.picRevunue.TabStop = false;
            // 
            // picProducts
            // 
            this.picProducts.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.Package_box_512;
            this.picProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picProducts.Location = new System.Drawing.Point(15, 214);
            this.picProducts.Name = "picProducts";
            this.picProducts.Size = new System.Drawing.Size(70, 70);
            this.picProducts.TabIndex = 2;
            this.picProducts.TabStop = false;
            // 
            // picCalender
            // 
            this.picCalender.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.istockphoto_1373245842_612x612_removebg_preview1;
            this.picCalender.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picCalender.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCalender.Location = new System.Drawing.Point(15, 118);
            this.picCalender.Name = "picCalender";
            this.picCalender.Size = new System.Drawing.Size(70, 70);
            this.picCalender.TabIndex = 1;
            this.picCalender.TabStop = false;
            // 
            // picHome
            // 
            this.picHome.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._3d_home_icon_free_png__1__removebg_preview;
            this.picHome.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHome.Location = new System.Drawing.Point(15, 23);
            this.picHome.Name = "picHome";
            this.picHome.Size = new System.Drawing.Size(70, 70);
            this.picHome.TabIndex = 0;
            this.picHome.TabStop = false;
            // 
            // frmRevenueChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.pnlPassword);
            this.Controls.Add(this.btnRevenue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartMonthlyRevenue);
            this.Controls.Add(this.panel1);
            this.Name = "frmRevenueChart";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRevenueChart";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRevenueChart_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartMonthlyRevenue)).EndInit();
            this.pnlPassword.ResumeLayout(false);
            this.pnlPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSetings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFeedback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picReports)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRevunue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProducts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCalender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picMore;
        private System.Windows.Forms.PictureBox picSetings;
        private System.Windows.Forms.PictureBox picFeedback;
        private System.Windows.Forms.PictureBox picEmployee;
        private System.Windows.Forms.PictureBox picGraph;
        private System.Windows.Forms.PictureBox picReports;
        private System.Windows.Forms.PictureBox picRevunue;
        private System.Windows.Forms.PictureBox picProducts;
        private System.Windows.Forms.PictureBox picCalender;
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartMonthlyRevenue;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button btnRevenue;
        private Guna.UI2.WinForms.Guna2Panel pnlPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button btnPassword;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private System.Windows.Forms.PictureBox picClose;
    }
}