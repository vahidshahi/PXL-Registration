using System.ComponentModel.DataAnnotations;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class Vak
    {
        public int VakId { get; set; }
        [Display(Name = "Vaknaam")]
        public string VakNaam { get; set; }
        public int Studiepunten { get; set; }
        public int HandboekID { get; set; }

        public Handboek Handboek { get; set; }

    }
}
