using System.ComponentModel.DataAnnotations;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class AcademieJaar
    {
        public int AcademieJaarId { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
    }
}
