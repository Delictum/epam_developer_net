using ManagerCloud.BL;
using System;
using System.Configuration;
using System.ServiceProcess;

namespace ManagerCloud.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = ServerInteraction.GetService("ManagerCloud");
            if (service != null &&(service.Status == ServiceControllerStatus.Running || service.Status == ServiceControllerStatus.StartPending))
            {
                DisplayReferenceInformation.OfferCloseServer();
                if (System.Console.Read() != 'y')
                {
                    Environment.Exit(0);
                }

                try
                {
                    ServerInteraction.StopService("ManagerCloud");
                }
                catch (Exception)
                {
                    DisplayReferenceInformation.ExitConsole();
                    Environment.Exit(0);
                }
            }

            var directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
            var fileNameFilter = ConfigurationManager.AppSettings["FileNameFilter"];

            DisplayReferenceInformation.MeetWelcome();

            var u = new Unity(directoryPath, fileNameFilter);

            while (System.Console.Read() != 'q')
            {
            }
        }
    }
}
