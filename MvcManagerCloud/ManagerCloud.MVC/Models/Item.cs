using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Models
{
    public class Item
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Name")]
        [StringLength(255, ErrorMessage = "Too many chars")]
        public string Name { get; set; }
    }
}