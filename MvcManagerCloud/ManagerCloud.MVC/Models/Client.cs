using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Models
{
    public class Client
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "First name")]
        [StringLength(30, ErrorMessage = "Too many chars")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Last name")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The length of the string must be from 2 to 40 characters")]
        public string LastName { get; set; }
    }
}