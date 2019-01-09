using System.ComponentModel;
using System.ServiceProcess;

namespace ManagerCloud.Service
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();
            var serviceInstaller = new ServiceInstaller();
            var processInstaller = new ServiceProcessInstaller { Account = ServiceAccount.LocalSystem };

            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ManagerCloud";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
