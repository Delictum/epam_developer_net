using ContoursOfTheInformationProvided;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ContoursOfTheInformationProvided.Identification;

namespace ModelOfTheInformationProvided
{
    public class InformationOfAnIndividual : IInformationOfAnIndividual
    {
        public IPassportData PassportData { get; }
        public IAdress Adress { get; set; }
        public IContactInforamtion ContactInforamtion { get; set; }


        public InformationOfAnIndividual(IAdress adress, IContactInforamtion contactInforamtion, IPassportData passportData)
        {
            Adress = adress;
            ContactInforamtion = contactInforamtion;
            PassportData = passportData;
        }


        public override string ToString()
        {
            return string.Join("\n", PassportData, Adress, ContactInforamtion);
        }
    }
}
