using Microsoft.AspNet.Identity.EntityFramework;

namespace ManagerCloud.MVC.Models
{
    public class ApplicationUser : IdentityUser
    {
        public Client Client { get; set; }

        public ApplicationUser()
        {

        }
    }
}