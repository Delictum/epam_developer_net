using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerCloud.MVC.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {

        }

        public string Description { get; set; }
    }
}