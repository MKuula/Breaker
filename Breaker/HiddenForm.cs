using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Breaker
{
    public partial class HiddenForm : Form
    {
        private bool rapidMessage = false;
        private int ticksNum = 0;   // Spent time at the computer (after last arrival)

        [DllImport("user32.dll")]
        private static extern void SetParent(IntPtr window, int value);

        private const int HWND_MESSAGE = -3;

        public HiddenForm()
        {
            InitializeComponent();
            SetParent(this.Handle, HWND_MESSAGE);
        }

        private void HiddenForm_Load(object sender, EventArgs e)
        {
            notifyIcon.Icon = Properties.Resources.Reminder;
            Microsoft.Win32.SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
            UpdateNotificationToolTipText();
        }

        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock || e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock)
            {
                timer.Stop();
                helpTimer.Stop();
                timer.Interval = 1800000;

                ticksNum = 0;   // Reset the number about the time spent at the computer
                UpdateNotificationToolTipText();

                // Next time show a normal message
                rapidMessage = false;
            }

            if (e.Reason == Microsoft.Win32.SessionSwitchReason.SessionUnlock)
            {
                timer.Start();
                helpTimer.Start();
            }

            isActiveToolStripMenuItem.Checked = timer.Enabled;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!rapidMessage)
                notifyIcon.ShowBalloonTip(10000, "Take a break", "It is the time to take a break. Please leave your computer now.", ToolTipIcon.Info);
            else
                notifyIcon.ShowBalloonTip(10000, "Take a break", "Keep the break as soon as possible!", ToolTipIcon.Warning);

            rapidMessage = true;
            timer.Interval = 600000;
            timer.Start();
        }

        private void breakerIsActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = isActiveToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void helpTimer_Tick(object sender, EventArgs e)
        {
            ticksNum++;
            UpdateNotificationToolTipText();
            helpTimer.Start();
        }

        private void UpdateNotificationToolTipText()
        {
            if (ticksNum != 1)
                notifyIcon.Text = string.Format("You have spent {0} minutes at the computer", ticksNum);
            else
                notifyIcon.Text = string.Format("You have spent {0} minute at the computer", ticksNum);
        }
    }
}
