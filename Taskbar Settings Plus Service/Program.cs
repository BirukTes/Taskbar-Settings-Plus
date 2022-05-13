using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Taskbar_Settings_Plus_Service
{
    class Program
    {
        ManagementEventWatcher startWatch, stopWatch;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            // Requests all instance creation events,
            // with a specified latency of
            // 10 seconds. The query created
            // is "SELECT * FROM __InstanceCreationEvent WITHIN 10"
            WqlEventQuery q = new WqlEventQuery("__InstanceCreationEvent",
                new TimeSpan(0, 0, 10));

            Console.WriteLine("hello: " + q.QueryString);
            Console.ReadKey();
        }
        private  void OnStart()
        {
            startWatch = new ManagementEventWatcher(
new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
            startWatch.EventArrived += new EventArrivedEventHandler(startWatch_EventArrived);
            startWatch.Start();

            stopWatch = new ManagementEventWatcher(
     new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace"));
            stopWatch.EventArrived += new EventArrivedEventHandler(stopWatch_EventArrived);
            stopWatch.Start();
        }

        //protected override void OnStop()
        //{
        //    startWatch.Stop();
        //    stopWatch.Stop();
        //}

        private void stopWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            //Console.WriteLine("Process stopped: {0}", e.NewEvent.Properties["ProcessName"].Value);
            writemsg(String.Format("Process stopped: {0} at {1}", e.NewEvent.Properties["ProcessName"].Value,
                DateTime.Now));
        }

        private void startWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            writemsg(String.Format("Process started: {0} at {1}", e.NewEvent.Properties["ProcessName"].Value,
                DateTime.Now));
        }

        private void writemsg(string msg)
        {
            // true: for append to file
            //using (StreamWriter streamWriter = new StreamWriter(@"C:\Temp\testing_tbsplus_log.txt", true))
            //{
            //    streamWriter.WriteLine(msg);
            //    streamWriter.Close();
            //}

            Console.WriteLine(msg);
        }

        // new process arrived
    //    private void NewProcessEventHandler(object sender, EventArrivedEventArgs eventArgs)
    //    {
    //        int processId = GetProcessId(eventArgs);
    //        Task.Run(() => {
    //            // block event handling for new windows
    //            handlingNewProcess.Reset();
    //            // check some time, while there is no mainWindowTitle set (it can take some time for the application to set mainWindowTitle)
    //            for (int i = 0; i < 30; i++)
    //            {
    //                try { if (Process.GetProcessById(processId).MainWindowTitle.Length > 0) break; }
    //                // nothing to do if the process has disappeared (sometimes there are processes starting that are of a short lifetime) > return
    //                catch { return; }
    //                Thread.Sleep(150);
    //            }

    //            // give the process some more time to initialize (or disappear)
    //            Thread.Sleep(300);
    //            // first thread puts a lock here, following threads wait here until the first thread has released this lock (this lock exists in 1(+?) other place)
    //            lock (accessTaskbarButtons)
    //            {
    //                // attempt to add the taskbarButton, if added highlight the button
    //                if (AddTaskbarButton(processId)) { SetButtonHighlighted(taskbarButtons.Count - 1); }
    //            }
    //            // allow event handling for new windows
    //            handlingNewProcess.Set();
    //        });
    //    }

    //    private void StopProcessEventHandler(object sender, EventArrivedEventArgs eventArgs)
    //    {
    //        // first thread puts a lock here, following threads wait here until the first thread has released this lock (this lock exists in 1(+?) other place)
    //        lock (accessTaskbarButtons)
    //        {
    //            for (int buttonNr = 0; buttonNr < taskbarButtons.Count; buttonNr++)
    //            {
    //                // find the concerned taskbarButton by checking the process-id
    //                if (GetProcessId(eventArgs) == taskbarButtons[buttonNr].ProcessId)
    //                {
    //                    RemoveTaskbarButton(buttonNr);
    //                    break;
    //                }
    //            }
    //        }
    //    }
    }
}
