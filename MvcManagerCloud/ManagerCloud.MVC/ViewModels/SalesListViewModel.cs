using System.Collections.Generic;
using System.Web.Mvc;
using ManagerCloud.MVC.Models;

namespace ManagerCloud.MVC.ViewModels
{
    public class SalesListViewModel
    {
        public IEnumerable<Sale> Sales { get; set; }
        public SelectList SaleSum { get; set; }
        public SelectList Items { get; set; }
    }
}