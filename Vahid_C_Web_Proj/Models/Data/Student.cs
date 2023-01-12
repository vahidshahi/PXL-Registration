using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vahid_C_Web_Proj.Models.Data
{
    public class Student 
   
    {
       
         public int StudentId { get; set; }
        public int GebruikerId { get; set; }
        public int Inschrijving { get; internal set; }
        public Gebruiker Gebruiker { get; set; }

    }
}
