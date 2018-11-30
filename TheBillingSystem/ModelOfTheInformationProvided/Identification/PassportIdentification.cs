using ContoursOfTheInformationProvided.Identification;
using System;

namespace ModelOfTheInformationProvided.Identification
{
    public class PassportIdentification : IPassportIdentification
    {
        public IFullName FullName { get; }
        public string Nationality { get; }
        public DateTime DateOfBirth { get; }
        public bool IsMale { get; }
        public string IdentificationNumber { get; }
        public string PlaceOfBirth { get; }


        public PassportIdentification(IFullName fullName, string nationality, DateTime dateOfBirth, bool isMale, string identificationNumber, string placeOfBirth)
        {
            FullName = fullName;
            Nationality = nationality;
            DateOfBirth = dateOfBirth;
            IsMale = isMale;
            IdentificationNumber = identificationNumber;
            PlaceOfBirth = placeOfBirth;
        }


        public override string ToString()
        {
            return string.Join("\n\t", string.Join(string.Empty, "Full name:", FullName.ToString()), string.Join(string.Empty, "Nationality: ", Nationality), 
                string.Join(string.Empty, "Date of birth: ", DateOfBirth.ToShortDateString()),
                string.Join(string.Empty, "Identification number: ", IdentificationNumber), 
                string.Join(string.Empty, "Sex: ", IsMale ? "M" : "W"), string.Join(string.Empty, "Place of birth: ", PlaceOfBirth));
        }
    }
}
