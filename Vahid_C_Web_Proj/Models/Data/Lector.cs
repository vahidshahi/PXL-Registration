namespace Vahid_C_Web_Proj.Models.Data
{
    public class Lector
    {
      
        public int LectorId { get; set; }
        public int GebruikerId { get; set; }
        public Gebruiker Gebruiker { get; set; }
    }
}
