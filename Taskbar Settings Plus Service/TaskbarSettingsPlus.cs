using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Taskbar_Settings_Plus_Service
{
    [RunInstaller(true)]
    public partial class TaskbarSettingsPlus : ServiceBase
    {
        ManagementEventWatcher startWatch, stopWatch;
        public TaskbarSettingsPlus()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
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

        protected override void OnStop()
        {
            startWatch.Stop();
            stopWatch.Stop();
        }

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
            using (StreamWriter streamWriter = new StreamWriter(@"C:\Temp\testing_tbsplus_log.txt", true))
            {
                streamWriter.WriteLine(msg);
                streamWriter.Close();
            }
        }
    }
}
