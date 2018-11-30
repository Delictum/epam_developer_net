using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;

namespace ContoursOfTheInformationProvided
{
    public interface ILegalEntityInformation
    {
        string NameLegalEntity { get; set; }
        string AboutLegalEntity { get; set; }
        IAdress Adress { get; set; }
        IContactInforamtion ContactInforamtion { get; set; }
    }
}
