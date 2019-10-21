using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CompanyMVC.ConnectDB;
using CompanyMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ConfArchUser> _userManager;
        private readonly SignInManager<ConfArchUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ConfArchUser> userManager, SignInManager<ConfArchUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var user = new ConfArchUser() { UserName = account.Email, Email = account.Email, BirthDate = account.BirthDate };
            var result = await _userManager.CreateAsync(
                user, account.Password);

            if (!await _roleManager.RoleExistsAsync("Organizer"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Organizer" });
            if (!await _roleManager.RoleExistsAsync("Speaker"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Speaker" });

            await _userManager.AddToRoleAsync(user, account.Role);
            await _userManager.AddClaimAsync(user, new Claim("technology", account.Technology));

            if (result.Succeeded)
            {
                return View("RegistrationConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("error", error.Description);
            }

            return View(account);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel account, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result =
                    await
                        _signInManager.PasswordSignInAsync(account.Email, account.Password, account.RememberMe,
                            lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);
                if (result.RequiresTwoFactor)
                {
                    //
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(account);

            }
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return View("LoggedOut");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Conference");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}