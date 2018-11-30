using System.Collections.Generic;
using ContourBillingSystem.Contracts;
using ContoursOfTheInformationProvided;

namespace ContourBillingSystem
{
    public interface IClient
    {
        IInformationOfAnIndividual CustomerInformation { get; }
        IList<ITerminalContract> Contracts { get; set; }
    }
}