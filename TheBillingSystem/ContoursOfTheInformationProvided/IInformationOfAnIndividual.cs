using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ContoursOfTheInformationProvided.Identification;

namespace ContoursOfTheInformationProvided
{
    public interface IInformationOfAnIndividual
    {
        IPassportData PassportData { get; }
        IAdress Adress { get; set; }
        IContactInforamtion ContactInforamtion { get; set; }
    }
}
