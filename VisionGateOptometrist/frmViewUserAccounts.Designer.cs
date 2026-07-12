namespace VisionGateOptometrist
{
    partial class frmViewUserAccounts
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
            this.picHome = new System.Windows.Forms.PictureBox();
            this.picBoxBackButton = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.pnlUserAcconts = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetPassword = new Guna.UI2.WinForms.Guna2Button();
            this.txtGetpassword = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackButton)).BeginInit();
            this.pnlUserAcconts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.picHome.Click += new System.EventHandler(this.picHome_Click);
            // 
            // picBoxBackButton
            // 
            this.picBoxBackButton.BackgroundImage = global::VisionGateOptometrist.Properties.Resources.arrow1_png;
            this.picBoxBackButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picBoxBackButton.Location = new System.Drawing.Point(136, 35);
            this.picBoxBackButton.Name = "picBoxBackButton";
            this.picBoxBackButton.Size = new System.Drawing.Size(86, 84);
            this.picBoxBackButton.TabIndex = 68;
            this.picBoxBackButton.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnAdd.Location = new System.Drawing.Point(785, 767);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(176, 46);
            this.btnAdd.TabIndex = 88;
            this.btnAdd.Text = "Manage";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // pnlUserAcconts
            // 
            this.pnlUserAcconts.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlUserAcconts.Controls.Add(this.btnReload);
            this.pnlUserAcconts.Controls.Add(this.label2);
            this.pnlUserAcconts.Controls.Add(this.btnGetPassword);
            this.pnlUserAcconts.Controls.Add(this.txtGetpassword);
            this.pnlUserAcconts.Controls.Add(this.dataGridView1);
            this.pnlUserAcconts.Controls.Add(this.btnAdd);
            this.pnlUserAcconts.Controls.Add(this.label4);
            this.pnlUserAcconts.Controls.Add(this.btnSearch);
            this.pnlUserAcconts.Controls.Add(this.label1);
            this.pnlUserAcconts.Controls.Add(this.txtSearch);
            this.pnlUserAcconts.Location = new System.Drawing.Point(165, 118);
            this.pnlUserAcconts.Name = "pnlUserAcconts";
            this.pnlUserAcconts.Size = new System.Drawing.Size(1860, 857);
            this.pnlUserAcconts.TabIndex = 69;
            this.pnlUserAcconts.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlUserAcconts_Paint);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.SteelBlue;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.btnReload.Location = new System.Drawing.Point(1408, 767);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(176, 46);
            this.btnReload.TabIndex = 94;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 577);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(561, 25);
            this.label2.TabIndex = 92;
            this.label2.Text = "• Provide your Admin Token here to make the passwords visible";
            // 
            // btnGetPassword
            // 
            this.btnGetPassword.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnGetPassword.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnGetPassword.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnGetPassword.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnGetPassword.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetPassword.ForeColor = System.Drawing.Color.White;
            this.btnGetPassword.Location = new System.Drawing.Point(377, 633);
            this.btnGetPassword.Name = "btnGetPassword";
            this.btnGetPassword.Size = new System.Drawing.Size(199, 45);
            this.btnGetPassword.TabIndex = 91;
            this.btnGetPassword.Text = "Get Passwords";
            this.btnGetPassword.Click += new System.EventHandler(this.btnGetPassword_Click);
            // 
            // txtGetpassword
            // 
            this.txtGetpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGetpassword.Location = new System.Drawing.Point(45, 640);
            this.txtGetpassword.Name = "txtGetpassword";
            this.txtGetpassword.PasswordChar = '•';
            this.txtGetpassword.Size = new System.Drawing.Size(297, 34);
            this.txtGetpassword.TabIndex = 90;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(642, 57);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1160, 662);
            this.dataGridView1.TabIndex = 89;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(45, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(245, 38);
            this.label4.TabIndex = 60;
            this.label4.Text = "User Accounts";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.Location = new System.Drawing.Point(397, 153);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(113, 35);
            this.btnSearch.TabIndex = 65;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 42);
            this.label1.TabIndex = 61;
            this.label1.Text = "_____________________";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(76, 154);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(298, 34);
            this.txtSearch.TabIndex = 64;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.picHome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(94, 1033);
            this.panel1.TabIndex = 67;
            // 
            // frmViewUserAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.picBoxBackButton);
            this.Controls.Add(this.pnlUserAcconts);
            this.Controls.Add(this.panel1);
            this.Name = "frmViewUserAccounts";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmViewUserAccounts";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmViewUserAccounts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackButton)).EndInit();
            this.pnlUserAcconts.ResumeLayout(false);
            this.pnlUserAcconts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picHome;
        private System.Windows.Forms.PictureBox picBoxBackButton;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel pnlUserAcconts;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button btnGetPassword;
        private System.Windows.Forms.TextBox txtGetpassword;
        private System.Windows.Forms.Button btnReload;
    }
}