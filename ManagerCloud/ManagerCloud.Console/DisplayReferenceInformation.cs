using System;

namespace ManagerCloud.Console
{
    internal static class DisplayReferenceInformation
    {
        public static void MeetWelcome()
        {
            System.Console.WriteLine("Welcome to 'Manager cloud'! To exit the program, enter 'q'.");
        }

        public static void OfferCloseServer()
        {
            System.Console.WriteLine("The server is currently running. Press 'y' to stop the service, or any other key to close the program.");
        }

        public static void ExitConsole()
        {
            System.Console.WriteLine("Required to run the program as an administrator. Service is not closed, further program execution is impossible.");
            System.Console.ReadKey();
        }
    }
}
