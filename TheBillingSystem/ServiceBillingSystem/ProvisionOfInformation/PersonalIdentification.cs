using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ContoursOfTheInformationProvided.Identification;
using ModelOfTheInformationProvided.Identification;

namespace ServiceBillingSystem.ProvisionOfInformation
{
    public static class PersonalIdentification
    {
        public static IFullName CreateNewFullName(string surname, string givenNames, string patronymic)
        {
            return new FullName(surname, givenNames, patronymic);
        }

        public static IPassportIdentification CreateNewPassportIdentification(IFullName fullName, string nationality, 
            DateTime dateOfBirth, bool isMale, string identificationNumber, string placeOfBirth)
        {
            return new PassportIdentification(fullName, nationality, dateOfBirth, isMale, identificationNumber, placeOfBirth);
        }

        public static IPassportData CreateNewPassportData(IPassportIdentification passportIdentification,
            string passportNumber, string authority, DateTime dateOfIssue, DateTime dateOfExpiry)
        {
            return new PassportData(passportIdentification, passportNumber, authority, dateOfIssue, dateOfExpiry);
        }
    }
}
