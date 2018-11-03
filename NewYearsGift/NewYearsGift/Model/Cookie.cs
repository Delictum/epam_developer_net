using NewYearsGift.Enum;

namespace NewYearsGift.Model
{
    public class Cookie : Sweetness
    {
        private const int FixedCookieCalories = 15;

        public string Shape { get; set; }        

        public Cookie(TypeCookie type, string shape, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Shape = shape;
            Type = type;
        }

        public override int CountCalories()
        {            
            return FixedCookieCalories + base.CountCalories();
        }

        public override string ToString()
        {
            return "Cookie '" + Name + "':\n\tShape: " + Shape + ", type: " + Type + "\n\tContains " +
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }
    }
}
