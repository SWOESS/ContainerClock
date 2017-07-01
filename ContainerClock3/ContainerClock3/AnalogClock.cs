using System;
using System.Drawing;
using System.Windows.Forms;

namespace ContainerClock3
{
    class AnalogClock
    {
        public AnalogClock(PictureBox pb, int interv)
        {
            this.Width = 300;
            this.Height = 300;
            this.SecHand = 140;
            this.MinHand = 110;
            this.HourHand = 80;
            this.ParentBox = pb;
            UpdateInterval = interv;
            this.AnalogTimer = new Timer();
            this.AnalogTimer.Tick += AnalogTimerTick;
            this.AnalogTimer.Interval = UpdateInterval;
            AnalogBitmap = new Bitmap(this.Width+1, this.Height+1);
            CenterX = Width / 2;
            CenterY = Height / 2;
        }
        public int SS { get; private set; }
        public int MM { get; private set; }
        public int HH { get; private set; }
        public Color BackColor { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        public int SecHand { get; set; }
        public int MinHand { get; set; }
        public int HourHand { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int UpdateInterval { get; set; }
        public Timer AnalogTimer { get; set; }
        public Bitmap AnalogBitmap { get; set; }
        public Graphics AnalogGraphics { get; set; }
        public PictureBox ParentBox { get; set; }

        public void AnalogTimerTick(object sender, EventArgs e)
        {
            try
            {
                this.AnalogGraphics = Graphics.FromImage(this.AnalogBitmap);
                SS = DateTime.Now.Second;
                MM = DateTime.Now.Minute;
                HH = DateTime.Now.Hour;
                int[] handCoord = new int[2];
                AnalogGraphics.Clear(this.BackColor);

                AnalogGraphics.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, this.Width, this.Height);

                AnalogGraphics.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
                AnalogGraphics.DrawString("3", new Font("Arial", 12), Brushes.Black, new PointF(286, 140));
                AnalogGraphics.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
                AnalogGraphics.DrawString("9", new Font("Arial", 12), Brushes.Black, new PointF(0, 140));

                handCoord = msCoord(SS, SecHand);
                AnalogGraphics.DrawLine(new Pen(Color.Red, 1f), new Point(CenterX, CenterY), new Point(handCoord[0], handCoord[1]));

                handCoord = msCoord(MM, MinHand);
                AnalogGraphics.DrawLine(new Pen(Color.Black, 2f), new Point(CenterX, CenterY), new Point(handCoord[0], handCoord[1]));

                handCoord = hrCoords(HH / 12, MM, HourHand);
                AnalogGraphics.DrawLine(new Pen(Color.Gray, 3f), new Point(CenterX, CenterY), new Point(handCoord[0], handCoord[1]));

                ParentBox.Image = AnalogBitmap;

                AnalogBitmap.Dispose();
            }
            catch (Exception ex)
            {
                Logger l = new Logger();
                l.LogWrite(ex.Message);
            }
           
        }

        private int[] msCoord(int val, int hlen)
        {
            int[] coord = new int[2];
            val *= 6;

            if (val>=0 && val<180)
            {
                coord[0] = CenterX + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = CenterY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = CenterX - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = CenterY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;
        }
        private int[] hrCoords(int hval, int mval, int hlen)
        {
            int[] coord = new int[2];

            int val = (int)((hval * 30) + (mval * 0.5));
            if (val >=0 && val<180)
            {
                coord[0] = CenterX + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = CenterY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            else
            {
                coord[0] = CenterX - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = CenterY - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord; 
        }
        public void Start()
        {
            this.AnalogTimer.Start();
        }
    }
}
