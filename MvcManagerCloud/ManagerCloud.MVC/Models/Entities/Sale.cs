using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ManagerCloud.MVC.Models.Entities
{
    public class Sale
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public Client Client { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [Display(Name = "Item")]
        public Item Item { get; set; }

        [Display(Name = "Data source")]
        public DataSource DataSource { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Required to fill")]
        [DataType(DataType.Currency)]
        [UIHint("Decimal")]
        [Display(Name = "Cost")]
        [Range(typeof(decimal), "0,01", "999999,99")]
        public double SaleSum { get; set; }
    }
}