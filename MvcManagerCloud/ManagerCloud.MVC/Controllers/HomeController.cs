using ManagerCloud.BL;
using ManagerCloud.MVC.Menu;
using ManagerCloud.MVC.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ManagerCloud.MVC.Filters;

namespace ManagerCloud.MVC.Controllers
{
    public class HomeController : Controller
    {
        private const int PageSize = 5;
        private readonly Unity _unity = new Unity();

        public ActionResult Index(int? page)
        {
            var listSales = Helpers.BlModelConversion.GetListSales();

            var pageNumber = (page ?? 1);
            return View(listSales.ToPagedList(pageNumber, PageSize));
        }

        public ActionResult Menu()
        {
            var menuItems = MenuFactory.CreateMenuItems();

            return PartialView(menuItems);
        }

        [Authorize(Roles = "admin")]
        [UserAuthentication]
        [HttpGet]
        public ActionResult Buy()
        {
            var listEntityItems = _unity.GetAllItems();
            var items = new List<string>();
            var itemsId = new List<int>();

            foreach (var item in listEntityItems)
            {
                items.Add(item.Item2);
                itemsId.Add(item.Item1);
            }

            ViewBag.Items = items;
            ViewBag.ItemsId = itemsId;

            return View();
        }

        [LocalAuthorize(Roles = "admin")]
        [UserAuthentication]
        [HttpPost]
        public ActionResult Buy(Client client, Sale sale, Item item)
        {
            var listEntityItems = _unity.GetAllItems();
            var items = new List<string>();
            var itemsId = new List<int>();

            foreach (var listItem in listEntityItems)
            {
                items.Add(listItem.Item2);
                itemsId.Add(listItem.Item1);
            }

            ViewBag.Items = items;
            ViewBag.ItemsId = itemsId;

            if (client.LastName.Length < 0.01 || client.LastName.Length > 999999.99)
            {
                ModelState.AddModelError("SaleSum", "Incorrect cost value");
                ViewBag.Message = "Failed";
                return View();
            }

            _unity.AddSale(new Tuple<int, string, DateTime, double>(260, item.Name, DateTime.Now, sale.SaleSum));
            ViewBag.Message = "Successful";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "This site was developed for educational purposes on task 5 for EPAM Systems by a student Igor Olishkevich course of 2018/2019.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacts.";

            return View();
        }
    }
}