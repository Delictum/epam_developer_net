using ContoursOfTheInformationProvided.Adress;

namespace ModelOfTheInformationProvided.Adress
{
    public class Country : ICountry
    {
        public string Name { get; }


        public Country(string name)
        {
            Name = name;
        }
    }
}
