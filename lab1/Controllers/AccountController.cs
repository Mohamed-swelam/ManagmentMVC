using lab1.Models;
using lab1.ViewModels.AccountVM;
using lab1.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace lab1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RgisterVM VM)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = VM.Email,
                    Email = VM.Email,
                    FirstName = VM.FirstName,
                    LastName = VM.LastName,
                    Age = VM.Age,
                    Address = VM.Address
                };

                var result = await userManager.CreateAsync(user, VM.Password);

                if (result.Succeeded)
                {
                    //var res = await userManager.AddToRoleAsync(user, "Admin");
                    var res = await userManager.AddToRoleAsync(user, "User");
                    if (res.Succeeded)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("FirstName", user.FirstName ?? ""),
                            new Claim("LastName", user.LastName ?? "")
                        };
                        await signInManager.SignInWithClaimsAsync(user, false, claims);
                        return RedirectToAction("Index", "Student");
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(VM);
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(VM);
                }
            }
            return View(VM);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(loginVM.Email);
                if (user != null)
                {
                    var result = await userManager.CheckPasswordAsync(user, loginVM.Password);
                    if (result)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("FirstName", user.FirstName ?? ""),
                            new Claim("LastName", user.LastName ?? "")
                        };
                        await signInManager.SignInWithClaimsAsync(user, loginVM.RememberMe, claims);

                        return RedirectToAction("Index", "Student");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Email Address.");
            }

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
