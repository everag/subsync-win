namespace SubSync.GUI
{
    partial class LanguageSelectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LanguageSelectionForm));
            this.LbSelectLanguage = new System.Windows.Forms.Label();
            this.CboLanguage = new System.Windows.Forms.ComboBox();
            this.BtOk = new System.Windows.Forms.Button();
            this.BtCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LbSelectLanguage
            // 
            resources.ApplyResources(this.LbSelectLanguage, "LbSelectLanguage");
            this.LbSelectLanguage.Name = "LbSelectLanguage";
            // 
            // CboLanguage
            // 
            resources.ApplyResources(this.CboLanguage, "CboLanguage");
            this.CboLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CboLanguage.FormattingEnabled = true;
            this.CboLanguage.Name = "CboLanguage";
            // 
            // BtOk
            // 
            resources.ApplyResources(this.BtOk, "BtOk");
            this.BtOk.Name = "BtOk";
            this.BtOk.UseVisualStyleBackColor = true;
            this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
            // 
            // BtCancel
            // 
            resources.ApplyResources(this.BtCancel, "BtCancel");
            this.BtCancel.Name = "BtCancel";
            this.BtCancel.UseVisualStyleBackColor = true;
            this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
            // 
            // LanguageSelectionForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtCancel);
            this.Controls.Add(this.BtOk);
            this.Controls.Add(this.CboLanguage);
            this.Controls.Add(this.LbSelectLanguage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LanguageSelectionForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.LanguageSelectionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LbSelectLanguage;
        private System.Windows.Forms.ComboBox CboLanguage;
        private System.Windows.Forms.Button BtOk;
        private System.Windows.Forms.Button BtCancel;
    }
}