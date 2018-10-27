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
    public partial class SettingWindow : Form
    {
        public SettingWindow()
        {
            InitializeComponent();
            checkBox1.Checked = Properties.Settings.Default.ShowProgress;
            checkBox2.Checked = !Properties.Settings.Default.AskBeforeBackup;
            checkBox3.Checked = Properties.Settings.Default.AutoCloseProgress;
        }

        //ShowProgress
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ShowProgress = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        //Autosync
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AskBeforeBackup = !checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoCloseProgress = checkBox3.Checked;
            Properties.Settings.Default.Save();
        }
    }
}
