namespace FileSync
{
    partial class Form1
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
            this.driveListView = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.driveEditorGroup = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.driveSettingDisableBackup = new System.Windows.Forms.Button();
            this.driveSettingEnableBackup = new System.Windows.Forms.Button();
            this.driveSettingBackupPath = new System.Windows.Forms.Button();
            this.driveSettingSetup = new System.Windows.Forms.Button();
            this.driveSettingForceBackup = new System.Windows.Forms.Button();
            this.driveSettingDelete = new System.Windows.Forms.Button();
            this.folderSettingsGroup = new System.Windows.Forms.GroupBox();
            this.folderSettingDisable = new System.Windows.Forms.Button();
            this.folderSettingEnable = new System.Windows.Forms.Button();
            this.selectedFolderLabel = new System.Windows.Forms.Label();
            this.selectedFolderPath = new System.Windows.Forms.TextBox();
            this.backupDetailsGroup = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.backupPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backupEnabledCheck = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backupTreeView = new System.Windows.Forms.TreeView();
            this.hToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.hToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.setBackupLocationBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.driveEditorGroup.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.folderSettingsGroup.SuspendLayout();
            this.backupDetailsGroup.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // driveListView
            // 
            this.driveListView.Location = new System.Drawing.Point(12, 49);
            this.driveListView.Name = "driveListView";
            this.driveListView.Size = new System.Drawing.Size(271, 413);
            this.driveListView.TabIndex = 0;
            this.driveListView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.driveListView_BeforeCollapse);
            this.driveListView.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Drive List";
            // 
            // driveEditorGroup
            // 
            this.driveEditorGroup.Controls.Add(this.groupBox4);
            this.driveEditorGroup.Controls.Add(this.folderSettingsGroup);
            this.driveEditorGroup.Controls.Add(this.selectedFolderLabel);
            this.driveEditorGroup.Controls.Add(this.selectedFolderPath);
            this.driveEditorGroup.Controls.Add(this.backupDetailsGroup);
            this.driveEditorGroup.Controls.Add(this.label2);
            this.driveEditorGroup.Controls.Add(this.backupTreeView);
            this.driveEditorGroup.Enabled = false;
            this.driveEditorGroup.Location = new System.Drawing.Point(289, 33);
            this.driveEditorGroup.Name = "driveEditorGroup";
            this.driveEditorGroup.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.driveEditorGroup.Size = new System.Drawing.Size(688, 429);
            this.driveEditorGroup.TabIndex = 2;
            this.driveEditorGroup.TabStop = false;
            this.driveEditorGroup.Text = "Drive Editor";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.driveSettingDisableBackup);
            this.groupBox4.Controls.Add(this.driveSettingEnableBackup);
            this.groupBox4.Controls.Add(this.driveSettingBackupPath);
            this.groupBox4.Controls.Add(this.driveSettingSetup);
            this.groupBox4.Controls.Add(this.driveSettingForceBackup);
            this.groupBox4.Controls.Add(this.driveSettingDelete);
            this.groupBox4.Location = new System.Drawing.Point(335, 17);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(347, 80);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Drive Settings";
            // 
            // driveSettingDisableBackup
            // 
            this.driveSettingDisableBackup.Location = new System.Drawing.Point(238, 48);
            this.driveSettingDisableBackup.Name = "driveSettingDisableBackup";
            this.driveSettingDisableBackup.Size = new System.Drawing.Size(103, 24);
            this.driveSettingDisableBackup.TabIndex = 4;
            this.driveSettingDisableBackup.Text = "Disable Backup";
            this.driveSettingDisableBackup.UseVisualStyleBackColor = true;
            this.driveSettingDisableBackup.Click += new System.EventHandler(this.driveSettingDisableBackup_Click);
            // 
            // driveSettingEnableBackup
            // 
            this.driveSettingEnableBackup.Location = new System.Drawing.Point(130, 48);
            this.driveSettingEnableBackup.Name = "driveSettingEnableBackup";
            this.driveSettingEnableBackup.Size = new System.Drawing.Size(102, 24);
            this.driveSettingEnableBackup.TabIndex = 3;
            this.driveSettingEnableBackup.Text = "Enable Backup";
            this.driveSettingEnableBackup.UseVisualStyleBackColor = true;
            this.driveSettingEnableBackup.Click += new System.EventHandler(this.driveSettingEnableBackup_Click);
            // 
            // driveSettingBackupPath
            // 
            this.driveSettingBackupPath.Location = new System.Drawing.Point(9, 48);
            this.driveSettingBackupPath.Name = "driveSettingBackupPath";
            this.driveSettingBackupPath.Size = new System.Drawing.Size(115, 24);
            this.driveSettingBackupPath.TabIndex = 2;
            this.driveSettingBackupPath.Text = "Set Backup Location";
            this.driveSettingBackupPath.UseVisualStyleBackColor = true;
            this.driveSettingBackupPath.Click += new System.EventHandler(this.driveSettingBackupPath_Click);
            // 
            // driveSettingSetup
            // 
            this.driveSettingSetup.Location = new System.Drawing.Point(9, 19);
            this.driveSettingSetup.Name = "driveSettingSetup";
            this.driveSettingSetup.Size = new System.Drawing.Size(75, 23);
            this.driveSettingSetup.TabIndex = 2;
            this.driveSettingSetup.Text = "Setup";
            this.driveSettingSetup.UseVisualStyleBackColor = true;
            this.driveSettingSetup.Click += new System.EventHandler(this.driveSettingSetup_Click);
            // 
            // driveSettingForceBackup
            // 
            this.driveSettingForceBackup.Location = new System.Drawing.Point(90, 19);
            this.driveSettingForceBackup.Name = "driveSettingForceBackup";
            this.driveSettingForceBackup.Size = new System.Drawing.Size(111, 23);
            this.driveSettingForceBackup.TabIndex = 1;
            this.driveSettingForceBackup.Text = "Force Backup";
            this.driveSettingForceBackup.UseVisualStyleBackColor = true;
            this.driveSettingForceBackup.Click += new System.EventHandler(this.driveSettingForceBackup_Click);
            // 
            // driveSettingDelete
            // 
            this.driveSettingDelete.Location = new System.Drawing.Point(207, 19);
            this.driveSettingDelete.Name = "driveSettingDelete";
            this.driveSettingDelete.Size = new System.Drawing.Size(134, 23);
            this.driveSettingDelete.TabIndex = 0;
            this.driveSettingDelete.Text = "Delete Backup Data";
            this.driveSettingDelete.UseVisualStyleBackColor = true;
            this.driveSettingDelete.Click += new System.EventHandler(this.driveSettingDelete_Click);
            // 
            // folderSettingsGroup
            // 
            this.folderSettingsGroup.Controls.Add(this.folderSettingDisable);
            this.folderSettingsGroup.Controls.Add(this.folderSettingEnable);
            this.folderSettingsGroup.Location = new System.Drawing.Point(335, 234);
            this.folderSettingsGroup.Name = "folderSettingsGroup";
            this.folderSettingsGroup.Size = new System.Drawing.Size(347, 54);
            this.folderSettingsGroup.TabIndex = 5;
            this.folderSettingsGroup.TabStop = false;
            this.folderSettingsGroup.Text = "Folder Settings";
            // 
            // folderSettingDisable
            // 
            this.folderSettingDisable.Location = new System.Drawing.Point(127, 19);
            this.folderSettingDisable.Name = "folderSettingDisable";
            this.folderSettingDisable.Size = new System.Drawing.Size(115, 24);
            this.folderSettingDisable.TabIndex = 1;
            this.folderSettingDisable.Text = "Disable Backup";
            this.folderSettingDisable.UseVisualStyleBackColor = true;
            this.folderSettingDisable.Click += new System.EventHandler(this.folderSettingDisable_Click);
            // 
            // folderSettingEnable
            // 
            this.folderSettingEnable.Location = new System.Drawing.Point(6, 19);
            this.folderSettingEnable.Name = "folderSettingEnable";
            this.folderSettingEnable.Size = new System.Drawing.Size(115, 24);
            this.folderSettingEnable.TabIndex = 0;
            this.folderSettingEnable.Text = "Enable Backup";
            this.folderSettingEnable.UseVisualStyleBackColor = true;
            this.folderSettingEnable.Click += new System.EventHandler(this.folderSettingEnable_Click);
            // 
            // selectedFolderLabel
            // 
            this.selectedFolderLabel.AutoSize = true;
            this.selectedFolderLabel.Location = new System.Drawing.Point(338, 192);
            this.selectedFolderLabel.Name = "selectedFolderLabel";
            this.selectedFolderLabel.Size = new System.Drawing.Size(81, 13);
            this.selectedFolderLabel.TabIndex = 4;
            this.selectedFolderLabel.Text = "Selected Folder";
            // 
            // selectedFolderPath
            // 
            this.selectedFolderPath.Location = new System.Drawing.Point(338, 208);
            this.selectedFolderPath.Name = "selectedFolderPath";
            this.selectedFolderPath.ReadOnly = true;
            this.selectedFolderPath.Size = new System.Drawing.Size(347, 20);
            this.selectedFolderPath.TabIndex = 3;
            // 
            // backupDetailsGroup
            // 
            this.backupDetailsGroup.Controls.Add(this.label5);
            this.backupDetailsGroup.Controls.Add(this.backupPath);
            this.backupDetailsGroup.Controls.Add(this.label4);
            this.backupDetailsGroup.Controls.Add(this.backupEnabledCheck);
            this.backupDetailsGroup.Location = new System.Drawing.Point(335, 103);
            this.backupDetailsGroup.Name = "backupDetailsGroup";
            this.backupDetailsGroup.Size = new System.Drawing.Size(347, 86);
            this.backupDetailsGroup.TabIndex = 2;
            this.backupDetailsGroup.TabStop = false;
            this.backupDetailsGroup.Text = "Backup Details";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Backups Enabled";
            // 
            // backupPath
            // 
            this.backupPath.Location = new System.Drawing.Point(6, 55);
            this.backupPath.Name = "backupPath";
            this.backupPath.ReadOnly = true;
            this.backupPath.Size = new System.Drawing.Size(335, 20);
            this.backupPath.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Backup Location";
            // 
            // backupEnabledCheck
            // 
            this.backupEnabledCheck.AutoSize = true;
            this.backupEnabledCheck.Enabled = false;
            this.backupEnabledCheck.Location = new System.Drawing.Point(6, 19);
            this.backupEnabledCheck.Name = "backupEnabledCheck";
            this.backupEnabledCheck.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.backupEnabledCheck.Size = new System.Drawing.Size(15, 14);
            this.backupEnabledCheck.TabIndex = 0;
            this.backupEnabledCheck.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Selected Backup Folders";
            // 
            // backupTreeView
            // 
            this.backupTreeView.Location = new System.Drawing.Point(6, 32);
            this.backupTreeView.Name = "backupTreeView";
            this.backupTreeView.Size = new System.Drawing.Size(323, 391);
            this.backupTreeView.TabIndex = 0;
            this.backupTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.backupTreeView_BeforeExpand);
            this.backupTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.backupTreeView_AfterSelect);
            // 
            // hToolStripMenuItem1
            // 
            this.hToolStripMenuItem1.Name = "hToolStripMenuItem1";
            this.hToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem1.Text = "h";
            // 
            // hToolStripMenuItem2
            // 
            this.hToolStripMenuItem2.Name = "hToolStripMenuItem2";
            this.hToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem2.Text = "h";
            // 
            // hToolStripMenuItem3
            // 
            this.hToolStripMenuItem3.Name = "hToolStripMenuItem3";
            this.hToolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem3.Text = "h";
            // 
            // hToolStripMenuItem4
            // 
            this.hToolStripMenuItem4.Name = "hToolStripMenuItem4";
            this.hToolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem4.Text = "h";
            // 
            // hToolStripMenuItem5
            // 
            this.hToolStripMenuItem5.Name = "hToolStripMenuItem5";
            this.hToolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem5.Text = "h";
            // 
            // hToolStripMenuItem6
            // 
            this.hToolStripMenuItem6.Name = "hToolStripMenuItem6";
            this.hToolStripMenuItem6.Size = new System.Drawing.Size(180, 22);
            this.hToolStripMenuItem6.Text = "h";
            // 
            // setBackupLocationBrowser
            // 
            this.setBackupLocationBrowser.Description = "Select Backup Location";
            this.setBackupLocationBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(989, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItem1.Text = "Open...";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 469);
            this.Controls.Add(this.driveEditorGroup);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.driveListView);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FileSync";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.driveEditorGroup.ResumeLayout(false);
            this.driveEditorGroup.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.folderSettingsGroup.ResumeLayout(false);
            this.backupDetailsGroup.ResumeLayout(false);
            this.backupDetailsGroup.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView driveListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox driveEditorGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView backupTreeView;
        private System.Windows.Forms.GroupBox backupDetailsGroup;
        private System.Windows.Forms.Label selectedFolderLabel;
        private System.Windows.Forms.TextBox selectedFolderPath;
        private System.Windows.Forms.CheckBox backupEnabledCheck;
        private System.Windows.Forms.TextBox backupPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox folderSettingsGroup;
        private System.Windows.Forms.Button driveSettingBackupPath;
        private System.Windows.Forms.Button folderSettingDisable;
        private System.Windows.Forms.Button folderSettingEnable;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button driveSettingDelete;
        private System.Windows.Forms.Button driveSettingForceBackup;
        private System.Windows.Forms.Button driveSettingSetup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem hToolStripMenuItem6;
        private System.Windows.Forms.FolderBrowserDialog setBackupLocationBrowser;
        private System.Windows.Forms.Button driveSettingDisableBackup;
        private System.Windows.Forms.Button driveSettingEnableBackup;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

