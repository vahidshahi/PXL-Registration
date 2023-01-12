using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vahid_C_Web_Proj.Models.Data;

namespace Vahid_C_Web_Proj.Data.DefaultData
{
    public class SeedData
    {
        static AppDbContext? _context;
        static RoleManager<IdentityRole>? _roleManager;
        static UserManager<IdentityUser>? _userManager;
        public static async Task EnsurePopulatedAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {

                _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                _userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                await AddRolesAsync();
                await CreateIdentityRecordAsync(userName: "Vahid", email: "student@pxl.be", pwd: "Student123!", Roles.Student);
                await CreateIdentityRecordAsync(userName: "Kristof", email: "admin@pxl.be", pwd: "Admin456!", Roles.Admin);
                LoadData();

            }


        }



        private static void LoadData()
        {


            var gebruiker = new Gebruiker() { Email = "vvvahid@gmail.com", Naam = "Khosrowshahi Fard", Voornaam = "Vahid" };
            var gebruikerLector = new Gebruiker() { Email = "kristof.palmaers@pxl.be", Naam = "Kristof", Voornaam = "Palmaers" };
            //check if data is already loaded
            if (_context!.Gebruikers.Any())
            {
                return;
            }
            else
            {

                _context.Gebruikers.Add(gebruiker);
                _context.Gebruikers.Add(gebruikerLector);
                _context.SaveChanges();
                var gebruikerLectorDB = _context.Gebruikers.FirstOrDefault(x => x.Email.Equals(gebruikerLector.Email))!;
                var gebruikerDB = _context.Gebruikers.FirstOrDefault(x => x.Email.Equals(gebruiker.Email))!;

                var student = new Student() { GebruikerId = gebruikerDB.GebruikerId };
                _context.Students.Add(student);
                _context.SaveChanges();
                var studentDB = _context.Students.FirstOrDefault(x => x.GebruikerId == gebruikerDB.GebruikerId);

                var lector = new Lector() { GebruikerId = gebruikerLectorDB.GebruikerId };
                _context.Lector.Add(lector);
                _context.SaveChanges();
                var lectorDB = _context.Lector.FirstOrDefault(x => x.GebruikerId == gebruikerLectorDB.GebruikerId);

                var handboek = new Handboek() { Titel = "C# Web1", UitgifteDatum = DateTime.Now.AddYears(-3), Kostprijs = 1, Afbeelding = "~/img/afCSharp.jpg" };
                _context.Handboeks.Add(handboek);
                _context.SaveChanges();
                var handboekDB = _context.Handboeks.FirstOrDefault(x => x.Titel.Equals("C# Web1"));

                var vak = new Vak() { HandboekID = handboekDB.HandboekId, Studiepunten = 6, VakNaam = "C# Web 1" };
                _context.Vaks.Add(vak);
                _context.SaveChanges();
                var vakDB = _context.Vaks.FirstOrDefault(x => x.VakNaam.Equals("C# Web 1"));

                var vakLector = new VakLector() { LectorId = lectorDB.LectorId, VakId = vakDB.VakId };
                _context.VakLectors.Add(vakLector);
                _context.SaveChanges();
                var vakLectorDB = _context.VakLectors.FirstOrDefault(x => x.LectorId == lectorDB.LectorId && x.VakId == vakDB.VakId);

                var startdatum = new DateTime(2021, 9, 20);
                var academieJaar = new AcademieJaar() { StartDatum = startdatum };
                _context.AcademieJaars.Add(academieJaar);
                _context.SaveChanges();
                var academieJaarDB = _context.AcademieJaars.FirstOrDefault(x => x.StartDatum == startdatum);

                var inschrijving = new Inschrijving() { AcademieJaarId = academieJaarDB.AcademieJaarId, VakLectorId = vakLectorDB.VakLectorId, StudentId = studentDB.StudentId };
                _context.Inschrijvingen.Add(inschrijving);
                _context.SaveChanges();
            }
        }


        //add roles
        private static async Task AddRolesAsync()
        {
            if (!_context.Roles.Any())
            {
                await AddRoleAsync(_roleManager, Roles.Admin);
                await AddRoleAsync(_roleManager, Roles.Lector);
                await AddRoleAsync(_roleManager, Roles.Student);
            }

        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> _roleManager, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private static async Task CreateIdentityRecordAsync(string userName, string email, string pwd, string role)
    {

        if (_userManager != null && await _userManager.FindByEmailAsync(email) == null &&
                await _userManager.FindByNameAsync(userName) == null)
        {
            var identityUser = new IdentityUser() { Email = email, UserName = userName };
            var result = await _userManager.CreateAsync(identityUser, pwd);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(identityUser, role);
            }
            }
        }


     


}
}
