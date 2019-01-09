using System.Collections.Generic;
using System.Linq;
using ManagerCloud.MVC.Models;
using ManagerCloud.MVC.ViewModels;
using System.Web.Mvc;
using ManagerCloud.MVC.Filters;

namespace ManagerCloud.MVC.Controllers
{
    [UserAuthentication]
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index(double? saleSum, string items)
        {
            var listItems = Helpers.BlModelConversion.GetListItems();
            var listSales = Helpers.BlModelConversion.GetListSales();
            var listSaleSum = new List<double>();

            foreach (var sale in listSales)
            {
                listSaleSum.Add(sale.SaleSum);
            }

            var sales = listSales;
            if (saleSum != null && saleSum != -1)
            {
                sales = sales.FindAll(p => p.SaleSum == saleSum);
            }

            if (!string.IsNullOrEmpty(items) && !items.Equals("All"))
            {
                sales = sales.FindAll((p => p.Item.Name == items));
            }

            listSaleSum.Insert(0, -1);
            listItems.Insert(0, new Item
            {
                Name = "All"
            });

            var salesView = new SalesListViewModel
            {
                Sales = sales,
                SaleSum = new SelectList(listSaleSum.Distinct()),
                Items = new SelectList(listItems, "Name", "Name")
            };
            return View(salesView);
        }
    }
}