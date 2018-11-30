using ContoursOfTheInformationProvided.Adress;
using System;

namespace ModelOfTheInformationProvided.Adress
{
    public class Adress : IAdress
    {
        public ICountry Country { get; }
        public ICity City { get; }
        public string Street { get; }
        public Tuple<int, char, char> HouseNumber { get; }
        public int AppartmentNumber { get; }


        public Adress(Country country, City city, string street, Tuple<int, char, char> houseNumber, int appartmentNumber)
        {
            Country = country;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            AppartmentNumber = appartmentNumber;
        }


        public override string ToString()
        {
            return string.Join(string.Empty, "Adress: \n\t", string.Join(", ", Country.Name, City.Name, Street, 
                string.Join(string.Empty, HouseNumber.Item1, HouseNumber.Item2, HouseNumber.Item3),
                AppartmentNumber));
        }
    }
}
