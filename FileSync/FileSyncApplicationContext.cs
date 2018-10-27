using FileSync.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSync
{
    class FileSyncApplicationContext : ApplicationContext
    {
        public NotifyIcon trayIcon;

        public FileSyncApplicationContext()
        {
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Open...",Open),
                    new MenuItem("Settings...",OpenSetting),
                    new MenuItem("View Progress...",OpenProgress),
                    new MenuItem("Sync All Drives",RunSync),
                    new MenuItem("Terminate Sync",TerminateBackup),
                    new MenuItem("Exit",Exit)
                }),
                Visible = true
            };
        }

        void Open(object sender, EventArgs e)
        {
            Program.OpenMainWindow();
        }

        void RunSync(object sender, EventArgs e)
        {
            Program.StartBackup();
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        void OpenProgress(object sender, EventArgs e)
        {
            Program.OpenProgressWindow();
        }

        void OpenSetting(object sender, EventArgs e)
        {
            Program.OpenSettingWindow();
        }

        void TerminateBackup(object sender, EventArgs e)
        {
            Program.TERMINATE_BACKUP = true;
        }
    }

    public static class FormUtils
    {
        private static Icon _defaultFormIcon;
        public static Icon DefaultFormIcon
        {
            get
            {
                if (_defaultFormIcon == null)
                    _defaultFormIcon = (Icon)typeof(Form).
                        GetProperty("DefaultIcon", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null, null);

                return _defaultFormIcon;
            }
        }
    }
}
