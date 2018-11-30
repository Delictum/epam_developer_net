using ContoursOfTheInformationProvided;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ContoursOfTheInformationProvided.Identification;
using ModelOfTheInformationProvided;
using ModelOfTheInformationProvided.Identification;

namespace ServiceBillingSystem.ProvisionOfInformation
{
    public static class InformationProvided
    {
        public static ILegalEntityInformation CreateNewLegalEntityInformation(string name, string about, IAdress adress, IContactInforamtion contactInforamtion)
        {
            return new LegalEntityInformation(adress, contactInforamtion, name, about);
        }

        public static IInformationOfAnIndividual CreateNewInformationOfAnIndividual(IAdress adress, IContactInforamtion contactInforamtion, IPassportData passportData)
        {
            return new InformationOfAnIndividual(adress, contactInforamtion, passportData);
        }
    }
}
