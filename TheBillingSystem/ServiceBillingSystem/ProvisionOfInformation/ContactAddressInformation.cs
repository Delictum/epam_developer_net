using System;
using System.Collections.Generic;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ModelOfTheInformationProvided.Adress;
using ModelOfTheInformationProvided.Contact;

namespace ServiceBillingSystem.ProvisionOfInformation
{
    public static class ContactAddressInformation
    {
        public static IAdress CreateNewAdress(string countryName, string cityName, string streetName, Tuple<int, char, char> houseNumber, int appartmentNumber)
        {
            return new Adress(new Country(countryName), new City(cityName), streetName, houseNumber, appartmentNumber);
        }

        public static IContactInforamtion CreateNewContacts(IList<int> companyListNumbers, IList<string> companyListEmails)
        {
            return new ContactInformation(companyListNumbers, companyListEmails);
        }
    }
}
