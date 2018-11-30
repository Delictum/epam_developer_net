using ContourBillingSystem;
using ContoursOfTheInformationProvided;
using ContoursOfTheInformationProvided.Adress;
using ContoursOfTheInformationProvided.Contact;
using ModelBillingSystem;
using ModelOfTheInformationProvided;
using ServiceBillingSystem.CustomExceptions;
using System.Collections.Generic;

namespace ServiceBillingSystem.ManagementDecisions
{
    public static class CompanyManagement
    {
        public static IBillingCompany CreateNewCompany(string nameCompany)
        {
            return new BillingCompany(new LegalEntityInformation(null, null, nameCompany, null), new List<IStation>());
        }

        public static IBillingCompany CreateNewCompany(ILegalEntityInformation legalEntityInformation, IList<IStation> stations)
        {
            return new BillingCompany(legalEntityInformation, stations);
        }


        public static void ChangeAdress(IBillingCompany billingCompany, IAdress adress)
        {
            billingCompany.LegalEntityInformation.Adress = adress;
        }

        public static void ChangeContactInforamtion(IBillingCompany billingCompany, IContactInforamtion contactInforamtion)
        {
            billingCompany.LegalEntityInformation.ContactInforamtion = contactInforamtion;
        }

        public static void ChangeAboutLegalEntity(IBillingCompany billingCompany, string aboutLegalEntity)
        {
            billingCompany.LegalEntityInformation.AboutLegalEntity = aboutLegalEntity;
        }

        public static void ChangeNameLegalEntity(IBillingCompany billingCompany, string nameLegalEntity)
        {
            billingCompany.LegalEntityInformation.NameLegalEntity = nameLegalEntity;
        }


        public static void AddContactNumber(IBillingCompany billingCompany, int phoneNumber)
        {
            billingCompany.LegalEntityInformation.ContactInforamtion.PhoneNumbers.Add(phoneNumber);
        }

        public static void ChangeContactNumber(IBillingCompany billingCompany, int oldPhoneNumber, int newPhoneNumber)
        {
            DeleteContactNumber(billingCompany, oldPhoneNumber);
            AddContactNumber(billingCompany, newPhoneNumber);
        }

        public static void DeleteContactNumber(IBillingCompany billingCompany, int phoneNumber)
        {
            if (!billingCompany.LegalEntityInformation.ContactInforamtion.PhoneNumbers.Contains(phoneNumber))
            {
                throw new ContainArgumentException(phoneNumber.ToString());
            }
            billingCompany.LegalEntityInformation.ContactInforamtion.PhoneNumbers.Remove(phoneNumber);
        }


        public static void AddContactEmail(IBillingCompany billingCompany, string email)
        {
            billingCompany.LegalEntityInformation.ContactInforamtion.Emails.Add(email);
        }

        public static void ChangeContactEmail(IBillingCompany billingCompany, string oldEmail, string newEmail)
        {
            DeleteContactEmail(billingCompany, oldEmail);
            AddContactEmail(billingCompany, newEmail);
        }

        public static void DeleteContactEmail(IBillingCompany billingCompany, string email)
        {
            if (!billingCompany.LegalEntityInformation.ContactInforamtion.Emails.Contains(email))
            {
                throw new ContainArgumentException(email);
            }
            billingCompany.LegalEntityInformation.ContactInforamtion.Emails.Remove(email);
        }


        public static void AddStation(IBillingCompany billingCompany, IStation station)
        {
            billingCompany.Stations.Add(station);
        }

        public static void ChangeStation(IBillingCompany billingCompany, IStation oldStation, IStation newStation)
        {
            DeleteStation(billingCompany, oldStation);
            AddStation(billingCompany, newStation);
        }

        public static void DeleteStation(IBillingCompany billingCompany, IStation station)
        {
            if (!billingCompany.Stations.Contains(station))
                return;
            billingCompany.Stations.Remove(station);
        }
    }
}
