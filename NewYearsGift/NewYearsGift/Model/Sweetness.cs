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
            try
            {
                if (CanBeDecreased(Weight, deletedWeight))
                    Weight -= deletedWeight;
                else
                    throw new System.ArithmeticException("Can't be decrease to a negative value.");
            }
            catch (System.ArithmeticException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void IncreaseSugar(int addedSugarInGramms)
        {
            SugarInGramms += addedSugarInGramms;
        }

        public void DecreaseSugar(int deletedSugarInGramms)
        {
            try
            {
                if (CanBeDecreased(SugarInGramms, deletedSugarInGramms))
                    SugarInGramms -= deletedSugarInGramms;
                else
                    throw new System.ArithmeticException("Can't be decrease to a negative value.");
            }
            catch (System.ArithmeticException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        private bool CanBeDecreased(double currentValue, double decreaseValue) =>
            (currentValue - decreaseValue > 0) ? true : false;
    }
}
