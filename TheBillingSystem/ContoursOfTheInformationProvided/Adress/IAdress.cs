using System;

namespace ContoursOfTheInformationProvided.Adress
{
    public interface IAdress
    {
        ICountry Country { get; }
        ICity City { get; }
        string Street { get; }
        Tuple<int, char, char> HouseNumber { get; }
        int AppartmentNumber { get; }
    }
}
