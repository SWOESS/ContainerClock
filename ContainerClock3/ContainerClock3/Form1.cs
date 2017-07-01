using System;
using System.Windows.Forms;
using System.Threading;

namespace ContainerClock3
{
    public partial class Form1 : Form
    {
        //TODO: Import Timetable
        public CancellationTokenSource CancellationTokenSource { get; private set; }
       
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CancellationTokenSource.Cancel();
        }

        public Form1()
        {
            InitializeComponent();
            CancellationTokenSource = new CancellationTokenSource();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            LessonManager h = new LessonManager(this);
            Thread ClockThread = new Thread(() => h.KeeptimeUpdated(CancellationTokenSource.Token));
            ClockThread.Start();
            AnalogClock Clock = new AnalogClock(pictureBox1, 500);
        }
        public void rotateHourGP(string newHour)
        {
            Invoke(new Action(() =>
            {
                lastHourlabel.Text = currentHourlabel.Text;
                currentHourlabel.Text = nextHourlabel.Text;
                nextHourlabel.Text = newHour;
            }));
        }
        public void FillHourGP(string last, string curr, string next)
        {
            Invoke(new Action(() =>
            {
                lastHourlabel.Text = last;
                currentHourlabel.Text = curr;
                nextHourlabel.Text = next;
            }));
        }
        public void syncLabels(string start, string end)
        {
            HourStartLabel.Text = start;
            HourEndLabel.Text = end;
        }

    }
}
