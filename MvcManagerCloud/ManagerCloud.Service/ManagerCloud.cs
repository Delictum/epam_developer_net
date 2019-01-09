using ManagerCloud.BL;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using ManagerCloud.Core.Helpers;

namespace ManagerCloud.Service
{
    public partial class ManagerCloud : ServiceBase
    {
        private readonly string _directoryPath;
        private readonly Unity _unity;

        public ManagerCloud()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;

            _directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
            var fileNameFilter = ConfigurationManager.AppSettings["FileNameFilter"];
            _unity = new Unity();
        }

        protected override void OnStart(string[] args)
        {
            if (CheckProcessConsole())
            {
                LoggerHelper.AddErrorLog(eventLog, "The service cannot be started while the console application is running");
                Environment.Exit(0);
            }
            LoggerHelper.AddInfoLog(eventLog, "Start service");
            StartTaskWatcher();
            LoggerHelper.AddInfoLog(eventLog, "Start file watcher");
        }

        protected override void OnStop()
        {
            LoggerHelper.AddInfoLog(eventLog, "Stop service");
            eventLog.Dispose();
        }

        protected override void OnPause()
        {
            _unity.StopFileWatcher();
            LoggerHelper.AddInfoLog(eventLog, "Pause service");
        }

        protected override void OnContinue()
        {
            if (CheckProcessConsole())
            {
                LoggerHelper.AddErrorLog(eventLog, "The service cannot be started while the console application is running");
                Environment.Exit(0);
            }
            StartTaskWatcher();
            LoggerHelper.AddInfoLog(eventLog, "Resume service");
        }

        protected void StartTaskWatcher()
        {
            var thread = new Thread(StartWatcher)
            {
                IsBackground = true,
                Name = "StartWatcher"
            };
            thread.Start();
        }

        private void StartWatcher()
        {
            LoggerHelper.AddInfoLog(eventLog, "Start watcher in service");
            _unity.StartFileWatcher(_directoryPath);
            while (true)
            {
            }
        }

        private static bool CheckProcessConsole() => Process.GetProcessesByName("ManagerCloud.Console").Any();

    }
}
