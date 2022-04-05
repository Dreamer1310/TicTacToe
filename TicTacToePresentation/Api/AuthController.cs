using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicTacToePresentation.Api
{
    public class AuthController : ControllerBase
    {


        [HttpPost]
        public async Task<JsonResult> GetCookies(String userId)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, userId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId));
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);


            return new JsonResult(new
            {
                Cookies = Request.Cookies
            });
        }

    }
}
