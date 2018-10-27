using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow()
        {
            InitializeComponent();

            Timer timer = new Timer();
            timer.Interval = 1;

            timer.Tick += (delegate {
                if(IsDisposed)
                {
                    return;
                }
                backupProcess.Value = Program.BACKUP_PROGRESS;
                if(backupProcess.DisplayText != Program.BACKUP_FILE)
                {
                    backupProcess.DisplayText = Program.BACKUP_FILE;
                    backupProcess.Refresh();
                }
                driveLabel.Text = Program.BACKUP_DRIVE;
                stepLabel.Text = Program.BACKUP_STATE;

                currentFileProgress.Value = BackupUtil.BACKUP_PROGRESS_CURRENTFILE;
                //float speedmb = (((float)BackupUtil.BACKUP_BYTESPROCESSED_CURRENTFILE / 1000000) / ((DateTime.Now - BackupUtil.BACKUP_START_CURRENTFILE).Seconds));
                string progval = BackupUtil.BACKUP_PROGRESS_CURRENTFILE + "%";
                if(currentFileProgress.DisplayText != progval)
                {
                    currentFileProgress.DisplayText = progval;
                    currentFileProgress.Refresh();
                }
            });
            timer.Start();
        }
    }
}
