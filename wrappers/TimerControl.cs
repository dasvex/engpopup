using Timer = System.Timers.Timer;
using ElapsedEventArgs = System.Timers.ElapsedEventArgs;
using System;

namespace ElapsedTimer  {
    public class TimerControl:IDisposable {
        private Timer timer;
        public event EventHandler Elapsed;

        public TimerControl(){
            timer = new Timer();
            timer.Elapsed+=timer_Elapsed;
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
        public void Stop() {
            this.timer.Stop();
        }
        public void Start(){
            this.timer.Start();
        }
        public void CallTimer() {
        }
        private void timer_Elapsed(object sender,ElapsedEventArgs args) {
            if(Elapsed != null) {
                Elapsed(sender,args);
            }
        }
    }
}
