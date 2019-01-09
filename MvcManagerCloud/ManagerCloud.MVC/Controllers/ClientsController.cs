using ManagerCloud.BL;
using ManagerCloud.MVC.Models;
using ManagerCloud.MVC.ViewModels;
using System;
using System.ComponentModel;
using System.Web.Mvc;
using ManagerCloud.MVC.Filters;

namespace ManagerCloud.MVC.Controllers
{
    public class ClientsController : Controller
    {
        private const int MyFirstClientId = 260;
        private readonly Unity _unity = new Unity();

        // GET: Clients
        public ActionResult Index()
        {
            var customers = Helpers.BlModelConversion.GetListClients();

            var viewModel = new IndexClientViewModel
            {
                Clients = customers
            };

            return View(viewModel);
        }

        // GET: Clients
        [UserAuthentication]
        [LocalAuthorize(Roles = "admin")]
        [HandleError]
        [OutputCache(Duration = 300)]
        public ActionResult Details([DefaultValue(MyFirstClientId)] int id)
        {
            var client = _unity.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var customer = new Client
            {
                Id = client.Item1,
                FirstName = client.Item2,
                LastName = client.Item3
            };

            return View(customer);
        }

        [LocalAuthorize(Roles = "admin")]
        [HandleError]
        [HttpGet]
        [OutputCache(Duration = 300)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var client = _unity.GetClient((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var customer = new Client
            {
                Id = client.Item1,
                FirstName = client.Item2,
                LastName = client.Item3
            };

            return View(customer);
        }

        [LocalAuthorize(Roles = "admin")]
        [HandleError]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.FirstName.Length > 30)
                {
                    ModelState.AddModelError("FirstName", "Too many chars");
                }
                else if (client.LastName.Length < 2 || client.LastName.Length > 40)
                {
                    ModelState.AddModelError("LastName", "Incorrect string length");
                }
                _unity.UpdateClient(new Tuple<int, string, string>(client.Id, client.FirstName, client.LastName));
            }
            else
            {
                ViewBag.Message = "Failed";
                return View();
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
        public ActionResult Create([Bind(Include = "FirstName,LastName")] Client client)
        {
            if (ModelState.IsValid)
            {
                if (client.FirstName.Length > 30)
                {
                    ModelState.AddModelError("FirstName", "Too many chars");
                }
                else if (client.LastName.Length < 2 || client.LastName.Length > 40)
                {
                    ModelState.AddModelError("LastName", "Incorrect string length");
                }
                _unity.AddClient(new Tuple<string, string>(client.FirstName, client.LastName));
            }
            else
            {
                ViewBag.Message = "Failed";
                return View();
            }

            ViewBag.Message = "Successful";
            return RedirectToAction("Index");
        }

        [LocalAuthorize(Roles = "admin")]
        [HandleError]
        [OutputCache(Duration = 300)]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var client = _unity.GetClient((int)id);
            if (client == null)
            {
                return HttpNotFound();
            }

            var customer = new Client
            {
                Id = client.Item1,
                FirstName = client.Item2,
                LastName = client.Item3
            };

            return View(customer);
        }

        [LocalAuthorize(Roles = "admin")]
        [HandleError]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var client = _unity.GetClient(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            _unity.RemoveClient(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ClientSearch(string fName, string lName)
        {
            var listClients = Helpers.BlModelConversion.GetListClients();
            if (!string.IsNullOrEmpty(fName))
            {
                listClients = listClients.FindAll(client => client.FirstName.Contains(fName));
            }
            if (!string.IsNullOrEmpty(lName))
            {
                listClients = listClients.FindAll(client => client.LastName.Contains(lName));
            }

            if (listClients.Count <= 0)
            {
                return HttpNotFound();
            }

            return PartialView(listClients);
        }
    }
}