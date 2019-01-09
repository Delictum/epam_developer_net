using ManagerCloud.BL;
using System;
using System.ServiceProcess;

namespace ManagerCloud.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Console.Title = "Manager cloud";
            var service = ServerInteraction.GetService("ManagerCloud");
            if (service != null && (service.Status == ServiceControllerStatus.Running ||
                                    service.Status == ServiceControllerStatus.StartPending))
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

            DisplayReferenceInformation.MeetWelcome();

            var u = new Unity();
            
            while (System.Console.Read() != 'q')
            {
            }
        }
    }
}
