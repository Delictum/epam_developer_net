using System;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Mvc;
using ManagerCloud.BL;
using ManagerCloud.MVC.Filters;
using ManagerCloud.MVC.Models.Entities;

namespace ManagerCloud.MVC.Controllers
{
    public class ItemsController : Controller
    {
        private readonly Unity _unity = new Unity();

        // GET: Items
        public ActionResult Index()
        {
            return View(Helpers.BlModelConversion.GetListItems());
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var items = Helpers.BlModelConversion.GetListItems()
                .Where(a => a.Name.Contains(term))
                .Select(a => new { value = a.Name })
                .Distinct();

            return Json(items, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var item = Helpers.BlModelConversion.GetListItems().FirstOrDefault(i => i.Id == id);
            if (item != null)
                return PartialView(item);
            return HttpNotFound();
        }

        public string GetData()
        {
            return JsonConvert.SerializeObject(Helpers.BlModelConversion.GetListItems());
        }

        [LocalAuthorize(Roles = "admin")]
        [HttpGet]
        [OutputCache(Duration = 300)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var item = _unity.GetItem((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var customer = new Item
            {
                Id = item.Item1,
                Name = item.Item2
            };

            return View(customer);
        }

        [LocalAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                _unity.UpdateItem(new Tuple<int, string>(item.Id, item.Name));
            }
            else
            {
                ViewBag.Message = "Failed";
            }

            ViewBag.Message = "Successful";
            return RedirectToAction("Index");
        }

        [LocalAuthorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [LocalAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _unity.AddItem(item.Name);
            }
            else
            {
                ViewBag.Message = "Failed";
            }

            ViewBag.Message = "Successful";
            return RedirectToAction("Index");
        }

        [LocalAuthorize(Roles = "admin")]
        [OutputCache(Duration = 300)]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var item = _unity.GetItem((int)id);
            if (item == null)
            {
                return HttpNotFound();
            }

            var customer = new Item
            {
                Id = item.Item1,
                Name = item.Item2
            };

            return View(customer);
        }

        [LocalAuthorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var client = _unity.GetItem(id);
            if (client == null)
            {
                return RedirectToAction("Index");
            }

            _unity.RemoveItem(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ItemSearch(string iName)
        {
            var client = Helpers.BlModelConversion.GetListItems().FirstOrDefault(x => x.Name == iName);

            return Details(client.Id);
        }
    }
}