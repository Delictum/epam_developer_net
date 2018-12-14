using ManagerCloud.BL;
using System;
using System.Configuration;
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
            _unity = new Unity(_directoryPath, fileNameFilter);
        }

        protected override void OnStart(string[] args)
        {
            LoggerHelper.AddInfoLog(eventLog, string.Join(string.Empty, "Start service - ", DateTime.Now));
            StartTaskWatcher();
            LoggerHelper.AddInfoLog(eventLog, string.Join(string.Empty, "Start file watcher - ", DateTime.Now));
        }

        protected override void OnStop()
        {
            LoggerHelper.AddInfoLog(eventLog, string.Join(string.Empty, "Stop service - ", DateTime.Now));
            eventLog.Dispose();
        }

        protected override void OnPause()
        {
            _unity.StopFileWatcher();
            LoggerHelper.AddInfoLog(eventLog, string.Join(string.Empty, "Pause service - ", DateTime.Now));
        }

        protected override void OnContinue()
        {
            StartTaskWatcher();
            LoggerHelper.AddInfoLog(eventLog, string.Join(string.Empty, "Resume service - ", DateTime.Now));
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
            while (true)
            {
                _unity.StartFileWatcher(_directoryPath);
            }
        }
    }
}
