using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Models.Entities
{
    public class DataSource
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "File name")]
        [StringLength(100, ErrorMessage = "Too many chars")]
        public string FileName { get; set; }
    }
}