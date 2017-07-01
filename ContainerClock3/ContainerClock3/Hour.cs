using System;
using System.Media;
using System.Threading;

namespace ContainerClock3
{
    public class LessonManager
    {
        public LessonManager(Form1 frm)
        {
            //Test values until I get csv support
            lessons = new string[] { "RW", "RW", "RW", "Pause", "STL", "STL", "Mittag", "STL", "STL", "Pause", "STL", "STL" };
            lessonStartTimes = new DateTime[] { new DateTime(2017, 6, 28, 7, 45, 0), new DateTime(2017, 6, 28, 8, 35, 0), new DateTime(2017, 6, 28, 9, 25, 0), new DateTime(2017, 6, 28, 10, 15, 0), new DateTime(2017, 6, 28, 10, 30, 0), new DateTime(2017, 6, 28, 11, 20, 0), new DateTime(2017, 6, 28, 12, 10, 0), new DateTime(2017, 6, 28, 13, 10, 0), new DateTime(2017, 6, 28, 14, 00, 0), new DateTime(2017, 6, 28, 14, 50, 0), new DateTime(2017, 6, 28, 15, 00, 0), new DateTime(2017, 6, 28, 15, 50, 0) };

            this.Length = lessonStartTimes[HourCount + 1] - lessonStartTimes[HourCount];
            this.Name = lessons[HourCount];
         
             HourCount = 0;
            this.StartTime = lessonStartTimes[HourCount];
            UIForm = frm;
            UIForm.FillHourGP("--", lessons[HourCount], lessons[HourCount+1]);
        }

        //Properties
        public TimeSpan Length { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Name { get; private set; }
        Form1 UIForm;

        private string[,] timeTable;
        private string[] lessons;
        private DateTime[] lessonStartTimes;
        private int HourCount;

        Logger log = new Logger();

        //Methods
        private string GetTimeTableValue(int row, int col)
        {
            return timeTable[row, col];
        }

        public void endHour()
        {
           // playAudio();
            log.LogWrite("Lesson " + lessons[HourCount] + " ends at " + lessonStartTimes[HourCount+1]);
            
            HourCount++;
            
            this.Name = lessons[HourCount];
            this.Length = lessonStartTimes[HourCount + 1] - lessonStartTimes[HourCount];
            UIForm.rotateHourGP(lessons[HourCount+1]);
            //TODO: ENTFERNEN
            this.StartTime = lessonStartTimes[HourCount];
        }

        private void playAudio()
        {
            string currentTime = DateTime.Now.ToShortDateString();
            SoundPlayer schoolBell = new SoundPlayer(Properties.Resources.acdc);
            //TODO: import preference from file/reg 

            schoolBell.Play();
        }
        public void KeeptimeUpdated(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                //TODO: update TimeTable and update Clock... etc
                DateTime HourEndTime = this.StartTime.Add(this.Length);
                if(HourEndTime <= DateTime.Now)
                {
                    endHour();
                }
                Thread.Sleep(1000);
            }
        }
    }
}