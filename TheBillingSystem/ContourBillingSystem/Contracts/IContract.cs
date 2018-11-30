using System;

namespace ContourBillingSystem.Contracts
{
    public interface IContract
    {
        string Number { get; }
        Tuple<IBillingCompany, IClient> Parties { get; }
        DateTime DateOfSigning { get; }
    }
}