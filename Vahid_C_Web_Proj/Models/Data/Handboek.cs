using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Vahid_C_Web_Proj.CustomValidation;


namespace Vahid_C_Web_Proj.Models.Data
{
    public class Handboek
    {
        public int HandboekId { get; set; }
        public string Titel { get; set; }
        public double Kostprijs { get; set; }
        public string Afbeelding { get; set; }
        
        [UitgifteDatum]
        public DateTime UitgifteDatum { get; set; }


    }
}
