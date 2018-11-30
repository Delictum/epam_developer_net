using System;

namespace ContoursOfTheInformationProvided.Identification
{
    public interface IPassportData
    {
        IPassportIdentification PassportIdentification { get; }
        string PassportNumber { get; }
        string Authority { get; }
        DateTime DateOfIssue { get; }
        DateTime DateOfExpiry { get; }
    }
}
