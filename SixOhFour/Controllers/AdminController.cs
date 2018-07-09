using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using curmudgeon.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixOhFour.Models;
using SixOhFour.ViewModels;

namespace SixOhFour.Controllers
{
    public class AdminController : Controller
    {
        private readonly SixOhFourDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, DisplayName = model.DisplayName };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> UpdateUser()
        {
            // needs a ViewModel
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            return View(thisUser);
        }

        [HttpPost]
        //needs to take a ViewModel version of the user as a parameter.

        public async Task<IActionResult> UpdateUser(UpdateUserViewModel model)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            thisUser.DisplayName = model.DisplayName;
            thisUser.Email = model.Email;
            var result = await _userManager.UpdateAsync(thisUser);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete()
        {
            // needs a ViewModel
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            return View(thisUser);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed()
        {

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var thisUser = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(thisUser);
            await _signInManager.SignOutAsync();
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel model)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}