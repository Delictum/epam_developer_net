using System;
using System.Collections.Generic;


namespace NewYearsGift
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChildGift childGift = new ChildGift("Теремок", 1);

            var candy1 = new Model.Candy(Enum.TypeCandy.waffle, Enum.Filling.chocolate, "1", 1, 1);
            var candy2 = new Model.Candy(Enum.TypeCandy.chocolate, Enum.Filling.nougat, "Sweety", 14.4, 14);
            var chocolate1 = new Model.Chocolate(Enum.TypeChocolate.commonBitter, Enum.Filling.nuts, "Orvel", 32.6, 42);
            var cookie1 = new Model.Cookie(Enum.TypeCookie.butter, "Oatmeal", "Sindy", 22.1, 7);
            candy1.DecreaseSugar(2);

            //childGift.ShowComposition();
            //childGift.AddItem(can1);
            //childGift.ShowComposition();

            List<Model.Sweetness> listCandy = new List<Model.Sweetness> { candy1, candy2, candy2, chocolate1, cookie1 };
            ChildGift childGift2 = new ChildGift("Big castle", 150, listCandy);

            childGift2.AddItem(candy1);
            childGift2.ShowComposition();
            Console.WriteLine("Current weight: {0}.", childGift2.GetCurrentWeight());
            childGift2.SortItems();
            childGift2.SearchItemSugarRange(8, 16);
            Console.WriteLine(candy1.ToString() + "\n");
            Console.WriteLine(childGift2.ToString());
        }
    }
}
