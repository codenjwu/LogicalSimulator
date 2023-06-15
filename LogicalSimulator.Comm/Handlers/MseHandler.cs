using cViP.AA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace LogicalSimulator.Comm.Handlers
{
    public class MseHandler
    {
        bool isPeriod;
        double totalMilSeconds;
        bool dir = false;
        int interval = 5000;
        int movementAmount;
        long runTime = 0;
        static Lazy<MseHandler> handler = null;
        MseHandler(bool isPeriod, double totalMinutes, int interval, int movementAmount)
        {
            this.isPeriod = isPeriod;
            this.totalMilSeconds = totalMinutes * 60 * 1000;
            this.interval = interval;
            this.movementAmount = movementAmount;
        }
        public static MseHandler Instance(bool isPeriod = false, int minutes = 0, int interval = 5000, int movementAmout = 1)
        =>
           (handler ??= new Lazy<MseHandler>(() => new MseHandler(isPeriod, minutes, interval, movementAmout))).Value;
        
        public void OnStart()
        {
            System.Diagnostics.Debug.WriteLine("Started!!");
            if (!isPeriod)
            {
                EndlessMove();
            }
            else
                PeriodMove();
        }
        private void PeriodMove()
        {
            System.Timers.Timer mainTimer = new System.Timers.Timer()
            {
                AutoReset = false
            };

            System.Timers.Timer timer = new System.Timers.Timer()
            {
                AutoReset = true,
                Enabled = true,
                Interval = interval // mse move interval
            };
            System.Diagnostics.Debug.WriteLine("Period Movement Started!!");
            System.Diagnostics.Debug.WriteLine($"Run Time Set: {totalMilSeconds / 60 / 1000} minutes");
            mainTimer.Interval = totalMilSeconds;
            mainTimer.Elapsed += (sender, e) =>
            {
                timer.Stop();
                int existTime = 5;
                do
                {
                    System.Diagnostics.Debug.WriteLine($"Program will be exist in {existTime} seconds");
                    Thread.Sleep(1000);
                    existTime--;
                } while (existTime >= 0);
                Environment.Exit(0);
            };
            mainTimer.Start();
            Task.Run(() =>
            {
                System.Diagnostics.Debug.WriteLine($"Period Job Started!!");
                timer.Elapsed += timer_Elapsed;
                timer.Start();
            });
        }
        private void EndlessMove()
        {
            System.Diagnostics.Debug.WriteLine("Endless Movement Started!!");
            System.Timers.Timer timer = new System.Timers.Timer()
            {
                AutoReset = true,
                Enabled = true,
                Interval = interval // mse move interval
            };
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        void timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            MouseActive();
            dir = !dir;
            var code = dir ? "↑" : "↓";
            if (isPeriod)
            {
                totalMilSeconds -= interval;
                System.Diagnostics.Debug.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is active, time left: {(int)(totalMilSeconds / 1000 / 60)} minutes {(int)((totalMilSeconds / 1000) % 60)} seconds, mse direction: {code}");
            }
            else
            {
                runTime += interval;
                System.Diagnostics.Debug.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is active, total run time: {(int)(runTime / 1000 / 60)} minutes {(int)((runTime /1000) % 60)} seconds, mse direction: {code}");
            }
        }
        void MouseActive()
        {
            MseHelper.ShowCursor(false);
            if (dir)
                MseHelper.mouse_event(MseHelper.MOUSEEVENTF_WHEEL, 0, 0, movementAmount, 0);
            else
                MseHelper.mouse_event(MseHelper.MOUSEEVENTF_WHEEL, 0, 0, -movementAmount, 0);
        }
    }
}
