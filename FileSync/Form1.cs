using FileSync.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileSync
{
    public partial class Form1 : Form
    {

        private DriveInfo selectedDrive;
        private DirectoryNode selectedFolder;

        public Form1()
        {
            InitializeComponent();

            ImageList treeImages = new ImageList();

            Bitmap cross = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(cross);
            g.FillRectangle(new SolidBrush(Color.White), 0, 0, 16, 16);
            g.DrawImage(Resources.Cross.ToBitmap(), 0, 0, 16, 16);

            treeImages.Images.Add(cross);

            Bitmap tick = new Bitmap(16, 16);
            Graphics g2 = Graphics.FromImage(tick);
            g2.FillRectangle(new SolidBrush(Color.White), 0, 0, 16, 16);
            g2.DrawImage(Resources.Tick.ToBitmap(), 0, 0, 16, 16);
            treeImages.Images.Add(tick);

            Bitmap dash = new Bitmap(16, 16);
            Graphics g3 = Graphics.FromImage(dash);
            g3.FillRectangle(new SolidBrush(Color.White), 0, 0, 16, 16);
            g3.DrawImage(Resources.Dash.ToBitmap(), 0, 0, 16, 16);
            treeImages.Images.Add(dash);

            backupTreeView.ImageList = treeImages;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDriveList();
        }

        private void LoadDriveList()
        {
            driveListView.Nodes.Clear();
            foreach (KeyValuePair<DriveInfo,DriveBackupData> dat in Program.driveDataList)
            {
                DriveBackupData driveDat = dat.Value;
                DriveInfo drive = driveDat.drive;
                TreeNode tmp = new TreeNode();
                tmp.Text = "(" + drive.Name + ") " + drive.VolumeLabel;
                tmp.Tag = driveDat;

                if (driveDat.backupList != null && driveDat.backupList.Count > 0) 
                {
                    foreach (var v in driveDat.backupList)
                    {
                        TreeNode tmp2 = new TreeNode();
                        tmp2.Text = BackupUtil.ConvertRealPath(v,drive);
                        tmp.Nodes.Add(tmp2);
                    }
                }
                else
                {
                    TreeNode tmp2 = new TreeNode();
                    tmp2.Text = "No synced folders found";
                    tmp.Nodes.Add(tmp2);
                }
                driveListView.Nodes.Add(tmp);
            }

            driveListView.ExpandAll();
            UpdateUIData();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if(driveListView.SelectedNode.Tag is DriveBackupData)
            {
                selectedDrive = ((DriveBackupData)driveListView.SelectedNode.Tag).drive;
                PopulateBackupDriveTree();
            }
        }

        public void RefreshAllDataExternal()
        {
            this.Invoke((MethodInvoker) delegate
            {
                LoadDriveList();
                if (selectedDrive != null && !Program.driveDataList.ContainsKey(selectedDrive))
                {
                    selectedDrive = null;
                    selectedFolder = null;
                    PopulateBackupDriveTree();
                }
            });
        }

        private void PopulateBackupDriveTree()
        {
            backupTreeView.Nodes.Clear();
            if (!(selectedDrive == null || !selectedDrive.IsReady))
            {

                TreeNode root = new TreeNode();
                root.Text = selectedDrive.Name;

                DirectoryInfo directories = selectedDrive.RootDirectory;
                int imageIndex = 0;
                foreach (string inf in Program.driveDataList[selectedDrive].backupList)
                {
                    if (BackupUtil.ConvertDrivePath(directories.FullName) == inf)
                    {
                        imageIndex = 1;
                        break;
                    }

                    if (inf.Contains(BackupUtil.ConvertDrivePath(directories.FullName)))
                    {
                        imageIndex = 2;
                    }
                }

                root.ImageIndex = imageIndex;
                root.SelectedImageIndex = imageIndex;

                root.Tag = directories;

                if(CountSubDirectories(directories) != 0)
                {
                    TreeNode node = new TreeNode();
                    node.Text = "**Temp";
                    node.Tag = null;

                    root.Nodes.Add(node);
                }

                backupTreeView.Nodes.Add(root);

                root.Expand();
            }

            UpdateUIData();
        }

        private void UpdateUIData()
        {
            if(selectedDrive != null)
            {
                driveEditorGroup.Enabled = true;
            } else
            {
                driveEditorGroup.Enabled = false;
                return;
            }

            try
            {

                //DriveSettings Group
                driveSettingSetup.Enabled = !Program.driveDataList[selectedDrive].backupDataExists;
                driveSettingForceBackup.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                driveSettingDelete.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                driveSettingBackupPath.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                driveSettingEnableBackup.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                driveSettingDisableBackup.Visible = Program.driveDataList[selectedDrive].backupDataExists;

                driveSettingEnableBackup.Enabled = !Program.driveDataList[selectedDrive].backupEnabled;
                driveSettingDisableBackup.Enabled = Program.driveDataList[selectedDrive].backupEnabled;

                //BackupDetails Group
                backupDetailsGroup.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                if (Program.driveDataList[selectedDrive].backupDataExists)
                {
                    backupPath.Text = Program.driveDataList[selectedDrive].defaultBackupLocation + (selectedFolder != null && selectedFolder.FullName != null ? selectedFolder.FullName.Substring(2) : "");
                    backupEnabledCheck.Checked = Program.driveDataList[selectedDrive].backupEnabled;
                }

                //Folder Settings Group
                if (selectedFolder != null)
                {
                    folderSettingsGroup.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                    if (RecursiveNodeCheck(selectedFolder.node) == false)
                    {
                        folderSettingDisable.Enabled = Program.driveDataList[selectedDrive].backupList.Contains(BackupUtil.ConvertDrivePath(selectedFolder.FullName));
                        folderSettingEnable.Enabled = !Program.driveDataList[selectedDrive].backupList.Contains(BackupUtil.ConvertDrivePath(selectedFolder.FullName));
                    }
                    else
                    {
                        folderSettingEnable.Enabled = false;
                        folderSettingDisable.Enabled = false;
                    }
                    selectedFolderPath.Text = selectedFolder.FullName;
                }
                else
                {
                    selectedFolderPath.Text = "";
                    folderSettingsGroup.Visible = false;
                }

                //Selected Folder
                selectedFolderLabel.Visible = Program.driveDataList[selectedDrive].backupDataExists;
                selectedFolderPath.Visible = Program.driveDataList[selectedDrive].backupDataExists;
            } catch (System.Collections.Generic.KeyNotFoundException)
            {
                driveEditorGroup.Enabled = false;
            }

        }

        private void backupTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeCollection nodes = e.Node.Nodes;
            bool alreadyLoaded = true;

            foreach(TreeNode node in nodes)
            {
                if (node.Text == "**Temp" || node.Tag == null)
                {
                    node.Remove();
                    alreadyLoaded = false;
                    break;
                }
            }

            if(alreadyLoaded)
            {
                return;
            }

            foreach (DirectoryInfo dir in GetSubDirectories((DirectoryInfo)e.Node.Tag))
            {
                if (dir.Name == "__filesync")
                    continue;

                TreeNode tmp = new TreeNode(dir.Name,0,0);
                tmp.Tag = dir;

                if (CountSubDirectories(dir) != 0)
                {
                    TreeNode ph = new TreeNode();
                    ph.Text = "**Temp";
                    ph.Tag = null;

                    tmp.Nodes.Add(ph);
                }

                e.Node.Nodes.Add(tmp);

                //Recursively check (Needs to be after added to list)
                int imageIndex = 0;
                foreach (string inf in Program.driveDataList[selectedDrive].backupList)
                {
                    if (BackupUtil.ConvertDrivePath(dir.FullName) == inf || RecursiveNodeCheck(tmp))
                    {
                        imageIndex = 1;
                        break;
                    }

                    if (inf.Contains(BackupUtil.ConvertDrivePath(dir.FullName)))
                    {
                        imageIndex = 2;
                    }
                }

                tmp.ImageIndex = imageIndex;
                tmp.SelectedImageIndex = imageIndex;
            }
            
        }

        private int CountSubDirectories(DirectoryInfo dir)
        {
            try
            {
                return dir.GetDirectories().Length;
            } catch (System.UnauthorizedAccessException)
            {
                return 0;
            }
        }

        private DirectoryInfo[] GetSubDirectories(DirectoryInfo dir)
        {
            try
            {
                return dir.GetDirectories();
            } catch (System.UnauthorizedAccessException)
            {
                return new DirectoryInfo[0];
            }
        }

        private void driveSettingSetup_Click(object sender, EventArgs e)
        {
            DialogResult res = setBackupLocationBrowser.ShowDialog();

            if(res == DialogResult.OK && !string.IsNullOrWhiteSpace(setBackupLocationBrowser.SelectedPath))
            {
                //Create file and initial setup.

                DriveBackupData tmp = Program.driveDataList[selectedDrive];
                tmp.backupDataExists = true;
                tmp.defaultBackupLocation = setBackupLocationBrowser.SelectedPath;
                Program.driveDataList[selectedDrive] = tmp;
                Program.SaveDriveData(tmp);

                MessageBox.Show("Backup system has been set up for " + selectedDrive.Name, "Setup Complete");
            }

            LoadDriveList();
            UpdateUIData();
        }

        private void backupTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Debug.WriteLine(e.Node.Tag.GetType() + ":" + e.Node.Text);
            if(e.Node.Tag is DirectoryInfo)
            {
                DirectoryInfo dir = (DirectoryInfo)e.Node.Tag;
                selectedFolder = new DirectoryNode(dir,e.Node);
                UpdateUIData();
            }
        }

        

        private void folderSettingEnable_Click(object sender, EventArgs e)
        {
            if (RecursiveNodeCheck(selectedFolder.node) == false)
            {
                DriveBackupData tmp = Program.driveDataList[selectedDrive];
                tmp.backupList.RemoveAll(str => str.StartsWith(BackupUtil.ConvertDrivePath(selectedFolder.FullName)));
                tmp.backupList.Add(BackupUtil.ConvertDrivePath(selectedFolder.FullName));
                Program.driveDataList[selectedDrive] = tmp;
                Program.SaveDriveData(tmp);
                /*selectedFolder.ImageIndex(1);

                bool isExpanded = selectedFolder.node.IsExpanded;
                selectedFolder.node.Collapse();
                selectedFolder.node.Nodes.Clear();
                selectedFolder.node.Nodes.Add(new TreeNode("**Temp", 0, 0));
                if(isExpanded)
                    selectedFolder.node.Expand();*/

                selectedFolder = null;
                LoadDriveList();
                PopulateBackupDriveTree();
                UpdateUIData();
            }
        }

        private void folderSettingDisable_Click(object sender, EventArgs e)
        {
            if (RecursiveNodeCheck(selectedFolder.node) == false)
            {
                DriveBackupData tmp = Program.driveDataList[selectedDrive];
                tmp.backupList.Remove(BackupUtil.ConvertDrivePath(selectedFolder.FullName));
                Program.driveDataList[selectedDrive] = tmp;
                Program.SaveDriveData(tmp);
                /*selectedFolder.ImageIndex(0);

                bool isExpanded = selectedFolder.node.IsExpanded;
                selectedFolder.node.Collapse();
                selectedFolder.node.Nodes.Clear();
                selectedFolder.node.Nodes.Add(new TreeNode("**Temp", 0, 0));
                if (isExpanded)
                    selectedFolder.node.Expand();*/

                selectedFolder = null;
                LoadDriveList();
                PopulateBackupDriveTree();
                UpdateUIData();
            }
        }

        private bool RecursiveNodeCheck(TreeNode node)
        {
            if(node.Parent != null)
            {
                if(node.Parent.ImageIndex == 1)
                {
                    Debug.WriteLine(node.Parent.Text);
                    return true;
                }
                return RecursiveNodeCheck(node.Parent);
            } else
            {
                return false;
            }
        }

        private void driveSettingEnableBackup_Click(object sender, EventArgs e)
        {
            DriveBackupData tmp = Program.driveDataList[selectedDrive];
            tmp.backupEnabled = true;
            Program.driveDataList[selectedDrive] = tmp;
            Program.SaveDriveData(tmp);
            UpdateUIData();
        }

        private void driveSettingDisableBackup_Click(object sender, EventArgs e)
        {
            DriveBackupData tmp = Program.driveDataList[selectedDrive];
            tmp.backupEnabled = false;
            Program.driveDataList[selectedDrive] = tmp;
            Program.SaveDriveData(tmp);
            UpdateUIData();
        }

        private void driveSettingBackupPath_Click(object sender, EventArgs e)
        {
            DialogResult res = setBackupLocationBrowser.ShowDialog();

            if (res == DialogResult.OK && !string.IsNullOrWhiteSpace(setBackupLocationBrowser.SelectedPath))
            {
                DriveBackupData tmp = Program.driveDataList[selectedDrive];
                tmp.defaultBackupLocation = setBackupLocationBrowser.SelectedPath;
                Program.driveDataList[selectedDrive] = tmp;
                Program.SaveDriveData(tmp);
            }

            LoadDriveList();
            UpdateUIData();
        }

        private void driveSettingDelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to delete the backup data\nNote: This will leave the backup files on the computer, but just delete the configuration file.","Delete Config", MessageBoxButtons.YesNo);

            if(res == DialogResult.Yes)
            {
                DriveBackupData tmp = Program.driveDataList[selectedDrive];
                tmp.backupDataExists = false;
                tmp.backupEnabled = false;
                tmp.backupList.Clear();
                tmp.defaultBackupLocation = "";

                Program.driveDataList[selectedDrive] = tmp;

                string path = selectedDrive.RootDirectory + "__filesync" + "\\" + Program.GetCPUSerial() + ".cfg";
                if(File.Exists(path))
                {
                    File.Delete(path);
                } else
                {
                    MessageBox.Show("Could not find file!");
                }

                selectedFolder = null;
                LoadDriveList();
                PopulateBackupDriveTree();
                UpdateUIData();
            }
        }

        private async void driveSettingForceBackup_Click(object sender, EventArgs e)
        {
            await Program.StartBackup(Program.driveDataList[selectedDrive]);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.OpenSettingWindow();
        }

        private void driveListView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
