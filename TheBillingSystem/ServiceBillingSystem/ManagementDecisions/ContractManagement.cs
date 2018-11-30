using ContourBillingSystem;
using ContourBillingSystem.Contracts;
using ContourBillingSystem.Terminal;
using ContoursOfTheInformationProvided;
using ModelBillingSystem.Contracts;
using ModelBillingSystem.Terminal;
using System;
using System.Collections.Generic;
using ModelBillingSystem;

namespace ServiceBillingSystem.ManagementDecisions
{
    public static class ContractManagement
    {
        public static IClient CreateNewClient(IInformationOfAnIndividual informationOfAnIndividual)
        {
            return new Client(informationOfAnIndividual, new List<ITerminalContract>());
        }

        public static ITerminalContract MakeNewTerminalContract(IBillingCompany billingCompany, IClient client, ISubscriberNumber subscriberNumber)
        {
            ITerminalModel model = new TerminalModel("ModelName", new[] { "ru", "eng" }, new Tuple<double, double>(3.14, 3.14), true);

            ITerminal terminal = new Terminal(model, subscriberNumber);
            
            var contract = new TerminalContract(terminal, new Tuple<IBillingCompany, IClient>(billingCompany, client));

            client.Contracts.Add(contract);
            billingCompany.Contracts.Add(contract);

            return contract;
        }

        public static ITerminalContract MakeNewTerminalContract(ITerminal terminal, IBillingCompany billingCompany, IClient client, ISubscriberNumber subscriberNumber)
        {
            var contract = new TerminalContract(terminal, new Tuple<IBillingCompany, IClient>(billingCompany, client));
            client.Contracts.Add(contract);
            billingCompany.Contracts.Add(contract);

            return contract;
        }

        public static ISubscriberNumber CreateNewSubscriberNumber(IRate rate, int number)
        {
            return new SubscriberNumber(rate, number);
        }

        public static IRate CreateNewRate(string name, double subscriptionFee)
        {
            return new Rate(name, subscriptionFee);
        }
    }
}
