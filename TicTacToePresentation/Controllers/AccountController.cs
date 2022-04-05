using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicTacToePresentation.Controllers
{
    public class AccountController : Controller
    {
        private static Int64 ID = 1;

        public ActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Validate(String username)
        {   
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ID.ToString()));
            ID++;
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
            return Redirect("/home/index");
        }

        public ActionResult GoogleAuth()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("AuthResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public ActionResult FacebookAuth()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("AuthResponse") };
            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> AuthResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Secured", "Home");
        }
    }
}
