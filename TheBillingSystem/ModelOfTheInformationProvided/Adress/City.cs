using ContoursOfTheInformationProvided.Adress;

namespace ModelOfTheInformationProvided.Adress
{
    public class City : ICity
    {
        public string Name { get; }


        public City(string name)
        {
            Name = name;
        }
    }
}
