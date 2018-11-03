using NewYearsGift.Enum;

namespace NewYearsGift.Model
{
    public class Candy : Sweetness, ISweetnessWithFilling
    {
        private const int FixedCandyCalories = 25;

        public System.Enum Filling { get; set; }        

        public Candy(TypeCandy type, Filling filling, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Type = type;
            Filling = filling;
        }

        public override int CountCalories()
        {
            return FixedCandyCalories + base.CountCalories();
        }

        public override string ToString()
        {
            return "Candy '" + Name + "':\n\tFilling: " + Filling.ToString() + ", type: " + Type + "\n\tContains " + 
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }        
    }
}
