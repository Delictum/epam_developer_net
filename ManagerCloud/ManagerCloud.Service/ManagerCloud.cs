using System.Configuration;
using System.ServiceProcess;
using ManagerCloud.BL;

namespace ManagerCloud.Service
{
    public partial class ManagerCloud : ServiceBase
    {
        private readonly string _directoryPath;
        private Unity unity;

        public ManagerCloud()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;

            _directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
            var fileNameFilter = ConfigurationManager.AppSettings["FileNameFilter"];
            unity = new Unity(_directoryPath, fileNameFilter);
        }

        protected override void OnStart(string[] args)
        {
            unity.StartFileWatcher(_directoryPath);
        }

        protected override void OnStop()
        {
        }

        protected override void OnPause()
        {
            unity.StopFileWatcher();
        }

        protected override void OnContinue()
        {
            unity.StartFileWatcher(_directoryPath);
        }
    }
}
