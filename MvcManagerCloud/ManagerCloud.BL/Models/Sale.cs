using System;

namespace ManagerCloud.BL.Models
{
    public class Sale
    {
        public int SaleId { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Item Item { get; set; }
        public double SaleSum { get; set; }
        public DataSource DataSource { get; set; }
    }
}
