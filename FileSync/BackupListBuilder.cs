using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    struct BackupData
    {
        public uint backupHash;
        public uint fileHash;

        public string path;
        public string filename;

        public long length;

        public BackupData(uint fileHash, uint backupHash, string path, string filename, long length)
        {
            this.backupHash = backupHash;
            this.fileHash = fileHash;
            this.path = path;
            this.filename = filename;
            this.length = length;
        }
    }

    class BackupListBuilder
    {
        public List<BackupData> data;
        private DriveBackupData driveData;
        bool isComplete = false;
        public long totalCopySize = 0;
        int progress = 0;

        public BackupListBuilder(DriveBackupData driveData)
        {
            this.driveData = driveData;
            this.data = new List<BackupData>();

            Debug.WriteLine("Create");
        }

        

        public async Task Start(MethodInvoker method)
        {
            Debug.WriteLine("Start");
            await Task.Run(() => {
                try
                {
                    int c = 0;
                    foreach (string path in driveData.backupList)
                    {
                        if(Program.TERMINATE_BACKUP)
                        {
                            Program.BACKUP_STATE = "Terminated";
                            return;
                        }
                        c++;
                        DirectoryInfo inf = new DirectoryInfo(BackupUtil.ConvertRealPath(path,driveData.drive));
                        if (!inf.Exists)
                            continue;

                        List<BackupData> dat = GetBackupData(inf, null);
                        data = data.Concat(dat).ToList();
                        progress = ((c * 100) / driveData.backupList.Count);
                        Program.BACKUP_PROGRESS = progress;

                    }
                    Debug.WriteLine("Complete" + data.Count);
                    method();
                    isComplete = true;
                } catch (FileNotFoundException e)
                {
                    Program.ThrowErrorMessage(e);
                    return;
                } catch (IOException e)
                {
                    Program.ThrowErrorMessage(e);
                    return;
                } 
            });
        }

        int totalItems = 0;
        int scannedItems = 0;

        private List<BackupData> GetBackupData(DirectoryInfo dir, List<BackupData> dat)
        {
            if(dat == null)
            {
                dat = new List<BackupData>();
            }

            if (Program.TERMINATE_BACKUP)
            {
                Program.BACKUP_STATE = "Terminated";
                return new List<BackupData>();
            }

            try
            {
                FileInfo[] files = dir.GetFiles();
                totalItems += files.Length;
                foreach(FileInfo file in files)
                {
                    Program.BACKUP_FILE = file.FullName;
                    if (Program.TERMINATE_BACKUP)
                    {
                        Program.BACKUP_STATE = "Terminated";
                        return new List<BackupData>();
                    }
                    BackupData backup = new BackupData();
                    bool sizeDiff = false;
                    totalCopySize += file.Length;
                    backup.filename = file.Name;
                    backup.path = file.DirectoryName;
                    backup.length = file.Length;
                    FileInfo corr = BackupUtil.FindCorrespondingFile(backup, driveData);
                    backup.fileHash = BackupUtil.ComputeHash(file);
                    if(!sizeDiff && corr != null && corr.Exists)
                    {
                        backup.backupHash = BackupUtil.FindUncompressedHash(corr);
                    }
                    scannedItems++;
                    Program.BACKUP_PROGRESS = (scannedItems * 100) / totalItems;
                    dat.Add(backup);
                    Program.BACKUP_STATE = "Collecting file data... (" + dat.Count + ")";
                }

            } catch (System.UnauthorizedAccessException)
            {
            }

            try
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                totalItems += dirs.Length;
                foreach (DirectoryInfo val in dirs)
                {
                    if (Program.TERMINATE_BACKUP)
                    {
                        Program.BACKUP_STATE = "Terminated";
                        return new List<BackupData>();
                    }

                    if (val.Name == "__filesync")
                        continue;

                    scannedItems++;
                    Program.BACKUP_PROGRESS = (scannedItems * 100) / totalItems;
                    dat = GetBackupData(val, dat);
                }
            } catch (System.UnauthorizedAccessException)
            {
            }
            

            return dat;
        }
    }
}
