using NewYearsGift.Enum;

namespace NewYearsGift.Model
{
    public class Chocolate : Sweetness, ISweetnessWithFilling
    {
        private const int FixedCaloryChoclate = 15;

        public System.Enum Filling { get; set; }        

        public Chocolate(TypeChocolate type, Filling filling, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Type = type;
            Filling = filling;
        }

        public override int CountCalories()
        {
            return FixedCaloryChoclate + base.CountCalories();
        }

        public override string ToString()
        {
            return "Chololate '" + Name + "':\n\tFilling: " + Filling.ToString() + ", type: " + Type + "\n\tContains " +
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }      
    }
}
