namespace FileSync
{
    partial class ProgressWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.driveLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backupProcess = new FileSync.ProgressBarText();
            this.stepLabel = new System.Windows.Forms.Label();
            this.currentFileProgress = new FileSync.ProgressBarText();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Drive:";
            // 
            // driveLabel
            // 
            this.driveLabel.AutoSize = true;
            this.driveLabel.Location = new System.Drawing.Point(90, 9);
            this.driveLabel.Name = "driveLabel";
            this.driveLabel.Size = new System.Drawing.Size(33, 13);
            this.driveLabel.TabIndex = 1;
            this.driveLabel.Text = "None";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Current Step:";
            // 
            // backupProcess
            // 
            this.backupProcess.Location = new System.Drawing.Point(12, 47);
            this.backupProcess.Name = "backupProcess";
            this.backupProcess.Size = new System.Drawing.Size(437, 19);
            this.backupProcess.TabIndex = 5;
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Location = new System.Drawing.Point(87, 31);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(33, 13);
            this.stepLabel.TabIndex = 6;
            this.stepLabel.Text = "None";
            // 
            // currentFileProgress
            // 
            this.currentFileProgress.Location = new System.Drawing.Point(12, 72);
            this.currentFileProgress.Name = "currentFileProgress";
            this.currentFileProgress.Size = new System.Drawing.Size(437, 19);
            this.currentFileProgress.TabIndex = 7;
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 102);
            this.Controls.Add(this.currentFileProgress);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.driveLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.backupProcess);
            this.Name = "ProgressWindow";
            this.Text = "Backup Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label driveLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label stepLabel;
        private ProgressBarText backupProcess;
        private ProgressBarText currentFileProgress;
    }
}