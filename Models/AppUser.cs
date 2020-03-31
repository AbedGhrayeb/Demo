using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Demo.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Pets = new HashSet<Pet>();
        }
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
