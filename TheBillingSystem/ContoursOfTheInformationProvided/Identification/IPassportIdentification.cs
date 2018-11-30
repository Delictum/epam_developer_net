using System;

namespace ContoursOfTheInformationProvided.Identification
{
    public interface IPassportIdentification
    {
        IFullName FullName { get; }
        string Nationality { get; }
        DateTime DateOfBirth { get; }
        bool IsMale { get; }
        string IdentificationNumber { get; }
        string PlaceOfBirth { get; }
    }
}
