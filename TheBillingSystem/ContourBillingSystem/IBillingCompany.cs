using ContoursOfTheInformationProvided;
using System.Collections.Generic;
using ContourBillingSystem.Contracts;

namespace ContourBillingSystem
{
    public interface IBillingCompany
    {
        ILegalEntityInformation LegalEntityInformation { get; set; }
        IList<IStation> Stations { get; set; }
        IList<ITerminalContract> Contracts { get; set; }
    }
}
