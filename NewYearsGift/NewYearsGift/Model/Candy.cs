namespace NewYearsGift.Model
{
    public class Candy : Sweetness, ISweetnessWithFilling, ISweetnessWithType
    {
        public System.Enum Type { get; private set; }
        public System.Enum Filling { get; private set; }

        private const int fixedCaloryCandy = 25;

        public Candy(Enum.TypeCandy type, Enum.Filling filling, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Type = type;
            Filling = filling;
        }

        public override int CountCalories()
        {
            return fixedCaloryCandy + base.CountCalories();
        }

        public override string ToString()
        {
            return "Candy '" + Name + "':\n\tFilling: " + Filling.ToString() + ", type: " + Type + "\n\tContains " + 
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }

        public void AlterFilling(System.Enum newFilling)
        {
            Filling = (Enum.Filling)newFilling;
        }        
    }
}
