namespace NewYearsGift.Model
{
    public abstract class Sweetness
    {
        private const int CaloryInSugarGramm = 4;

        public double Weight { get; private set; } 
        public int SugarInGramms { get; private set; }
        public string Name { get; private set; }
        public System.Enum Type { get; protected set; }

        public Sweetness(string name, double weight, int sugarInGramms)
        {
            Name = name;
            Weight = weight;
            SugarInGramms = sugarInGramms;
        }

        public virtual int CountCalories()
        {
            return CaloryInSugarGramm * SugarInGramms;
        }

        public void IncreaseWeight(double addedWeight)
        {
            Weight += addedWeight;
        }
        public void DecreaseWeight(double deletedWeight)
        {
            if (CanBeDecrease(Weight, deletedWeight))
                Weight -= deletedWeight;                   
        }

        public void IncreaseSugar(int addedSugarInGramms)
        {
            SugarInGramms += addedSugarInGramms;
        }

        public void DecreaseSugar(int deletedSugarInGramms)
        {
            if (CanBeDecrease(SugarInGramms, deletedSugarInGramms))
                SugarInGramms -= deletedSugarInGramms;
        }

        private bool CanBeDecrease(double currentValue, double decreaseValue)
        {
            try
            {
                if (currentValue - decreaseValue > 0)
                    return true;
                else
                    throw new System.ArithmeticException("Can't be decrease to a negative value.");
            }
            catch (System.ArithmeticException e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
