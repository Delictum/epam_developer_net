using ContourBillingSystem;
using ContourBillingSystem.Contracts;
using ContoursOfTheInformationProvided;
using System.Collections.Generic;

namespace ModelBillingSystem
{
    public class Client : IClient
    {
        public IInformationOfAnIndividual CustomerInformation { get; }
        public IList<ITerminalContract> Contracts { get; set; }


        public Client(IInformationOfAnIndividual customerInformation)
        {
            CustomerInformation = customerInformation;
            Contracts = new List<ITerminalContract>();
        }

        public Client(IInformationOfAnIndividual customerInformation, IList<ITerminalContract> contracts)
        {
            CustomerInformation = customerInformation;
            Contracts = contracts;
        }
    }
}
