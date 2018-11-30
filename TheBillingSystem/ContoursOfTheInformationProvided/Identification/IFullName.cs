namespace ContoursOfTheInformationProvided.Identification
{
    public interface IFullName
    {
        string Surname { get; }
        string GivenNames { get; }
        string Patronymic { get; }
    }
}
