using ContoursOfTheInformationProvided.Identification;

namespace ModelOfTheInformationProvided.Identification
{
    public class FullName : IFullName
    {
        public string Surname { get; }
        public string GivenNames { get; }
        public string Patronymic { get; }


        public FullName(string surname, string givenNames, string patronymic)
        {
            Surname = surname;
            GivenNames = givenNames;
            Patronymic = patronymic;
        }


        public override string ToString()
        {
            return string.Join(" ", Surname, GivenNames, Patronymic);
        }
    }
}
