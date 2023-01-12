using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lector> Lector { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<AcademieJaar> AcademieJaars { get; set; }
        public DbSet<RolesCheck> CheckRole { get; set; }
        public DbSet<Handboek> Handboeks { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Inschrijving> Inschrijvingen { get; set; }
        public DbSet<Vak> Vaks { get; set; }
        public DbSet<VakLector> VakLectors { get; set; }
       
       
      




















    }
}
