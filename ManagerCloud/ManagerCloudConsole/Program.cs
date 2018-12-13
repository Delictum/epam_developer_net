using BL.ManagerCloud;
using System;
using System.Configuration;

namespace ManagerCloudConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
            var fileNameFilter = ConfigurationManager.AppSettings["FileNameFilter"];

            var u = new Unity(directoryPath, fileNameFilter);

            Console.WriteLine("Press \'q\' to quit the programm.");
            while (Console.Read() != 'q')
            {
            }
        }
    }
}
