namespace NewYearsGift.Model
{
    public class Chocolate : Sweetness, ISweetnessWithFilling, ISweetnessWithType
    {
        public System.Enum Type { get; private set; }
        public System.Enum Filling { get; private set; }

        private const int fixedCaloryChoclate = 15;

        public Chocolate(Enum.TypeChocolate type, Enum.Filling filling, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Type = type;
            Filling = filling;
        }

        public override int CountCalories()
        {
            return fixedCaloryChoclate + base.CountCalories();
        }

        public override string ToString()
        {
            return "Chololate '" + Name + "':\n\tFilling: " + Filling.ToString() + ", type: " + Type + "\n\tContains " +
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }

        public void AlterFilling(System.Enum newFilling)
        {
            Filling = (Enum.Filling)newFilling;
        }        
    }
}
