using System;
using System.Text;
using ContourBillingSystem;
using ContourBillingSystem.Contracts;

namespace ModelBillingSystem.Contracts
{
    public class Contract : IContract
    {
        public string Number { get; }
        public Tuple<IBillingCompany, IClient> Parties { get; }
        public DateTime DateOfSigning { get; }


        public Contract(Tuple<IBillingCompany, IClient> parties)
        {
            Number = Guid.NewGuid().ToString();
            Parties = parties;
            DateOfSigning = DateTime.Today;
        }

        public Contract(Tuple<IBillingCompany, IClient> parties, DateTime dateOfSigning)
        {
            Number = Guid.NewGuid().ToString();
            Parties = parties;
            DateOfSigning = dateOfSigning;
        }


        public override string ToString()
        {
            StringBuilder tempBuilder = new StringBuilder();
            tempBuilder.Append("Number: ").Append(Number).Append(", parties: ")
                .Append(Parties.Item1.LegalEntityInformation.NameLegalEntity).Append(" & ")
                .Append(Parties.Item2.CustomerInformation.PassportData.PassportIdentification.FullName)
                .Append(", date of signing: ").Append(DateOfSigning.ToShortDateString());
            return tempBuilder.ToString();
        }
    }
}
