using System;
using ContoursOfTheInformationProvided.Adress;
using ModelOfTheInformationProvided.Adress;

namespace ServiceOfTheInformationProvided
{
    public static class CreatorRepresentativeInformation
    {
        public static IAdress CreateNewAdress(string countryName, string cityName, string streetName, Tuple<int, char, char> houseNumber, int appartmentNumber)
        {
            return new Adress(new Country(countryName), new City(cityName), streetName, houseNumber, appartmentNumber);
        }
    }
}
