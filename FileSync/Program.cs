using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using xxHashSharp;

namespace FileSync
{
    struct DriveBackupData
    {
        public DriveInfo drive;
        public bool backupEnabled;
        public bool backupDataExists;
        public List<string> backupList;
        public string defaultBackupLocation;

        public DriveBackupData(DriveInfo drive)
        {
            this.drive = drive;
            backupList = new List<string>();
            backupEnabled = false;
            backupDataExists = false;
            defaultBackupLocation = null;
        }

        public DriveBackupData(DriveInfo drive, string data)
        {
            this.drive = drive;

            backupList = ParseData(data);

            backupEnabled = false;
            backupDataExists = true;

            defaultBackupLocation = null;
        }

        public void SetData(string data)
        {
            backupList = ParseData(data);
        }


        public static List<string> ParseData(string data)
        {
            string[] list = data.Split('?');

            List<string> dat = new List<string>();

            foreach (string str in list)
            {
                if (str.Length == 0) continue;
                dat.Add(str);
            }

            return dat;
        }

        public override string ToString()
        {
            return String.Join("?",backupList.ToArray());
        }
    }

    static class Program
    {

        public static Dictionary<DriveInfo,DriveBackupData> driveDataList = new Dictionary<DriveInfo, DriveBackupData>();

        //public static SHA1 hashObject = new SHA1CryptoServiceProvider();
        public static xxHash hashObject = new xxHash();

        public static FileSyncApplicationContext noteTray;

        public static int BACKUP_PROGRESS = 0;
        public static string BACKUP_FILE = "";
        public static string BACKUP_DRIVE = "";
        public static string BACKUP_STATE = "";

