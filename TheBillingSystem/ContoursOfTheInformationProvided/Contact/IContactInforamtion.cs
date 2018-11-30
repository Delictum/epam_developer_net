using System.Collections.Generic;

namespace ContoursOfTheInformationProvided.Contact
{
    public interface IContactInforamtion
    {
        IList<int> PhoneNumbers { get; set; }
        IList<string> Emails { get; set; }
    }
}
