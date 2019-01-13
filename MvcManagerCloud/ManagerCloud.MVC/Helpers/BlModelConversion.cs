using System.Collections.Generic;
using ManagerCloud.BL;
using ManagerCloud.MVC.Models.Entities;

namespace ManagerCloud.MVC.Helpers
{
    public static class BlModelConversion
    {
        private static readonly Unity _unity = new Unity();

        public static List<Sale> GetListSales()
        {
            var listEntitySales = _unity.GetAllSales();
            var listSales = new List<Sale>();

            foreach (var sale in listEntitySales)
            {
                listSales.Add(new Sale
                {
                    Id = sale.Item1,
                    Client = new Client
                    {
                        Id = sale.Item2.Item1,
                        FirstName = sale.Item2.Item2,
                        LastName = sale.Item2.Item3
                    },
                    DataSource = new DataSource
                    {
                        Id = sale.Item3.Item1,
                        FileName = sale.Item3.Item2
                    },
                    Item = new Item
                    {
                        Id = sale.Item4.Item1,
                        Name = sale.Item4.Item2
                    },
                    Date = sale.Item5,
                    SaleSum = sale.Item6
                });
            }

            return listSales;
        }

        public static List<Item> GetListItems()
        {
            var listEntityItems = _unity.GetAllItems();
            var listItems = new List<Item>();
            foreach (var item in listEntityItems)
            {
                listItems.Add(new Item
                {
                    Id = item.Item1,
                    Name = item.Item2
                });
            }

            return listItems;
        }

        public static List<Client> GetListClients()
        {
            var clients = _unity.GetAllClients();

            var customers = new List<Client>();
            foreach (var client in clients)
            {
                customers.Add(new Client
                {
                    Id = client.Item1,
                    FirstName = client.Item2,
                    LastName = client.Item3
                });
            }

            return customers;
        }
    }
}