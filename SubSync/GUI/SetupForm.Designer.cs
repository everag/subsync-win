namespace SubSync.GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
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
            this.GpbSelectLanguages = new System.Windows.Forms.GroupBox();
            this.BtnLanguageDown = new System.Windows.Forms.Button();
            this.BtnLanguageUp = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.BtnStartStop = new System.Windows.Forms.Button();
            this.NotifyIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NotifyIconContextMenuItemOpenGUI = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuItemStartStop = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemRunAtLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemCheckNow = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIconContextMenuSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyIconContextMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.BkgWorkerStartStopSync = new System.ComponentModel.BackgroundWorker();
            this.GpbSelectDirectories.SuspendLayout();
            this.GpbSelectLanguages.SuspendLayout();
            this.NotifyIconContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbWelcomeMessage
            // 
            resources.ApplyResources(this.LbWelcomeMessage, "LbWelcomeMessage");
            this.LbWelcomeMessage.AutoEllipsis = true;
            this.LbWelcomeMessage.Name = "LbWelcomeMessage";
            this.ToolTip.SetToolTip(this.LbWelcomeMessage, resources.GetString("LbWelcomeMessage.ToolTip"));
            // 
            // LbWelcomeTitle
            // 
            resources.ApplyResources(this.LbWelcomeTitle, "LbWelcomeTitle");
            this.LbWelcomeTitle.Name = "LbWelcomeTitle";
            this.ToolTip.SetToolTip(this.LbWelcomeTitle, resources.GetString("LbWelcomeTitle.ToolTip"));
            // 
            // GpbSelectDirectories
            // 
            resources.ApplyResources(this.GpbSelectDirectories, "GpbSelectDirectories");
            this.GpbSelectDirectories.Controls.Add(this.LstDirectories);
            this.GpbSelectDirectories.Controls.Add(this.BtnRemoveDirectory);
            this.GpbSelectDirectories.Controls.Add(this.BtnAddDirectory);
            this.GpbSelectDirectories.Name = "GpbSelectDirectories";
            this.GpbSelectDirectories.TabStop = false;
            this.ToolTip.SetToolTip(this.GpbSelectDirectories, resources.GetString("GpbSelectDirectories.ToolTip"));
            // 
            // LstDirectories
            // 
            resources.ApplyResources(this.LstDirectories, "LstDirectories");
            this.LstDirectories.AllowDrop = true;
            this.LstDirectories.FormattingEnabled = true;
            this.LstDirectories.Name = "LstDirectories";
            this.LstDirectories.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstDirectories.Sorted = true;
            this.ToolTip.SetToolTip(this.LstDirectories, resources.GetString("LstDirectories.ToolTip"));
            this.LstDirectories.SelectedIndexChanged += new System.EventHandler(this.LstDirectories_SelectedIndexChanged);
            this.LstDirectories.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstDirectories_KeyDown);
            // 
            // BtnRemoveDirectory
            // 
            resources.ApplyResources(this.BtnRemoveDirectory, "BtnRemoveDirectory");
            this.BtnRemoveDirectory.Name = "BtnRemoveDirectory";
            this.ToolTip.SetToolTip(this.BtnRemoveDirectory, resources.GetString("BtnRemoveDirectory.ToolTip"));
            this.BtnRemoveDirectory.UseVisualStyleBackColor = true;
            this.BtnRemoveDirectory.Click += new System.EventHandler(this.BtnRemoveDirectory_Click);
            // 
            // BtnAddDirectory
            // 
            resources.ApplyResources(this.BtnAddDirectory, "BtnAddDirectory");
            this.BtnAddDirectory.Name = "BtnAddDirectory";
            this.ToolTip.SetToolTip(this.BtnAddDirectory, resources.GetString("BtnAddDirectory.ToolTip"));
            this.BtnAddDirectory.UseVisualStyleBackColor = true;
            this.BtnAddDirectory.Click += new System.EventHandler(this.BtnAddDirectory_Click);
            // 
            // FolderBrowserDialog
            // 
            resources.ApplyResources(this.FolderBrowserDialog, "FolderBrowserDialog");
            // 
            // LbLanguages
            // 
            resources.ApplyResources(this.LbLanguages, "LbLanguages");
            this.LbLanguages.Name = "LbLanguages";
            this.ToolTip.SetToolTip(this.LbLanguages, resources.GetString("LbLanguages.ToolTip"));
            // 
            // LstLanguagePreferences
            // 
            resources.ApplyResources(this.LstLanguagePreferences, "LstLanguagePreferences");
            this.LstLanguagePreferences.FormattingEnabled = true;
            this.LstLanguagePreferences.Name = "LstLanguagePreferences";
            this.ToolTip.SetToolTip(this.LstLanguagePreferences, resources.GetString("LstLanguagePreferences.ToolTip"));
            this.LstLanguagePreferences.SelectedIndexChanged += new System.EventHandler(this.LstLanguagePreferences_SelectedIndexChanged);
            this.LstLanguagePreferences.DoubleClick += new System.EventHandler(this.LstLanguagePreferences_DoubleClick);
            // 
            // LbLanguagePreferences
            // 
            resources.ApplyResources(this.LbLanguagePreferences, "LbLanguagePreferences");
            this.LbLanguagePreferences.Name = "LbLanguagePreferences";
            this.ToolTip.SetToolTip(this.LbLanguagePreferences, resources.GetString("LbLanguagePreferences.ToolTip"));
            // 
            // LstLanguages
            // 
            resources.ApplyResources(this.LstLanguages, "LstLanguages");
            this.LstLanguages.FormattingEnabled = true;
            this.LstLanguages.Name = "LstLanguages";
            this.LstLanguages.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.LstLanguages.Sorted = true;
            this.ToolTip.SetToolTip(this.LstLanguages, resources.GetString("LstLanguages.ToolTip"));
            this.LstLanguages.SelectedIndexChanged += new System.EventHandler(this.LstLanguages_SelectedIndexChanged);
            this.LstLanguages.DoubleClick += new System.EventHandler(this.LstLanguages_DoubleClick);
            // 
            // BtnAddLanguage
            // 
            resources.ApplyResources(this.BtnAddLanguage, "BtnAddLanguage");
            this.BtnAddLanguage.Name = "BtnAddLanguage";
            this.ToolTip.SetToolTip(this.BtnAddLanguage, resources.GetString("BtnAddLanguage.ToolTip"));
            this.BtnAddLanguage.UseVisualStyleBackColor = true;
            this.BtnAddLanguage.Click += new System.EventHandler(this.BtnLanguageAdd_Click);
            // 
            // BtnRemoveLanguage
            // 
            resources.ApplyResources(this.BtnRemoveLanguage, "BtnRemoveLanguage");
            this.BtnRemoveLanguage.Name = "BtnRemoveLanguage";
            this.ToolTip.SetToolTip(this.BtnRemoveLanguage, resources.GetString("BtnRemoveLanguage.ToolTip"));
            this.BtnRemoveLanguage.UseVisualStyleBackColor = true;
            this.BtnRemoveLanguage.Click += new System.EventHandler(this.BtnLanguageRemove_Click);
            // 
            // GpbSelectLanguages
            // 
            resources.ApplyResources(this.GpbSelectLanguages, "GpbSelectLanguages");
            this.GpbSelectLanguages.Controls.Add(this.BtnLanguageDown);
            this.GpbSelectLanguages.Controls.Add(this.BtnLanguageUp);
            this.GpbSelectLanguages.Controls.Add(this.BtnRemoveLanguage);
            this.GpbSelectLanguages.Controls.Add(this.BtnAddLanguage);
            this.GpbSelectLanguages.Controls.Add(this.LstLanguages);
            this.GpbSelectLanguages.Controls.Add(this.LbLanguagePreferences);
            this.GpbSelectLanguages.Controls.Add(this.LstLanguagePreferences);
            this.GpbSelectLanguages.Controls.Add(this.LbLanguages);
            this.GpbSelectLanguages.Name = "GpbSelectLanguages";
            this.GpbSelectLanguages.TabStop = false;
            this.ToolTip.SetToolTip(this.GpbSelectLanguages, resources.GetString("GpbSelectLanguages.ToolTip"));
            // 
            // BtnLanguageDown
            // 
            resources.ApplyResources(this.BtnLanguageDown, "BtnLanguageDown");
            this.BtnLanguageDown.Name = "BtnLanguageDown";
            this.ToolTip.SetToolTip(this.BtnLanguageDown, resources.GetString("BtnLanguageDown.ToolTip"));
            this.BtnLanguageDown.UseVisualStyleBackColor = true;
            this.BtnLanguageDown.Click += new System.EventHandler(this.BtnLanguageDown_Click);
            // 
            // BtnLanguageUp
            // 
            resources.ApplyResources(this.BtnLanguageUp, "BtnLanguageUp");
            this.BtnLanguageUp.Name = "BtnLanguageUp";
            this.ToolTip.SetToolTip(this.BtnLanguageUp, resources.GetString("BtnLanguageUp.ToolTip"));
            this.BtnLanguageUp.UseVisualStyleBackColor = true;
            this.BtnLanguageUp.Click += new System.EventHandler(this.BtnLanguageUp_Click);
            // 
            // BtnStartStop
            // 
            resources.ApplyResources(this.BtnStartStop, "BtnStartStop");
            this.BtnStartStop.Name = "BtnStartStop";
            this.ToolTip.SetToolTip(this.BtnStartStop, resources.GetString("BtnStartStop.ToolTip"));
            this.BtnStartStop.UseVisualStyleBackColor = true;
            this.BtnStartStop.Click += new System.EventHandler(this.BtnStartStop_Click);
            // 
            // NotifyIconContextMenu
            // 
            resources.ApplyResources(this.NotifyIconContextMenu, "NotifyIconContextMenu");
            this.NotifyIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotifyIconContextMenuItemOpenGUI,
            this.NotifyIconContextMenuItemStartStop,
            this.NotifyIconContextMenuSeparator1,
            this.NotifyIconContextMenuItemRunAtLogin,
            this.NotifyIconContextMenuSeparator2,
            this.NotifyIconContextMenuItemCheckNow,
            this.NotifyIconContextMenuSeparator3,
            this.NotifyIconContextMenuItemCheckUpdates,
            this.NotifyIconContextMenuItemAbout,
            this.NotifyIconContextMenuSeparator4,
            this.NotifyIconContextMenuItemExit});
            this.NotifyIconContextMenu.Name = "TrayIconContextMenu";
            this.ToolTip.SetToolTip(this.NotifyIconContextMenu, resources.GetString("NotifyIconContextMenu.ToolTip"));
            this.NotifyIconContextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.NotifyIconContextMenu_ItemClicked);
            // 
            // NotifyIconContextMenuItemOpenGUI
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemOpenGUI, "NotifyIconContextMenuItemOpenGUI");
            this.NotifyIconContextMenuItemOpenGUI.Name = "NotifyIconContextMenuItemOpenGUI";
            // 
            // NotifyIconContextMenuItemStartStop
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemStartStop, "NotifyIconContextMenuItemStartStop");
            this.NotifyIconContextMenuItemStartStop.Name = "NotifyIconContextMenuItemStartStop";
            // 
            // NotifyIconContextMenuSeparator1
            // 
            resources.ApplyResources(this.NotifyIconContextMenuSeparator1, "NotifyIconContextMenuSeparator1");
            this.NotifyIconContextMenuSeparator1.Name = "NotifyIconContextMenuSeparator1";
            // 
            // NotifyIconContextMenuItemRunAtLogin
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemRunAtLogin, "NotifyIconContextMenuItemRunAtLogin");
            this.NotifyIconContextMenuItemRunAtLogin.CheckOnClick = true;
            this.NotifyIconContextMenuItemRunAtLogin.Name = "NotifyIconContextMenuItemRunAtLogin";
            this.NotifyIconContextMenuItemRunAtLogin.CheckedChanged += new System.EventHandler(this.NotifyIconContextMenuItemRunAtLogin_CheckedChanged);
            // 
            // NotifyIconContextMenuSeparator2
            // 
            resources.ApplyResources(this.NotifyIconContextMenuSeparator2, "NotifyIconContextMenuSeparator2");
            this.NotifyIconContextMenuSeparator2.Name = "NotifyIconContextMenuSeparator2";
            // 
            // NotifyIconContextMenuItemCheckNow
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemCheckNow, "NotifyIconContextMenuItemCheckNow");
            this.NotifyIconContextMenuItemCheckNow.Name = "NotifyIconContextMenuItemCheckNow";
            // 
            // NotifyIconContextMenuSeparator3
            // 
            resources.ApplyResources(this.NotifyIconContextMenuSeparator3, "NotifyIconContextMenuSeparator3");
            this.NotifyIconContextMenuSeparator3.Name = "NotifyIconContextMenuSeparator3";
            // 
            // NotifyIconContextMenuItemCheckUpdates
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemCheckUpdates, "NotifyIconContextMenuItemCheckUpdates");
            this.NotifyIconContextMenuItemCheckUpdates.Name = "NotifyIconContextMenuItemCheckUpdates";
            // 
            // NotifyIconContextMenuItemAbout
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemAbout, "NotifyIconContextMenuItemAbout");
            this.NotifyIconContextMenuItemAbout.Name = "NotifyIconContextMenuItemAbout";
            // 
            // NotifyIconContextMenuSeparator4
            // 
            resources.ApplyResources(this.NotifyIconContextMenuSeparator4, "NotifyIconContextMenuSeparator4");
            this.NotifyIconContextMenuSeparator4.Name = "NotifyIconContextMenuSeparator4";
            // 
            // NotifyIconContextMenuItemExit
            // 
            resources.ApplyResources(this.NotifyIconContextMenuItemExit, "NotifyIconContextMenuItemExit");
            this.NotifyIconContextMenuItemExit.Name = "NotifyIconContextMenuItemExit";
            // 
            // NotifyIcon
            // 
            resources.ApplyResources(this.NotifyIcon, "NotifyIcon");
            this.NotifyIcon.ContextMenuStrip = this.NotifyIconContextMenu;
            this.NotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseDoubleClick);
            // 
            // BkgWorkerStartStopSync
            // 
            this.BkgWorkerStartStopSync.WorkerReportsProgress = true;
            this.BkgWorkerStartStopSync.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkgWorkerStartStopSync_DoWork);
            this.BkgWorkerStartStopSync.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BkgWorkerStartStopSync_ProgressChanged);
            // 
            // SetupForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.BtnStartStop);
            this.Controls.Add(this.GpbSelectLanguages);
            this.Controls.Add(this.GpbSelectDirectories);
            this.Controls.Add(this.LbWelcomeMessage);
            this.Controls.Add(this.LbWelcomeTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SetupForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ToolTip.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupForm_FormClosing);
            this.Load += new System.EventHandler(this.SetupForm_Load);
            this.GpbSelectDirectories.ResumeLayout(false);
            this.GpbSelectLanguages.ResumeLayout(false);
            this.GpbSelectLanguages.PerformLayout();
            this.NotifyIconContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox GpbSelectLanguages;
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
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemAbout;
        private System.Windows.Forms.ToolStripSeparator NotifyIconContextMenuSeparator4;
        private System.Windows.Forms.ToolStripMenuItem NotifyIconContextMenuItemCheckUpdates;




    }
}

