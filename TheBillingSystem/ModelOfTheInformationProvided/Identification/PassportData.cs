using ContoursOfTheInformationProvided.Identification;
using System;

namespace ModelOfTheInformationProvided.Identification
{
    public class PassportData : IPassportData
    {
        public IPassportIdentification PassportIdentification { get; }
        public string PassportNumber { get; }
        public string Authority { get; }
        public DateTime DateOfIssue { get; }
        public DateTime DateOfExpiry { get; }


        public PassportData(IPassportIdentification passportIdentification, string passportNumber, string authority, DateTime dateOfIssue, DateTime dateOfExpiry)
        {
            PassportIdentification = passportIdentification;
            PassportNumber = passportNumber;
            Authority = authority;
            DateOfIssue = dateOfIssue;
            DateOfExpiry = dateOfExpiry;
        }


        public override string ToString()
        {
            return string.Join("\n\t", "Passport data:", string.Join(string.Empty, "Passport number: ", PassportNumber), PassportIdentification,
                string.Join(string.Empty, "Date of issue: ", DateOfIssue.ToShortDateString()), string.Join(string.Empty, "Authority: ", Authority),
                string.Join(string.Empty, "Date of expiry: ", DateOfExpiry.ToShortDateString()));
        }
    }
}
