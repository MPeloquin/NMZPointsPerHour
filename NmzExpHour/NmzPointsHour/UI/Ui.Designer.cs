using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using NmzPointsHour.ImageProcessing;

namespace NmzPointsHour.UI
{
    partial class Ui
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
            this.timerPointsReader = new System.Windows.Forms.Timer(this.components);
            this.timerLabelUpdater = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.moreInfo = new System.Windows.Forms.Button();
            this.stopDream = new System.Windows.Forms.Button();
            this.newDream = new System.Windows.Forms.Button();
            this.fastButtonHider = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timerPointsReader
            // 
            this.timerPointsReader.Interval = 5000;
            this.timerPointsReader.Tick += new System.EventHandler(this.timerPointsReader_Tick);
            // 
            // timerLabelUpdater
            // 
            this.timerLabelUpdater.Interval = 10;
            this.timerLabelUpdater.Tick += new System.EventHandler(this.timerUiUpdater_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // moreInfo
            // 
            this.moreInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(47)))));
            this.moreInfo.BackgroundImage = global::NmzPointsHour.Properties.Resources.more;
            this.moreInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.moreInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.moreInfo.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(16)))));
            this.moreInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(48)))));
            this.moreInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(17)))));
            this.moreInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moreInfo.ForeColor = System.Drawing.Color.Transparent;
            this.moreInfo.Location = new System.Drawing.Point(47, 49);
            this.moreInfo.Name = "moreInfo";
            this.moreInfo.Size = new System.Drawing.Size(24, 22);
            this.moreInfo.TabIndex = 3;
            this.moreInfo.UseVisualStyleBackColor = false;
            this.moreInfo.Visible = false;
            this.moreInfo.Click += new System.EventHandler(this.moreInfo_Click);
            this.moreInfo.MouseEnter += new System.EventHandler(this.Ui_MouseEnter);
            this.moreInfo.MouseLeave += new System.EventHandler(this.Ui_MouseLeave);
            // 
            // stopDream
            // 
            this.stopDream.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(47)))));
            this.stopDream.BackgroundImage = global::NmzPointsHour.Properties.Resources.stop;
            this.stopDream.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.stopDream.Cursor = System.Windows.Forms.Cursors.Default;
            this.stopDream.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(16)))));
            this.stopDream.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(48)))));
            this.stopDream.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(17)))));
            this.stopDream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopDream.ForeColor = System.Drawing.Color.Transparent;
            this.stopDream.Location = new System.Drawing.Point(23, 49);
            this.stopDream.Name = "stopDream";
            this.stopDream.Size = new System.Drawing.Size(25, 22);
            this.stopDream.TabIndex = 2;
            this.stopDream.UseVisualStyleBackColor = false;
            this.stopDream.Visible = false;
            this.stopDream.Click += new System.EventHandler(this.stopDream_Click);
            this.stopDream.MouseEnter += new System.EventHandler(this.Ui_MouseEnter);
            this.stopDream.MouseLeave += new System.EventHandler(this.Ui_MouseLeave);
            // 
            // newDream
            // 
            this.newDream.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(47)))));
            this.newDream.BackgroundImage = global::NmzPointsHour.Properties.Resources.play;
            this.newDream.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.newDream.Cursor = System.Windows.Forms.Cursors.Default;
            this.newDream.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(16)))));
            this.newDream.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(93)))), ((int)(((byte)(72)))), ((int)(((byte)(47)))));
            this.newDream.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(70)))), ((int)(((byte)(16)))));
            this.newDream.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.newDream.ForeColor = System.Drawing.Color.Transparent;
            this.newDream.Location = new System.Drawing.Point(0, 49);
            this.newDream.Name = "newDream";
            this.newDream.Size = new System.Drawing.Size(24, 22);
            this.newDream.TabIndex = 1;
            this.newDream.UseVisualStyleBackColor = false;
            this.newDream.Visible = false;
            this.newDream.Click += new System.EventHandler(this.newDream_Click);
            this.newDream.MouseEnter += new System.EventHandler(this.Ui_MouseEnter);
            this.newDream.MouseLeave += new System.EventHandler(this.Ui_MouseLeave);
            // 
            // fastButtonHider
            // 
            this.fastButtonHider.Tick += new System.EventHandler(this.ButtonHiders_Tick);
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(327, 114);
            this.Controls.Add(this.moreInfo);
            this.Controls.Add(this.stopDream);
            this.Controls.Add(this.newDream);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Ui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Deactivate += new System.EventHandler(this.Ui_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newDream;
        private System.Windows.Forms.Timer timerPointsReader;
        private System.Windows.Forms.Timer timerLabelUpdater;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button stopDream;
        private System.Windows.Forms.Button moreInfo;
        private System.Windows.Forms.Timer fastButtonHider;
    }
}

