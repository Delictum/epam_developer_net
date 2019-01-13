using System;
using System.Linq;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            var rnd = new Random();
            var item = Helpers.BlModelConversion.GetListSales().Select(x => new object[] { x.Item.Name, rnd.Next(10) }).ToArray();
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSalesData()
        {
            return Json(Helpers.BlModelConversion.GetListSales(), JsonRequestBehavior.AllowGet);
        }
    }
}