        public static bool TERMINATE_BACKUP = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location)).Count() > 1)
            {
                MessageBox.Show("This application is already running");
                Process.GetCurrentProcess().Kill();
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            object path = key.GetValue("FileSyncAutorun");
            if(path == null || path.ToString() != Assembly.GetEntryAssembly().Location)
            {
                key.SetValue("FileSyncAutorun", Assembly.GetEntryAssembly().Location);
            }

            Debug.WriteLine(GetCPUSerial());
            
            Debug.WriteLine(driveDataList.Count);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            noteTray = new FileSyncApplicationContext();

            //Create event watcher
            ManagementEventWatcher watchDrives = new ManagementEventWatcher();
            WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 OR EventType = 3");
            watchDrives.EventArrived += new EventArrivedEventHandler((ob, target) => {
                /*foreach (var property in target.NewEvent.Properties)
                {
                    Debug.WriteLine(property.Name + " = " + property.Value);
                }*/
                RefreshDataSet();
            });
            watchDrives.Query = query;
            watchDrives.Start();

            RefreshDataSet();

            Application.Run(noteTray);
        }

        static Form1 mainWindow;
        static ProgressWindow progressWindow;
        static SettingWindow settingWindow;

        public static void OpenMainWindow()
        {
            if(mainWindow == null || mainWindow.IsDisposed)
            {
                mainWindow = new Form1();
                mainWindow.Visible = true;
            } else
            {
                mainWindow.Activate();
            }
        }

        public static void OpenProgressWindow()
        {
            if(progressWindow == null || progressWindow.IsDisposed)
            {
                progressWindow = new ProgressWindow();
                progressWindow.Visible = true;
            } else
            {
                progressWindow.Activate();
            }
        }

        public static void OpenSettingWindow()
        {
            if (settingWindow == null || settingWindow.IsDisposed)
            {
                settingWindow = new SettingWindow();
                settingWindow.Visible = true;
            }
            else
            {
                settingWindow.Activate();
            }
        }

        public static void ShowNotification(string title, string body)
        {
            noteTray.trayIcon.BalloonTipText = body;
            noteTray.trayIcon.BalloonTipTitle = title;
            noteTray.trayIcon.ShowBalloonTip(1000);
        }

        private static void RefreshDataSet()
        {
            var driveList = DriveInfo.GetDrives().Where(drive => drive.IsReady && drive.DriveType == DriveType.Removable);

            foreach (DriveInfo drive in driveList)
            {
                bool containsDrive = driveDataList.Any((val) => val.Value.drive.Name == drive.Name);

                if (!containsDrive)
                {
                    DriveBackupData driveDat = new DriveBackupData(drive);
                    string checkPath = drive.RootDirectory + "__filesync\\" + GetCPUSerial() + ".cfg";
                    if (File.Exists(checkPath))
                    {
                        StreamReader configData = new StreamReader(checkPath);

                        //Validate file (name contains CPUID)
                        /*
                         * Valid file config
                         * 0: [FILESYNC]
                         * 1: <DATA>
                         * 2: <ENABLED>
                         * 3: <DEFAULT BACKUP LOCATION> (Optional)
                         */

                        if (configData.ReadLine() == "[FILESYNC]")
                        {
                            driveDat.SetData(configData.ReadLine());

                            driveDat.backupEnabled = (configData.ReadLine() == "true" ? true : false);

                            driveDat.backupDataExists = true;

                            string defaultBackup = configData.ReadLine();
                            if (defaultBackup != null)
                                driveDat.defaultBackupLocation = defaultBackup;
                        }

                        configData.Close();
                    }

                    driveDataList.Add(driveDat.drive,driveDat);
                    if (driveDat.backupEnabled)
                    {
                        if (!Properties.Settings.Default.AskBeforeBackup)
                        {
                            new Task(async () => {
                                await StartBackup(driveDat);
                            }).Start();
                        }
                        else
                        {
                            ShowNotification("New Drive Inserted", driveDat.drive.Name + " is ready to be synced!");
                        }
                    }
                }
            }

            ValidateDataSet();

            if (!(mainWindow == null || mainWindow.IsDisposed))
            {
                mainWindow.RefreshAllDataExternal();
            }
        }

        public static async void StartBackup()
        {
            try
            {
                foreach (DriveBackupData dat in driveDataList.Values)
                {
                    bool end = await StartBackup(dat);
                    if (end) break;
                }
            } catch (InvalidOperationException e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
        }

        public static async Task<bool> StartBackup(DriveBackupData drive)
        {
            TERMINATE_BACKUP = false;
            try
            {
                ShowNotification("Backup Started", "A backup has started for " + drive.drive.Name + "...");
                if (Properties.Settings.Default.ShowProgress)
                {
                    OpenProgressWindow();
                }
                BACKUP_DRIVE = drive.drive.Name;
                BACKUP_PROGRESS = 0;
                BACKUP_STATE = "Collecting File Data...";
                BackupListBuilder build = new BackupListBuilder(drive);
                await build.Start(delegate
                {
                    BACKUP_STATE = "Starting Backup...";
                    List<BackupData> backupList = build.data;
                    int fileCount = 0;
                    int totalCount = 0;

                    long bytesWritten = 0;
                    foreach (BackupData dat in backupList)
                    {
                        if(TERMINATE_BACKUP)
                        {
                            BACKUP_STATE = "Terminated";
                            return;
                        }
                        BACKUP_STATE = "Syncing (" + totalCount + "/" + backupList.Count + ")...";
                        Debug.WriteLine(dat.fileHash);
                        Debug.WriteLine(dat.backupHash);
                        if (!(dat.fileHash == dat.backupHash))
                        {
                            BACKUP_FILE = "Syncing " + dat.filename;
                            bool ret = BackupUtil.BackupFile(dat, drive);
                            if (ret == false)
                            {
                                BACKUP_STATE = "FAILED";
                                return;
                            }
                            Debug.WriteLine("Synced " + dat.filename);
                            //BACKUP_FILE = "Syncing " + dat.filename;
                            fileCount++;
                        }
                        else
                        {
                            Debug.WriteLine("Skipped " + dat.filename);
                            BACKUP_FILE = "Skipping " + dat.filename;
                        }
                        totalCount++;
                        bytesWritten += dat.length;
                        BACKUP_PROGRESS = (int)((bytesWritten * 100) / build.totalCopySize);

                    }
                    BACKUP_PROGRESS = 100;
                    BACKUP_FILE = "Complete";
                    BACKUP_STATE = "Complete";
                    if (Properties.Settings.Default.AutoCloseProgress)
                    {
                        if (progressWindow != null && !progressWindow.IsDisposed)
                            progressWindow.Invoke((EventHandler)delegate
                       {
                                progressWindow.Close();
                            });
                    }
                    ShowNotification("Backup Finished", "The backup for " + drive.drive.Name + " has finished, " + fileCount + " files were synced.");
                });
                return true;
            }
            catch (FileNotFoundException e)
            {
                ThrowErrorMessage(e);
                return false;
            }
            catch (DirectoryNotFoundException e)
            {
                ThrowErrorMessage(e);
                return false;
            }
            catch (IOException e)
            {
                ThrowErrorMessage(e);
                return false;
            }
        }

        public static void ThrowErrorMessage(Exception e)
        {
            MessageBox.Show("Severe Error - " + e.Message + "\n\nWas the drive removed unexpectedly?\nCurrent sync operation has been cancelled.");
        }

        private static void ValidateDataSet()
        {
            List<DriveInfo> toRemove = new List<DriveInfo>();
            foreach (var driveDat in driveDataList)
            {
                if (!driveDat.Key.IsReady)
                {
                    toRemove.Add(driveDat.Key);
                }
            }

            foreach(DriveInfo dr in toRemove)
            {
                driveDataList.Remove(dr);
            }
        }

        public static string GetCPUSerial()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["processorID"].Value.ToString();
                break;
            }
            return cpuInfo;
        }

        public static string GenerateBackupDataFile()
        {
            string ret = "[FILESYNC]\n\nfalse\n";
            return ret;
        }
        
        public static string GenerateBackupDataFile(DriveBackupData dat)
        {
            string ret = "[FILESYNC]\n" + dat.ToString() + "\n" + dat.backupEnabled.ToString().ToLower() + "\n" + dat.defaultBackupLocation;
            return ret;
        }

        public static string GenerateBackupDataFile(string data, bool enabled, string path)
        {
            string ret = "[FILESYNC]\n" + data + "\n" + enabled.ToString().ToLower() + "\n" + path;
            return ret;
        }

        public static void SaveDriveData(DriveBackupData dat)
        {
            string path = dat.drive.RootDirectory + "__filesync";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }

            string writeFile = GenerateBackupDataFile(dat);
            try
            {
                File.WriteAllText(path + "\\" + GetCPUSerial() + ".cfg", writeFile);
            } catch (IOException e)
            {
                MessageBox.Show("Failed to update config: " + e);
            }
            
        }
    }
}
