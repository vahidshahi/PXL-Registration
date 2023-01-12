
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class VakLector
    {
      
        public int VakLectorId { get; set; }
        public int LectorId { get; set; }
        public int VakId { get; set; }



        public Lector Lector { get; set; }
        public Vak Vak { get; set; }
    }
}
