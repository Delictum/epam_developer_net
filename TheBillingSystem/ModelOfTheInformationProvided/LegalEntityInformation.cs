using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ContoursOfTheInformationProvided;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;

namespace ModelOfTheInformationProvided
{
    public class LegalEntityInformation : ILegalEntityInformation
    {
        public string NameLegalEntity { get; set; }
        public string AboutLegalEntity { get; set; }
        public IAdress Adress { get; set; }
        public IContactInforamtion ContactInforamtion { get; set; }


        public LegalEntityInformation(IAdress adress, IContactInforamtion contactInforamtion,
            string name, string aboutLegalEntity)
        {
            Adress = adress;
            ContactInforamtion = contactInforamtion;
            NameLegalEntity = name;
            AboutLegalEntity = aboutLegalEntity;
        }


        public override string ToString()
        {
            return string.Join("\n", string.Join(string.Empty, "Company: ", NameLegalEntity), 
                string.Join(string.Empty, "About company: ", AboutLegalEntity), Adress, ContactInforamtion);
        }
    }
}
