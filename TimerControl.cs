using System.Windows.Forms;
using System;

namespace ElapsedTimer  {
    public class TimerControl:IDisposable {
        private Timer timer;
        public event EventHandler Elapsed;

        public TimerControl(){
            timer = new Timer();
            timer.Tick+=timer_Elapsed;
        }
        public double Interval {
            get {
                return timer.Interval / 1000;
            }
            set {
                timer.Interval = (int)(value * 1000);
            }
        }
        public bool Enabled{
            get{
                return timer.Enabled;
            } 
            set{
                timer.Enabled=value;
            }
        }
        public void Dispose() {
            timer.Dispose();
        }
        public void CallTimer() {
            /*double _interval = timer.Interval;
            bool   _enebled  = timer.Enabled;
            timer.Enabled = false;
            timer.AutoReset = false;
            timer.Interval = 1;
            System.Threading.Thread.Sleep(2);
            timer.AutoReset = true;
            timer.Interval = _interval;
            timer.Enabled = _enebled;*/
        }
        private void timer_Elapsed(object sender,EventArgs args) {
            if(Elapsed != null) {
                Elapsed(sender,args);
            }
        }
    }
}
