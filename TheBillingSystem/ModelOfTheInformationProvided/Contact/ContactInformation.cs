using ContoursOfTheInformationProvided.Contact;
using System.Collections.Generic;
using System.Text;

namespace ModelOfTheInformationProvided.Contact
{
    public class ContactInformation : IContactInforamtion
    {
        public IList<int> PhoneNumbers { get; set; }
        public IList<string> Emails { get; set; }


        public ContactInformation(IList<int> phoneNumbers, IList<string> emails)
        {
            PhoneNumbers = phoneNumbers;
            Emails = emails;
        }


        public override string ToString()
        {
            StringBuilder tempBuilder = new StringBuilder();
            tempBuilder.Append("Contact information:\n");
            tempBuilder.Append("\tPhone numbers: ");
            foreach (var phoneNumber in PhoneNumbers)
            {
                tempBuilder.Append(phoneNumber).Append("; ");
            }
            tempBuilder.Append("\n\tEmails: ");
            foreach (var email in Emails)
            {
                tempBuilder.Append(email).Append("; ");
            }
            return tempBuilder.ToString();
        }
    }
}
