using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ManagerCloud.BL;

namespace ManagerCloud.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(String[] args)
        {

            if (args?.Length > 1 && args[0] == "console")
            {
                new Unity();
            }
            else
            {
                var servicesToRun = new ServiceBase[]
            {
                new ManagerCloud()
            };
            ServiceBase.Run(servicesToRun);
            }
            
        }
    }
}
