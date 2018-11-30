using ContourBillingSystem;
using ContourBillingSystem.Contracts;
using ContoursOfTheInformationProvided;
using System.Collections.Generic;

namespace ModelBillingSystem
{
    public class BillingCompany : IBillingCompany
    {
        public ILegalEntityInformation LegalEntityInformation { get; set; }
        public IList<IStation> Stations { get; set; }
        public IList<ITerminalContract> Contracts { get; set; }


        public BillingCompany(ILegalEntityInformation legalEntityInformation, IList<IStation> stations)
        {
            LegalEntityInformation = legalEntityInformation;
            Stations = stations;
            Contracts = new List<ITerminalContract>();
        }

        public BillingCompany(ILegalEntityInformation legalEntityInformation, IList<IStation> stations, IList<ITerminalContract> contracts)
        {
            LegalEntityInformation = legalEntityInformation;
            Stations = stations;
            Contracts = contracts;
        }
    }
}
