namespace SubSync
{
    partial class MainForm
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
            this.tbxFile = new System.Windows.Forms.TextBox();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.btnShowLanguages = new System.Windows.Forms.Button();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.lblLog = new System.Windows.Forms.Label();
            this.videoFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // tbxFile
            // 
            this.tbxFile.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxFile.Location = new System.Drawing.Point(13, 13);
            this.tbxFile.Name = "tbxFile";
            this.tbxFile.ReadOnly = true;
            this.tbxFile.Size = new System.Drawing.Size(250, 20);
            this.tbxFile.TabIndex = 0;
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(269, 12);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(55, 20);
            this.btnChooseFile.TabIndex = 1;
            this.btnChooseFile.Text = "Choose File";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // btnShowLanguages
            // 
            this.btnShowLanguages.Enabled = false;
            this.btnShowLanguages.Location = new System.Drawing.Point(330, 13);
            this.btnShowLanguages.Name = "btnShowLanguages";
            this.btnShowLanguages.Size = new System.Drawing.Size(109, 20);
            this.btnShowLanguages.TabIndex = 2;
            this.btnShowLanguages.Text = "Show Languages";
            this.btnShowLanguages.UseVisualStyleBackColor = true;
            this.btnShowLanguages.Click += new System.EventHandler(this.btnShowLanguages_Click);
            // 
            // tbxLog
            // 
            this.tbxLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbxLog.Location = new System.Drawing.Point(13, 73);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.ReadOnly = true;
            this.tbxLog.Size = new System.Drawing.Size(426, 132);
            this.tbxLog.TabIndex = 3;
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.Location = new System.Drawing.Point(13, 54);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(141, 13);
            this.lblLog.TabIndex = 4;
            this.lblLog.Text = "Available subtitle languages:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 217);
            this.Controls.Add(this.lblLog);
            this.Controls.Add(this.tbxLog);
            this.Controls.Add(this.btnShowLanguages);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.tbxFile);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search for subtitles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxFile;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Button btnShowLanguages;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.OpenFileDialog videoFileDialog;
    }
}

