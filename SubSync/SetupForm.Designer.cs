namespace SubSync
{
    partial class SetupForm
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
            this.PnWelcome = new System.Windows.Forms.Panel();
            this.BtStart = new System.Windows.Forms.Button();
            this.LbWelcomeMessage = new System.Windows.Forms.Label();
            this.LbWelcomeTitle = new System.Windows.Forms.Label();
            this.PnWelcome.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnWelcome
            // 
            this.PnWelcome.Controls.Add(this.BtStart);
            this.PnWelcome.Controls.Add(this.LbWelcomeMessage);
            this.PnWelcome.Controls.Add(this.LbWelcomeTitle);
            this.PnWelcome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnWelcome.Location = new System.Drawing.Point(0, 0);
            this.PnWelcome.Name = "PnWelcome";
            this.PnWelcome.Size = new System.Drawing.Size(346, 293);
            this.PnWelcome.TabIndex = 0;
            // 
            // BtStart
            // 
            this.BtStart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtStart.Location = new System.Drawing.Point(108, 183);
            this.BtStart.Name = "BtStart";
            this.BtStart.Size = new System.Drawing.Size(118, 36);
            this.BtStart.TabIndex = 5;
            this.BtStart.Text = "Start the tutorial";
            this.BtStart.UseVisualStyleBackColor = true;
            this.BtStart.Click += new System.EventHandler(this.BtStart_Click);
            // 
            // LbWelcomeMessage
            // 
            this.LbWelcomeMessage.AutoEllipsis = true;
            this.LbWelcomeMessage.AutoSize = true;
            this.LbWelcomeMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbWelcomeMessage.Location = new System.Drawing.Point(50, 126);
            this.LbWelcomeMessage.Name = "LbWelcomeMessage";
            this.LbWelcomeMessage.Size = new System.Drawing.Size(251, 17);
            this.LbWelcomeMessage.TabIndex = 4;
            this.LbWelcomeMessage.Text = "This tutorial will help you setup SubSync ;)";
            // 
            // LbWelcomeTitle
            // 
            this.LbWelcomeTitle.AutoSize = true;
            this.LbWelcomeTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbWelcomeTitle.Location = new System.Drawing.Point(71, 73);
            this.LbWelcomeTitle.Name = "LbWelcomeTitle";
            this.LbWelcomeTitle.Size = new System.Drawing.Size(194, 25);
            this.LbWelcomeTitle.TabIndex = 3;
            this.LbWelcomeTitle.Text = "Welcome to SubSync!";
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 293);
            this.Controls.Add(this.PnWelcome);
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubSync Setup";
            this.PnWelcome.ResumeLayout(false);
            this.PnWelcome.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnWelcome;
        private System.Windows.Forms.Button BtStart;
        private System.Windows.Forms.Label LbWelcomeMessage;
        private System.Windows.Forms.Label LbWelcomeTitle;



    }
}

