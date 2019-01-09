using System.Collections.Generic;

namespace ManagerCloud.MVC.Menu
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Header { get; set; } 
        public string Url { get; set; } 
        public int? Order { get; set; }  
        public int? ParentId { get; set; }  
        public MenuItem Parent { get; set; }  

        public ICollection<MenuItem> Children { get; set; } 
        public MenuItem()
        {
            Children = new List<MenuItem>();
        }
    }
}