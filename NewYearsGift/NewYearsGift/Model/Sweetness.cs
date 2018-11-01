namespace NewYearsGift.Model
{
    public abstract class Sweetness
    {
        public double Weight { get; private set; } 
        public int SugarInGramms { get; private set; }
        public string Name { get; private set; }

        private const int caloryInSugarGramm = 4;        

        public Sweetness(string name, double weight, int sugarInGramms)
        {
            Name = name;
            Weight = weight;
            SugarInGramms = sugarInGramms;
        }

        public virtual int CountCalories()
        {
            return caloryInSugarGramm * SugarInGramms;
        }

        public void IncreaseWeight(double addedWeight)
        {
            Weight += addedWeight;
        }
        public void DecreaseWeight(double deletedWeight)
        {
            Weight -= deletedWeight;
        }

        public void IncreaseSugar(int addedSugarInGramms)
        {
            SugarInGramms += addedSugarInGramms;
        }

        public void DecreaseSugar(int deletedSugarInGramms)
        {
            SugarInGramms -= deletedSugarInGramms;
        }
    }
}
