using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vahid_C_Web_Proj.Data;
using Vahid_C_Web_Proj.Models.ViewModel;

namespace Vahid_C_Web_Proj.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext _context;
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        SignInManager<IdentityUser> _signInManager;
        public AccountController(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            if (ModelState.IsValid)
            {
                if (model.RoleId != null)
                {
                    var identityUser = new IdentityUser();
                    identityUser.Email = model.Email;
                    identityUser.UserName = model.Email;

                    var identityResult = await _userManager.CreateAsync(identityUser, model.Password);
                    if (identityResult.Succeeded)
                    {
                        var identityRole = await _roleManager.FindByIdAsync(model.RoleId);

                        var roleResult = await _userManager.AddToRoleAsync(identityUser, identityRole.Name);
                        if (roleResult.Succeeded)
                            return View("Login");
                        else
                        {
                            ModelState.AddModelError("", "Problemen met toekennen van rol!");
                            return View();
                        }
                    }
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Geen rol geselecteerd!");
                }
            }
            return View();
        }
        #endregion

        //#region Login
        //[HttpGet]
        //public IActionResult Login()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> LoginAsync(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var identityUser = await _userManager.FindByEmailAsync(model.Email);
        //        if (identityUser != null)
        //        {
        //            var signInResult = await _signInManager.PasswordSignInAsync(identityUser, model.Password, false, false);
        //            if (signInResult.Succeeded)
        //            {
        //                var identityRole = await _userManager.GetRolesAsync(identityUser);
        //                if (identityRole.Contains("Admin"))
        //                    return RedirectToAction("Index", "Admin");
        //                else if (identityRole.Contains("Student"))
        //                    return RedirectToAction("Index", "Student");
        //                else
        //                    return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Verkeerd wachtwoord!");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Gebruiker niet gevonden!");
        //        }
        //    }
        //    return View();
        //}
        //#endregion

        #region login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel login)
        {
            var identityUser = await _userManager.FindByEmailAsync(login.Email);
            if (identityUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(identityUser.UserName, login.Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Probleem met inloggen");
            return View();
        }


        #endregion

        #region Logout
        public async Task<IActionResult> LogoutAsync()
            {
                await _signInManager.SignOutAsync();
                return View("Login");
            }
            #endregion

            #region Identity
            [HttpGet]
            public IActionResult Identity()
            {
                var identityViewModel = new IdentityViewModel
                {
                    Roles = _roleManager.Roles,
                    Users = _userManager.Users
                };
                return View(identityViewModel);
            }
        }
        #endregion
}
