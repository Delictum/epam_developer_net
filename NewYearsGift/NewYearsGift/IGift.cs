using System.Collections.Generic;

namespace NewYearsGift
{
    public interface IGift
    {
        string Name { get; } 

        double Weight { get; } 

        void AddItem(Model.Sweetness sweet);

        void AddItems(IEnumerable<Model.Sweetness> sweets);

        void RemoveItem(Model.Sweetness sweet);

        void ShowComposition();

        void SortItems();

        void SearchItemSugarRange(int minSugar, int maxSugar);
    }
}
