using ContourBillingSystem;
using ContourBillingSystem.ComponentStation;
using ContoursOfTheInformationProvided;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ContoursOfTheInformationProvided.Identification;
using ServiceBillingSystem.CustomExceptions;
using ServiceBillingSystem.ManagementDecisions;
using ServiceBillingSystem.ProvisionOfInformation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ConsoleBillingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var fileLog = ConfigurationManager.AppSettings["FileLog"];
                var log = new ProgramLog(fileLog);
            }
            catch (DirectoryNotFoundException)
            {
                var exception =
                    new ConfigurationDirectoryNotFoundException(ConfigurationManager.AppSettings["FileLog"]);
                Console.WriteLine(exception.Message);
                throw exception;
            }

            IAdress vasinAdress = ContactAddressInformation.CreateNewAdress("Belarus", "Grodno", "Kleckova",
                new Tuple<int, char, char>(12, '/', 'A'), 3);

            IList<int> vasinListNumbers = new List<int>();
            vasinListNumbers.Add(654321);
            vasinListNumbers.Add(543210);

            IList<string> vasinListEmails = new List<string>();
            vasinListEmails.Add("654321@email.com");
            vasinListEmails.Add("543210@email.com");

            IContactInforamtion vasinaContactInforamtion =
                ContactAddressInformation.CreateNewContacts(vasinListNumbers, vasinListEmails);

            IFullName vasyasFullName = PersonalIdentification.CreateNewFullName("Vasya", "Pupkin", "Ivanavich");

            IPassportIdentification passportIdentification = PersonalIdentification.CreateNewPassportIdentification(
                vasyasFullName,
                "REPUBLIC OF BELARUS", new DateTime(1997, 10, 28), true, "3456797K000PB9", "REPUBLIC OF BELARUS");
            IPassportData passportData = PersonalIdentification.CreateNewPassportData(passportIdentification,
                "KH248248",
                "MINISTRY OF INTERNAL AFFAIRS", new DateTime(2015, 10, 28), new DateTime(2025, 10, 28));

            IInformationOfAnIndividual informationOfVasya =
                InformationProvided.CreateNewInformationOfAnIndividual(vasinAdress, vasinaContactInforamtion,
                    passportData);
            Console.WriteLine(informationOfVasya.ToString());

            //
            IBillingCompany billingCompany = CompanyManagement.CreateNewCompany("Company");
            IAdress billingCompanyAdress = ContactAddressInformation.CreateNewAdress("Belarus", "Grodno", "Kleckova",
                new Tuple<int, char, char>(10, '/', '2'), 3);
            CompanyManagement.ChangeAdress(billingCompany, billingCompanyAdress);

            IList<int> companyListNumbers = new List<int>();
            vasinListNumbers.Add(123456);
            vasinListNumbers.Add(234567);

            IList<string> companyListEmails = new List<string>();
            vasinListEmails.Add("123456@email.com");
            vasinListEmails.Add("234567@email.com");

            IContactInforamtion companyContactInforamtion =
                ContactAddressInformation.CreateNewContacts(companyListNumbers, companyListEmails);

            CompanyManagement.ChangeContactInforamtion(billingCompany, companyContactInforamtion);
            CompanyManagement.ChangeAboutLegalEntity(billingCompany, "Some company");
            StationManagment.CreateNewStation(billingCompany, CodecType.G711);

            try
            {
                StationManagment.AddPortRange(billingCompany.Stations[0], 2030, 2110);
            }
            catch (PortArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.WriteLine(StationManagment.OutputAllPorts(billingCompany.Stations[0]));


            IClient client = ContractManagement.CreateNewClient(informationOfVasya);

            IRate rate = ContractManagement.CreateNewRate("Tarif", 0.5);

            ISubscriberNumber subscriberNumber1 = ContractManagement.CreateNewSubscriberNumber(rate, 505050);
            ISubscriberNumber subscriberNumber2 = ContractManagement.CreateNewSubscriberNumber(rate, 525252);

            ContractManagement.MakeNewTerminalContract(billingCompany, client, subscriberNumber1);
            ContractManagement.MakeNewTerminalContract(billingCompany, client, subscriberNumber2);

            TerminalManagement.ConnectTerminal(billingCompany.Stations[0], client.Contracts[0].Terminal, 2030);
            Thread.Sleep(200);
            TerminalManagement.ConnectTerminal(billingCompany.Stations[0], client.Contracts[1].Terminal, 2100);
            Thread.Sleep(200);

            TerminalManagement.CallToSubscriber(billingCompany.Stations[0], client.Contracts[0].Terminal,
                client.Contracts[1].Terminal);
            Thread.Sleep(200);
            TerminalManagement.AnswerTheCall(billingCompany.Stations[0], client.Contracts[0].Terminal,
                client.Contracts[1].Terminal);
            Thread.Sleep(1200);
            TerminalManagement.CompleteCall(billingCompany.Stations[0], client.Contracts[0].Terminal,
                client.Contracts[1].Terminal);
            Thread.Sleep(200);

            TerminalManagement.DisconnectTerminal(billingCompany.Stations[0], client.Contracts[0].Terminal);
            Thread.Sleep(200);
            TerminalManagement.DisconnectTerminal(billingCompany.Stations[0], client.Contracts[1].Terminal);


            try
            {
                Console.WriteLine(CallManagement.ViewCallLog(client.Contracts[0].Terminal.SubscriberNumber, 30));
            }
            catch (CallLogArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
