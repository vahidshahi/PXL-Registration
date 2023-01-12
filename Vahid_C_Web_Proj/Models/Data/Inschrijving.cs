using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class Inschrijving
    {

        public int InschrijvingId { get; set; }
        public int StudentId { get; set; }
        public int VakLectorId { get; set; }
        public int AcademieJaarId { get; set; }
       
        
        public Student Student { get; set; }

        [DisplayName("Lector")]
        public VakLector VakLector { get; set; }
        public AcademieJaar AcademieJaar { get; set; }

       

    }
       
        
}
