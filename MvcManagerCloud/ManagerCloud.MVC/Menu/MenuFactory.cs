using System.Collections.Generic;

namespace ManagerCloud.MVC.Menu
{
    public static class MenuFactory
    {
        public static List<MenuItem> CreateMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem{Id=1, Header = "Main", Url = "/Home/Index", Order = 1},
                new MenuItem{Id=2, Header = "Clients", Url = "/Clients/Index", Order = 2},
                new MenuItem{Id=3, Header = "Sales", Url = "/Sales/Index", Order = 3},
                new MenuItem{Id=4, Header = "Items", Url = "/Items/Index", Order = 4},
                new MenuItem{Id=5, Header = "About", Url = "/Home/About", Order = 5},
                new MenuItem{Id=6, Header = "Contact", Url = "/Home/Contact", Order = 6},
                new MenuItem{Id=7, Header = "Buy", Url = "/Home/Buy", Order = 1, ParentId = 1},
                new MenuItem{Id=8, Header = "Chart", Url = "/Chart/Index", Order = 2, ParentId = 1}
            };
        }
    }
}