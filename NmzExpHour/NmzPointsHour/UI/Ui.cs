using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NmzPointsHour.ImageProcessing;
using NmzPointsHour.NightmareZone;
using NmzPointsHour.Utils;

namespace NmzPointsHour.UI
{
    public partial class Ui : Form
    {
        private readonly NMZSession nmzSession;
        private LabelDropShadow label1;
        private LabelDropShadow label2;

        public Ui(NMZSession session)
        {
            InitializeLabels();
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(2460, 170);

            nmzSession = session;
        }

        private void newDream_Click(object sender, EventArgs e)
        {
            nmzSession.StartNewDream();
            nmzSession.CurrentDream.MonsterKilled += MonksterKilled;
            label2.Text = "Dream " + nmzSession.CurrentDream.Id;
            timerPointsReader.Enabled = true;
            timerLabelUpdater.Enabled = true;
        }

        private void stopDream_Click(object sender, EventArgs e)
        {
            nmzSession.StopCurrentDream();
            label2.Text = "No Dream";
        }

        private void MonksterKilled(Dream m, EventArgs e)
        {
            var eM = (MonsterKilledArgs) e;

            UpdatePoints(eM.Points.ToString());
        }

        private void timerPointsReader_Tick(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            nmzSession.CurrentDream.UpdatePoints();
        }
        
        private void timerUiUpdater_Tick(object sender, EventArgs e)
        {
            label1.Text = "Points/hour:\n" + nmzSession.CurrentDream.Calculator.CalculatePointsPerHour().KiloFormat();
        }

        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MoveForm();
            }
        }

        private void MoveForm()
        {
            ReleaseCapture();
            SendMessage(Handle, 0xA1, 0x2, 0);
            fastButtonHider.Stop();
        }
        
        private void ButtonHiders_Tick(object sender, EventArgs e)
        {
            HideButtons();
            fastButtonHider.Stop();
        }

        private void HideButtons()
        {
            newDream.Visible = false;
            stopDream.Visible = false;
            moreInfo.Visible = false;
            label2.Visible = false;
        }

        private void ShowButtons()
        {
            newDream.Visible = true;
            stopDream.Visible = true;
            moreInfo.Visible = true;
            label2.Visible = true;
        }

        private void Ui_MouseEnter(object sender, EventArgs e)
        {
            ShowButtons();
            fastButtonHider.Stop();
        }

        private void Ui_MouseLeave(object sender, EventArgs e)
        {
            if (this == ActiveForm)
            {
                fastButtonHider.Start();
            }
            else
            {
                fastButtonHider.Start();
            }
        }

        private void Ui_Deactivate(object sender, EventArgs e)
        {
            HideButtons();
            fastButtonHider.Stop();
        }

        delegate void SetTextCallback(string points);
        private void UpdatePoints(string points)
        {
            if (this.label2.InvokeRequired)
            {
                SetTextCallback d = UpdatePoints;
                Invoke(d, points);
            }
            else
            {
                label2.Text = "+" + points;
                label2.Visible = true;
            }
        }


        private void InitializeLabels()
        {
            label1 = new LabelDropShadow();

            label1.AutoSize = true;
            label1.BackColor = NMZColors.Background;
            label1.ForeColor = NMZColors.Font;
            label1.Location = new Point(0, 18);
            label1.Size = new Size(70, 24);
            label1.Text = "Points/hour:\n0";
            label1.UseCompatibleTextRendering = true;
            label1.MouseDown += label_MouseDown;
            label1.MouseEnter += Ui_MouseEnter;
            label1.MouseLeave += Ui_MouseLeave;
            label1.Padding = new Padding(0, 2, 0, 0);

            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.AutoSize = true;


            label2 = new LabelDropShadow();
            label2.AutoSize = true;
            label2.BackColor = NMZColors.Background;
            label2.ForeColor = NMZColors.Font;
            label2.Location = new Point(0, 0);
            label2.Size = new Size(71, 19);
            label2.Text = "No Dream";
            label2.MouseDown += label_MouseDown;
            label2.MouseEnter += Ui_MouseEnter;
            label2.MouseEnter += Label2_SetTextToTimer;
            label2.MouseLeave += Ui_MouseLeave;
            label2.Padding = new Padding(0, 0, 0, 0);
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Visible = false;
            label2.AutoSize = false;

            Controls.Add(label1);
            Controls.Add(label2);
        }

        private void Label2_SetTextToTimer(object sender, EventArgs e)
        {
            if (nmzSession.CurrentDream != null && nmzSession.CurrentDream.IsActive())
            {
                TimeSpan t = TimeSpan.FromMilliseconds(nmzSession.CurrentDream.Duration);

                label2.Text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s", t.Hours, t.Minutes, t.Seconds);
            }
        }


        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void moreInfo_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
