namespace SubSync.GUI
{
    partial class AboutForm
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
            this.ImgLogo = new System.Windows.Forms.PictureBox();
            this.LbVersion = new System.Windows.Forms.Label();
            this.LbBuild = new System.Windows.Forms.Label();
            this.LnkVisitHomePage = new System.Windows.Forms.LinkLabel();
            this.LnkGoToFacebook = new System.Windows.Forms.LinkLabel();
            this.LnkDevelopedBy = new System.Windows.Forms.LinkLabel();
            this.LnkLogoBy = new System.Windows.Forms.LinkLabel();
            this.LnkContactUs = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.ImgLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // ImgLogo
            // 
            this.ImgLogo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ImgLogo.Image = global::SubSync.Properties.Resources.SubSync_Logo_Horizontal;
            this.ImgLogo.InitialImage = null;
            this.ImgLogo.Location = new System.Drawing.Point(12, 12);
            this.ImgLogo.Name = "ImgLogo";
            this.ImgLogo.Size = new System.Drawing.Size(350, 100);
            this.ImgLogo.TabIndex = 0;
            this.ImgLogo.TabStop = false;
            // 
            // LbVersion
            // 
            this.LbVersion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbVersion.Location = new System.Drawing.Point(13, 122);
            this.LbVersion.Name = "LbVersion";
            this.LbVersion.Size = new System.Drawing.Size(170, 24);
            this.LbVersion.TabIndex = 1;
            this.LbVersion.Text = "SubSync";
            this.LbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LbBuild
            // 
            this.LbBuild.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbBuild.Location = new System.Drawing.Point(192, 122);
            this.LbBuild.Name = "LbBuild";
            this.LbBuild.Size = new System.Drawing.Size(170, 24);
            this.LbBuild.TabIndex = 2;
            this.LbBuild.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LnkVisitHomePage
            // 
            this.LnkVisitHomePage.AutoSize = true;
            this.LnkVisitHomePage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkVisitHomePage.Location = new System.Drawing.Point(13, 261);
            this.LnkVisitHomePage.Name = "LnkVisitHomePage";
            this.LnkVisitHomePage.Size = new System.Drawing.Size(111, 13);
            this.LnkVisitHomePage.TabIndex = 3;
            this.LnkVisitHomePage.TabStop = true;
            this.LnkVisitHomePage.Text = "Visit our Home Page";
            this.LnkVisitHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkVisitHomePage_LinkClicked);
            // 
            // LnkGoToFacebook
            // 
            this.LnkGoToFacebook.AutoSize = true;
            this.LnkGoToFacebook.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkGoToFacebook.Location = new System.Drawing.Point(247, 261);
            this.LnkGoToFacebook.Name = "LnkGoToFacebook";
            this.LnkGoToFacebook.Size = new System.Drawing.Size(115, 13);
            this.LnkGoToFacebook.TabIndex = 4;
            this.LnkGoToFacebook.TabStop = true;
            this.LnkGoToFacebook.Text = "Like us on Facebook!";
            this.LnkGoToFacebook.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkGoToFacebook_LinkClicked);
            // 
            // LnkDevelopedBy
            // 
            this.LnkDevelopedBy.AutoSize = true;
            this.LnkDevelopedBy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkDevelopedBy.Location = new System.Drawing.Point(13, 162);
            this.LnkDevelopedBy.Name = "LnkDevelopedBy";
            this.LnkDevelopedBy.Size = new System.Drawing.Size(77, 13);
            this.LnkDevelopedBy.TabIndex = 5;
            this.LnkDevelopedBy.TabStop = true;
            this.LnkDevelopedBy.Text = "Developed By";
            this.LnkDevelopedBy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkDevelopedBy_LinkClicked);
            // 
            // LnkLogoBy
            // 
            this.LnkLogoBy.AutoSize = true;
            this.LnkLogoBy.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkLogoBy.Location = new System.Drawing.Point(13, 188);
            this.LnkLogoBy.Name = "LnkLogoBy";
            this.LnkLogoBy.Size = new System.Drawing.Size(48, 13);
            this.LnkLogoBy.TabIndex = 6;
            this.LnkLogoBy.TabStop = true;
            this.LnkLogoBy.Text = "Logo By";
            this.LnkLogoBy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLogoBy_LinkClicked);
            // 
            // LnkContactUs
            // 
            this.LnkContactUs.AutoSize = true;
            this.LnkContactUs.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LnkContactUs.Location = new System.Drawing.Point(13, 225);
            this.LnkContactUs.Name = "LnkContactUs";
            this.LnkContactUs.Size = new System.Drawing.Size(63, 13);
            this.LnkContactUs.TabIndex = 7;
            this.LnkContactUs.TabStop = true;
            this.LnkContactUs.Text = "Contact Us";
            this.LnkContactUs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkContactUs_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 295);
            this.Controls.Add(this.LnkContactUs);
            this.Controls.Add(this.LnkLogoBy);
            this.Controls.Add(this.LnkDevelopedBy);
            this.Controls.Add(this.LnkGoToFacebook);
            this.Controls.Add(this.LnkVisitHomePage);
            this.Controls.Add(this.LbBuild);
            this.Controls.Add(this.LbVersion);
            this.Controls.Add(this.ImgLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(393, 334);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 334);
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About SubSync";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AboutForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImgLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImgLogo;
        private System.Windows.Forms.Label LbVersion;
        private System.Windows.Forms.Label LbBuild;
        private System.Windows.Forms.LinkLabel LnkVisitHomePage;
        private System.Windows.Forms.LinkLabel LnkGoToFacebook;
        private System.Windows.Forms.LinkLabel LnkDevelopedBy;
        private System.Windows.Forms.LinkLabel LnkLogoBy;
        private System.Windows.Forms.LinkLabel LnkContactUs;
    }
}