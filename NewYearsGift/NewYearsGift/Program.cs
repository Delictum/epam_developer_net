using System;
using System.Collections.Generic;


namespace NewYearsGift
{
    class Program
    {
        static void Main(string[] args)
        {
            //ChildGift childGift = new ChildGift("Теремок", 1);

            Model.Candy can1 = new Model.Candy(Enum.TypeCandy.waffle, Enum.Filling.chocolate, "1", 1, 1);
            Model.Candy can2 = new Model.Candy(Enum.TypeCandy.chocolate, Enum.Filling.nougat, "Sweety", 14.4, 14);
            Model.Chocolate choc1 = new Model.Chocolate(Enum.TypeChocolate.commonBitter, Enum.Filling.nuts, "Orvel", 32.6, 42);
            Model.Cookie cook1 = new Model.Cookie(Enum.TypeCookie.butter, "Oatmeal", "Sindy", 22.1, 7);
            can1.DecreaseSugar(2);

            //childGift.ShowComposition();
            //childGift.AddItem(can1);
            //childGift.ShowComposition();

            List<Model.Sweetness> listCandy = new List<Model.Sweetness> { can1, can2, can2, choc1, cook1 };
            ChildGift childGift2 = new ChildGift("Big castle", 150, listCandy);

            childGift2.AddItem(can1);
            childGift2.ShowComposition();
            Console.WriteLine("Current weight: {0}.", childGift2.GetCurrentWeight());
            childGift2.SortItems();
            childGift2.SearchItemSugarRange(8, 16);
            Console.WriteLine(can1.ToString() + "\n");
            Console.WriteLine(childGift2.ToString());
        }
    }
}
