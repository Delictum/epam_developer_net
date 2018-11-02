using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewYearsGift.Model;

namespace NewYearsGift
{
    class ChildGift : IGift
    {
        const double MinGiftWeight = 40;
        const double DefaultGiftWeight = 200;

        private readonly double weight;
        public double Weight { get => weight; }
        public string Name { get; private set; }  
        private Dictionary<Sweetness, int> sweets = new Dictionary<Sweetness, int>();

        public ChildGift(string name)
        {
            Name = name;
            weight = DefaultGiftWeight;
        }

        public ChildGift(string name, double weight)
        {
            Name = name;
            this.weight = WeightCorrect(weight);
        }

        public ChildGift(string name, double weight, List<Sweetness> sweets)
        {
            Name = name;
            this.weight = WeightCorrect(weight);
            AddItems(sweets);            
        }

        public double GetCurrentWeight()
        {
            double weightCounter = 0;
            foreach (var sweet in sweets)
            {
                weightCounter += sweet.Key.Weight * sweet.Value;
            }
            return weightCounter;
        }

        public void AddItem(Sweetness sweet)
        {
            try
            {
                if (CanSweetnessBeAdded(sweet.Weight))
                {
                    if (sweets.ContainsKey(sweet))
                        sweets[sweet]++;
                    else
                        sweets.Add(sweet, 1);
                }
                else
                    throw new Exception("Can't add sweetness. Reached the maximum weight of the gift.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public void AddItems(IEnumerable<Sweetness> sweets)
        {
            Sweetness[] currentSweet = sweets.ToArray();

            for (int i = 0; i < currentSweet.Length; i++)
            {
                AddItem(currentSweet[i]);
            }
        }

        public void RemoveItem(Sweetness sweet)
        {
            try
            {
                if (sweets.ContainsKey(sweet))
                {
                    if (sweets[sweet] > 1)
                        sweets[sweet]--;
                    else
                        sweets.Remove(sweet);
                }
                else
                    throw new KeyNotFoundException("The specified sweetness is not in the gift.");
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }        

        public void ShowComposition()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Child gift '{0}':\n", Name);
            foreach (var sweet in sweets)
            {
                Console.WriteLine(sweet.Value + "th. - " + sweet.Key.ToString() + "\n");
            }
            Console.WriteLine("--------------------------------------------");
        }

        public void SortItems() =>
            sweets = sweets.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

        public void SearchItemSugarRange(int minSugar, int maxSugar)
        {
            foreach (var sweet in sweets)
            {
                if (minSugar <= sweet.Key.SugarInGramms && sweet.Key.SugarInGramms <= maxSugar)
                    Console.WriteLine("Sweetness '{0}' contains {1}g sugar.", sweet.Key.Name, sweet.Key.SugarInGramms);
            }            
        }        

        private double WeightCorrect(double setWeight)
        {
            if (setWeight >= MinGiftWeight)
                return setWeight;
            else                         
                Console.WriteLine("An attempt was made to establish an incorrect weight. The maximum weight is set by default and is 200g.");
            return DefaultGiftWeight;            
        }

        private bool CanSweetnessBeAdded(double sweetWeight) =>
            (GetCurrentWeight() + sweetWeight < Weight) ? true : false;

        public override string ToString()
        {
            StringBuilder childGiftString = new StringBuilder();
            childGiftString.Append("Child gift \"");
            childGiftString.Append(Name);
            childGiftString.Append("\" (max weight - ");
            childGiftString.Append(Weight);
            childGiftString.Append(", current weight - ");
            childGiftString.Append(GetCurrentWeight());
            childGiftString.Append(":\n");
            foreach (KeyValuePair<Sweetness, int> sweet in sweets)
            {
                childGiftString.Append("\t");
                childGiftString.Append(sweet.Key.Name);
                childGiftString.Append(": ");
                childGiftString.Append(sweet.Value);
                childGiftString.Append("th.;\n");
            }
            return childGiftString.ToString();
        }
    }
}
