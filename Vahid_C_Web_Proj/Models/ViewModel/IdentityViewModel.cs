using Microsoft.AspNetCore.Identity;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Models.ViewModel
{
    public class IdentityViewModel
    {
        public IEnumerable<IdentityUser>? Users { get; set; }
        public IEnumerable<IdentityRole>? Roles { get; set; }
       
        
    }
}
