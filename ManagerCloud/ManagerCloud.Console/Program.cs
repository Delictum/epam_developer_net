using System.Configuration;
using ManagerCloud.BL;

namespace ManagerCloud.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var directoryPath = ConfigurationManager.AppSettings["DirectoryPath"];
            var fileNameFilter = ConfigurationManager.AppSettings["FileNameFilter"];

            var u = new Unity(directoryPath, fileNameFilter);

            System.Console.WriteLine("Press \'q\' to quit the programm.");
            while (System.Console.Read() != 'q')
            {
            }
        }
    }
}
