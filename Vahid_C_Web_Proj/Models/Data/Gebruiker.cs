using System.ComponentModel.DataAnnotations;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class Gebruiker
    {
     
        public int GebruikerId { get; set; }
        [Required]
        public string Naam { get; set; }
        [Required]
        public string Voornaam { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
