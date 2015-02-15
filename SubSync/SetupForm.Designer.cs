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
            this.components = new System.ComponentModel.Container();
            this.LbWelcomeMessage = new System.Windows.Forms.Label();
            this.LbWelcomeTitle = new System.Windows.Forms.Label();
            this.GpbSelectDirectories = new System.Windows.Forms.GroupBox();
            this.LstDirectories = new System.Windows.Forms.ListBox();
            this.BtnRemoveDirectory = new System.Windows.Forms.Button();
            this.BtnAddDirectory = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.LbLanguages = new System.Windows.Forms.Label();
            this.LstLanguagePreferences = new System.Windows.Forms.ListBox();
            this.LbLanguagePreferences = new System.Windows.Forms.Label();
            this.LstLanguages = new System.Windows.Forms.ListBox();
            this.BtnAddLanguage = new System.Windows.Forms.Button();
            this.BtnRemoveLanguage = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.BtnLanguageDown = new System.Windows.Forms.Button();
            this.BtnLanguageUp = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnStartStop = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.NotifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NotifyIconContextMenuItemOpenGUI = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuItemStartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemRunAtLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemCheckNow = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.BkgWorkerStartStopSync = new System.ComponentModel.BackgroundWorker();
            this.GpbSelectDirectories.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.NotifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbWelcomeMessage
            // 
            this.LbWelcomeMessage.AutoEllipsis = true;
            this.LbWelcomeMessage.AutoSize = true;
            this.LbWelcomeMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbWelcomeMessage.Location = new System.Drawing.Point(42, 72);
            this.LbWelcomeMessage.Name = "LbWelcomeMessage";
            this.LbWelcomeMessage.Size = new System.Drawing.Size(359, 17);
            this.LbWelcomeMessage.TabIndex = 7;
            this.LbWelcomeMessage.Text = "This simple setup will make SubSync up and running in a bit!";
            // 
            // LbWelcomeTitle
            // 
            this.LbWelcomeTitle.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbWelcomeTitle.Location = new System.Drawing.Point(29, 24);
            this.LbWelcomeTitle.Name = "LbWelcomeTitle";
            this.LbWelcomeTitle.Size = new System.Drawing.Size(385, 30);
            this.LbWelcomeTitle.TabIndex = 6;
            this.LbWelcomeTitle.Text = "Welcome to SubSync!";
            this.LbWelcomeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GpbSelectDirectories
            // 
            this.GpbSelectDirectories.Controls.Add(this.LstDirectories);
            this.GpbSelectDirectories.Controls.Add(this.BtnRemoveDirectory);
            this.GpbSelectDirectories.Controls.Add(this.BtnAddDirectory);
            this.GpbSelectDirectories.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GpbSelectDirectories.Location = new System.Drawing.Point(29, 116);
            this.GpbSelectDirectories.Name = "GpbSelectDirectories";
            this.GpbSelectDirectories.Size = new System.Drawing.Size(385, 118);
            this.GpbSelectDirectories.TabIndex = 8;
            this.GpbSelectDirectories.TabStop = false;
            this.GpbSelectDirectories.Text = "Step 1 - Choose your media folders";
            // 
            // LstDirectories
            // 
            this.LstDirectories.AllowDrop = true;
            this.LstDirectories.FormattingEnabled = true;
            this.LstDirectories.HorizontalScrollbar = true;
            this.LstDirectories.ItemHeight = 15;
            this.LstDirectories.Location = new System.Drawing.Point(16, 26);
            this.LstDirectories.Name = "LstDirectories";
            this.LstDirectories.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstDirectories.Size = new System.Drawing.Size(276, 79);
            this.LstDirectories.Sorted = true;
            this.LstDirectories.TabIndex = 3;
            this.LstDirectories.SelectedIndexChanged += new System.EventHandler(this.LstDirectories_SelectedIndexChanged);
            this.LstDirectories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstDirectories_KeyDown);
            // 
            // BtnRemoveDirectory
            // 
            this.BtnRemoveDirectory.Enabled = false;
            this.BtnRemoveDirectory.Location = new System.Drawing.Point(298, 70);
            this.BtnRemoveDirectory.Name = "BtnRemoveDirectory";
            this.BtnRemoveDirectory.Size = new System.Drawing.Size(75, 35);
            this.BtnRemoveDirectory.TabIndex = 2;
            this.BtnRemoveDirectory.Text = "Remove";
            this.ToolTip.SetToolTip(this.BtnRemoveDirectory, "Remove the selected media folders");
            this.BtnRemoveDirectory.UseVisualStyleBackColor = true;
            this.BtnRemoveDirectory.Click += new System.EventHandler(this.BtnRemoveDirectory_Click);
            // 
            // BtnAddDirectory
            // 
            this.BtnAddDirectory.Location = new System.Drawing.Point(298, 26);
            this.BtnAddDirectory.Name = "BtnAddDirectory";
            this.BtnAddDirectory.Size = new System.Drawing.Size(75, 35);
            this.BtnAddDirectory.TabIndex = 1;
            this.BtnAddDirectory.Text = "Add";
            this.ToolTip.SetToolTip(this.BtnAddDirectory, "Add a new media folder");
            this.BtnAddDirectory.UseVisualStyleBackColor = true;
            this.BtnAddDirectory.Click += new System.EventHandler(this.BtnAddDirectory_Click);
            // 
            // FolderBrowserDialog
            // 
            this.FolderBrowserDialog.Description = "Select your media folder";
            // 
            // LbLanguages
            // 
            this.LbLanguages.AutoSize = true;
            this.LbLanguages.Location = new System.Drawing.Point(20, 29);
            this.LbLanguages.Name = "LbLanguages";
            this.LbLanguages.Size = new System.Drawing.Size(119, 15);
            this.LbLanguages.TabIndex = 1;
            this.LbLanguages.Text = "Supported languages";
            // 
            // LstLanguagePreferences
            // 
            this.LstLanguagePreferences.FormattingEnabled = true;
            this.LstLanguagePreferences.HorizontalScrollbar = true;
            this.LstLanguagePreferences.ItemHeight = 15;
            this.LstLanguagePreferences.Location = new System.Drawing.Point(199, 48);
            this.LstLanguagePreferences.Name = "LstLanguagePreferences";
            this.LstLanguagePreferences.Size = new System.Drawing.Size(136, 94);
            this.LstLanguagePreferences.TabIndex = 2;
            this.LstLanguagePreferences.SelectedIndexChanged += new System.EventHandler(this.LstLanguagePreferences_SelectedIndexChanged);
            this.LstLanguagePreferences.DoubleClick += new System.EventHandler(this.LstLanguagePreferences_DoubleClick);
            // 
            // LbLanguagePreferences
            // 
            this.LbLanguagePreferences.AutoSize = true;
            this.LbLanguagePreferences.Location = new System.Drawing.Point(199, 29);
            this.LbLanguagePreferences.Name = "LbLanguagePreferences";
            this.LbLanguagePreferences.Size = new System.Drawing.Size(136, 15);
            this.LbLanguagePreferences.TabIndex = 3;
            this.LbLanguagePreferences.Text = "Your order of preference";
            // 
            // LstLanguages
            // 
            this.LstLanguages.FormattingEnabled = true;
            this.LstLanguages.HorizontalScrollbar = true;
            this.LstLanguages.ItemHeight = 15;
            this.LstLanguages.Location = new System.Drawing.Point(19, 48);
            this.LstLanguages.Name = "LstLanguages";
            this.LstLanguages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstLanguages.Size = new System.Drawing.Size(136, 94);
            this.LstLanguages.Sorted = true;
            this.LstLanguages.TabIndex = 4;
            this.LstLanguages.SelectedIndexChanged += new System.EventHandler(this.LstLanguages_SelectedIndexChanged);
            this.LstLanguages.DoubleClick += new System.EventHandler(this.LstLanguages_DoubleClick);
            // 
            // BtnAddLanguage
            // 
            this.BtnAddLanguage.Enabled = false;
            this.BtnAddLanguage.Location = new System.Drawing.Point(162, 63);
            this.BtnAddLanguage.Name = "BtnAddLanguage";
            this.BtnAddLanguage.Size = new System.Drawing.Size(31, 29);
            this.BtnAddLanguage.TabIndex = 5;
            this.BtnAddLanguage.Text = ">>";
            this.BtnAddLanguage.UseVisualStyleBackColor = true;
            this.BtnAddLanguage.Click += new System.EventHandler(this.BtnLanguageAdd_Click);
            // 
            // BtnRemoveLanguage
            // 
            this.BtnRemoveLanguage.Enabled = false;
            this.BtnRemoveLanguage.Location = new System.Drawing.Point(162, 98);
            this.BtnRemoveLanguage.Name = "BtnRemoveLanguage";
            this.BtnRemoveLanguage.Size = new System.Drawing.Size(31, 29);
            this.BtnRemoveLanguage.TabIndex = 6;
            this.BtnRemoveLanguage.Text = "<<";
            this.BtnRemoveLanguage.UseVisualStyleBackColor = true;
            this.BtnRemoveLanguage.Click += new System.EventHandler(this.BtnLanguageRemove_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BtnLanguageDown);
            this.groupBox1.Controls.Add(this.BtnLanguageUp);
            this.groupBox1.Controls.Add(this.BtnRemoveLanguage);
            this.groupBox1.Controls.Add(this.BtnAddLanguage);
            this.groupBox1.Controls.Add(this.LstLanguages);
            this.groupBox1.Controls.Add(this.LbLanguagePreferences);
            this.groupBox1.Controls.Add(this.LstLanguagePreferences);
            this.groupBox1.Controls.Add(this.LbLanguages);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(385, 158);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 2 - Choose the languages you are interested into";
            // 
            // BtnLanguageDown
            // 
            this.BtnLanguageDown.Enabled = false;
            this.BtnLanguageDown.Location = new System.Drawing.Point(342, 98);
            this.BtnLanguageDown.Name = "BtnLanguageDown";
            this.BtnLanguageDown.Size = new System.Drawing.Size(31, 29);
            this.BtnLanguageDown.TabIndex = 8;
            this.BtnLanguageDown.Text = "-";
            this.ToolTip.SetToolTip(this.BtnLanguageDown, "Move the selected language down on your preference list");
            this.BtnLanguageDown.UseVisualStyleBackColor = true;
            this.BtnLanguageDown.Click += new System.EventHandler(this.BtnLanguageDown_Click);
            // 
            // BtnLanguageUp
            // 
            this.BtnLanguageUp.Enabled = false;
            this.BtnLanguageUp.Location = new System.Drawing.Point(342, 63);
            this.BtnLanguageUp.Name = "BtnLanguageUp";
            this.BtnLanguageUp.Size = new System.Drawing.Size(31, 29);
            this.BtnLanguageUp.TabIndex = 7;
            this.BtnLanguageUp.Text = "+";
            this.ToolTip.SetToolTip(this.BtnLanguageUp, "Move the selected language up on your preference list");
            this.BtnLanguageUp.UseVisualStyleBackColor = true;
            this.BtnLanguageUp.Click += new System.EventHandler(this.BtnLanguageUp_Click);
            // 
            // BtnStartStop
            // 
            this.BtnStartStop.Enabled = false;
            this.BtnStartStop.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStartStop.Location = new System.Drawing.Point(152, 440);
            this.BtnStartStop.Name = "BtnStartStop";
            this.BtnStartStop.Size = new System.Drawing.Size(132, 46);
            this.BtnStartStop.TabIndex = 10;
            this.BtnStartStop.Text = "Start SubSync";
            this.BtnStartStop.UseVisualStyleBackColor = true;
            this.BtnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconContextMenu;
            this.NotifyIcon.Text = "SubSync";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // NotifyIconContextMenu
            // 
            this.NotifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotifyIconContextMenuItemOpenGUI,
            this.NotifyIconContextMenuItemStartStop,
            this.NotifyIconContextMenuSeparator1,
            this.NotifyIconContextMenuItemRunAtLogin,
            this.NotifyIconContextMenuSeparator2,
            this.NotifyIconContextMenuItemCheckNow,
            this.NotifyIconContextMenuSeparator3,
            this.NotifyIconContextMenuItemExit});
            this.NotifyIconContextMenu.Name = "TrayIconContextMenu";
            this.NotifyIconContextMenu.Size = new System.Drawing.Size(242, 132);
            this.NotifyIconContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NotifyIconContextMenu_ItemClicked);
            // 
            // NotifyIconContextMenuItemOpenGUI
            // 
            this.NotifyIconContextMenuItemOpenGUI.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.NotifyIconContextMenuItemOpenGUI.Name = "NotifyIconContextMenuItemOpenGUI";
            this.NotifyIconContextMenuItemOpenGUI.Size = new System.Drawing.Size(241, 22);
            this.NotifyIconContextMenuItemOpenGUI.Text = "&Open SubSync";
            // 
            // NotifyIconContextMenuItemStartStop
            // 
            this.NotifyIconContextMenuItemStartStop.Name = "NotifyIconContextMenuItemStartStop";
            this.NotifyIconContextMenuItemStartStop.Size = new System.Drawing.Size(241, 22);
            this.NotifyIconContextMenuItemStartStop.Text = "&Start SubSync...";
            // 
            // NotifyIconContextMenuSeparator1
            // 
            this.NotifyIconContextMenuSeparator1.Name = "NotifyIconContextMenuSeparator1";
            this.NotifyIconContextMenuSeparator1.Size = new System.Drawing.Size(238, 6);
            // 
            // NotifyIconContextMenuItemRunAtLogin
            // 
            this.NotifyIconContextMenuItemRunAtLogin.CheckOnClick = true;
            this.NotifyIconContextMenuItemRunAtLogin.Name = "NotifyIconContextMenuItemRunAtLogin";
            this.NotifyIconContextMenuItemRunAtLogin.Size = new System.Drawing.Size(241, 22);
            this.NotifyIconContextMenuItemRunAtLogin.Text = "Run SubSync at Windows &Login";
            this.NotifyIconContextMenuItemRunAtLogin.CheckedChanged += new System.EventHandler(this.NotifyIconContextMenuItemRunAtLogin_CheckedChanged);
            // 
            // NotifyIconContextMenuSeparator2
            // 
            this.NotifyIconContextMenuSeparator2.Name = "NotifyIconContextMenuSeparator2";
            this.NotifyIconContextMenuSeparator2.Size = new System.Drawing.Size(238, 6);
            // 
            // NotifyIconContextMenuItemCheckNow
            // 
            this.NotifyIconContextMenuItemCheckNow.Enabled = false;
            this.NotifyIconContextMenuItemCheckNow.Name = "NotifyIconContextMenuItemCheckNow";
            this.NotifyIconContextMenuItemCheckNow.Size = new System.Drawing.Size(241, 22);
            this.NotifyIconContextMenuItemCheckNow.Text = "&Check for subtitles now";
            this.NotifyIconContextMenuItemCheckNow.ToolTipText = "Check for missing subtitles for your videos now!";
            // 
            // NotifyIconContextMenuSeparator3
            // 
            this.NotifyIconContextMenuSeparator3.Name = "NotifyIconContextMenuSeparator3";
            this.NotifyIconContextMenuSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // NotifyIconContextMenuItemExit
            // 
            this.NotifyIconContextMenuItemExit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.NotifyIconContextMenuItemExit.Name = "NotifyIconContextMenuItemExit";
            this.NotifyIconContextMenuItemExit.Size = new System.Drawing.Size(241, 22);
            this.NotifyIconContextMenuItemExit.Text = "&Exit";
            // 
            // BkgWorkerStartStopSync
            // 
            this.BkgWorkerStartStopSync.WorkerReportsProgress = true;
            this.BkgWorkerStartStopSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkgWorkerStartStopSync_DoWork);
            this.BkgWorkerStartStopSync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BkgWorkerStartStopSync_ProgressChanged);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 513);
            this.Controls.Add(this.BtnStartStop);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GpbSelectDirectories);
            this.Controls.Add(this.LbWelcomeMessage);
            this.Controls.Add(this.LbWelcomeTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SubSync";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupForm_FormClosing);
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.GpbSelectDirectories.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.NotifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LbWelcomeMessage;
        private System.Windows.Forms.Label LbWelcomeTitle;
        private System.Windows.Forms.GroupBox GpbSelectDirectories;
        private System.Windows.Forms.Button BtnAddDirectory;
        private System.Windows.Forms.Button BtnRemoveDirectory;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.ListBox LstDirectories;
        private System.Windows.Forms.Label LbLanguages;
        private System.Windows.Forms.ListBox LstLanguagePreferences;
        private System.Windows.Forms.Label LbLanguagePreferences;
        private System.Windows.Forms.ListBox LstLanguages;
        private System.Windows.Forms.Button BtnAddLanguage;
        private System.Windows.Forms.Button BtnRemoveLanguage;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BtnLanguageDown;
        private System.Windows.Forms.Button BtnLanguageUp;
        private System.Windows.Forms.ToolTip ToolTip;
        private System.Windows.Forms.Button BtnStartStop;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.ComponentModel.BackgroundWorker BkgWorkerStartStopSync;
        private System.Windows.Forms.ContextMenuStrip NotifyIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemCheckNow;
        private System.Windows.Forms.ToolStripSeparator NotifyIconContextMenuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemOpenGUI;
        private System.Windows.Forms.ToolStripSeparator NotifyIconContextMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemStartStop;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemRunAtLogin;
        private System.Windows.Forms.ToolStripSeparator NotifyIconContextMenuSeparator3;




    }
}

