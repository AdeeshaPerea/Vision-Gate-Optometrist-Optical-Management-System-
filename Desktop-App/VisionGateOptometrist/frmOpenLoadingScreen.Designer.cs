namespace VisionGateOptometrist
{
    partial class frmOpenLoadingScreen
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.ProgressBar_OpenPageLoading = new System.Windows.Forms.ProgressBar();
            this.Timer_OpenPageLoadingScreen = new System.Windows.Forms.Timer(this.components);
            this.lblPresentage_Loading = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 591);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loading ...";
            // 
            // ProgressBar_OpenPageLoading
            // 
            this.ProgressBar_OpenPageLoading.Location = new System.Drawing.Point(52, 612);
            this.ProgressBar_OpenPageLoading.Name = "ProgressBar_OpenPageLoading";
            this.ProgressBar_OpenPageLoading.Size = new System.Drawing.Size(846, 17);
            this.ProgressBar_OpenPageLoading.TabIndex = 1;
            // 
            // Timer_OpenPageLoadingScreen
            // 
            this.Timer_OpenPageLoadingScreen.Tick += new System.EventHandler(this.Timer_OpenPageLoadingScreen_Tick);
            // 
            // lblPresentage_Loading
            // 
            this.lblPresentage_Loading.AutoSize = true;
            this.lblPresentage_Loading.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPresentage_Loading.Location = new System.Drawing.Point(130, 591);
            this.lblPresentage_Loading.Name = "lblPresentage_Loading";
            this.lblPresentage_Loading.Size = new System.Drawing.Size(31, 18);
            this.lblPresentage_Loading.TabIndex = 2;
            this.lblPresentage_Loading.Text = "0%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(531, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(320, 50);
            this.label2.TabIndex = 3;
            this.label2.Text = "Wait a moment...";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._5b0ee802f714b7c6465a89af2b6ab70a__1__removebg_preview;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(858, 431);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(481, 348);
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::VisionGateOptometrist.Properties.Resources._5b0ee802f714b7c6465a89af2b6ab70a__1__removebg_preview;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(6, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(613, 497);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // frmOpenLoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPresentage_Loading);
            this.Controls.Add(this.ProgressBar_OpenPageLoading);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmOpenLoadingScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOpenLoadingScreen";
            this.Load += new System.EventHandler(this.frmOpenLoadingScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar ProgressBar_OpenPageLoading;
        private System.Windows.Forms.Timer Timer_OpenPageLoadingScreen;
        private System.Windows.Forms.Label lblPresentage_Loading;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}