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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
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
            resources.ApplyResources(this.ImgLogo, "ImgLogo");
            this.ImgLogo.Name = "ImgLogo";
            this.ImgLogo.TabStop = false;
            // 
            // LbVersion
            // 
            resources.ApplyResources(this.LbVersion, "LbVersion");
            this.LbVersion.Name = "LbVersion";
            // 
            // LbBuild
            // 
            resources.ApplyResources(this.LbBuild, "LbBuild");
            this.LbBuild.Name = "LbBuild";
            // 
            // LnkVisitHomePage
            // 
            resources.ApplyResources(this.LnkVisitHomePage, "LnkVisitHomePage");
            this.LnkVisitHomePage.Name = "LnkVisitHomePage";
            this.LnkVisitHomePage.TabStop = true;
            this.LnkVisitHomePage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkVisitHomePage_LinkClicked);
            // 
            // LnkGoToFacebook
            // 
            resources.ApplyResources(this.LnkGoToFacebook, "LnkGoToFacebook");
            this.LnkGoToFacebook.Name = "LnkGoToFacebook";
            this.LnkGoToFacebook.TabStop = true;
            this.LnkGoToFacebook.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkGoToFacebook_LinkClicked);
            // 
            // LnkDevelopedBy
            // 
            resources.ApplyResources(this.LnkDevelopedBy, "LnkDevelopedBy");
            this.LnkDevelopedBy.Name = "LnkDevelopedBy";
            this.LnkDevelopedBy.TabStop = true;
            this.LnkDevelopedBy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkDevelopedBy_LinkClicked);
            // 
            // LnkLogoBy
            // 
            resources.ApplyResources(this.LnkLogoBy, "LnkLogoBy");
            this.LnkLogoBy.Name = "LnkLogoBy";
            this.LnkLogoBy.TabStop = true;
            this.LnkLogoBy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkLogoBy_LinkClicked);
            // 
            // LnkContactUs
            // 
            resources.ApplyResources(this.LnkContactUs, "LnkContactUs");
            this.LnkContactUs.Name = "LnkContactUs";
            this.LnkContactUs.TabStop = true;
            this.LnkContactUs.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkContactUs_LinkClicked);
            // 
            // AboutForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
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