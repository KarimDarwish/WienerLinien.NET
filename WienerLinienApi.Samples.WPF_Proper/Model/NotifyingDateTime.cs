using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.ComponentModel;

namespace WienerLinienApi.Samples.WPF_Proper.Model
{
   public class NotifyingDateTime : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private DateTime _now;
        private String _timeString;

        public NotifyingDateTime()
        {
            _now = DateTime.Now;
            DispatcherTimer timer = new DispatcherTimer();  
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timer_Tick; 
            timer.Start();
        }

        public DateTime Now
        {
            get { return _now; }
            private set
            {                      
                _now = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Now"));
            }
        }

        public string TimeString
        {
            get { return _timeString; }
            private set
            {
                _timeString = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TimeString"));
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            Now = time;
            TimeString = String.Concat(time.Hour, ":", time.Minute, ":", time.Second);
        }

    }
}
