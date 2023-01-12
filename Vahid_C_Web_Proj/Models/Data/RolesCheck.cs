using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class RolesCheck
    {

        public int Id { get; set; }
        public string IdentityUserId { get; set; }
        [DisplayName("Requested Role")]
        public string RoleName { get; set; }

        public IdentityUser User { get; set; }

    }
}
