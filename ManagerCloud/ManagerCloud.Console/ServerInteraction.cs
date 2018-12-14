using System.Linq;
using System.ServiceProcess;

namespace ManagerCloud.Console
{
    internal static class ServerInteraction
    {
        public static void StopService(string serviceName)
        {
            using (var controller = GetService(serviceName))
            {
                if (controller == null)
                {
                    return;
                }

                controller.Stop();
                controller.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        public static ServiceControllerStatus GetStatusService(string serviceName)
        {
            using (var controller = GetService(serviceName))
            {
                return controller.Status;
            }
        }

        public static ServiceController GetService(string serviceName)
        {
            var services = ServiceController.GetServices();
            return services.FirstOrDefault(x => x.ServiceName == serviceName);
        }
    }
}
