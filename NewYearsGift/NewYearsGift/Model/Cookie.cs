namespace NewYearsGift.Model
{
    public class Cookie : Sweetness, ISweetnessWithType
    {
        public string Shape { get; private set; }
        public System.Enum Type { get; private set; }

        private const int fixedCaloryCookie = 15;

        public Cookie(Enum.TypeCookie type, string shape, string name, double weight, int gSugar) : base(name, weight, gSugar)
        {
            Shape = shape;
            Type = type;
        }

        public override int CountCalories()
        {            
            return fixedCaloryCookie + base.CountCalories();
        }

        public override string ToString()
        {
            return "Cookie '" + Name + "':\n\tShape: " + Shape + ", type: " + Type + "\n\tContains " +
                SugarInGramms + "g sugar (calories in one candy - " + CountCalories().ToString() + ").";
        }

        public void AlterShape(string newShape)
        {
            Shape = newShape;
        }
    }
}
