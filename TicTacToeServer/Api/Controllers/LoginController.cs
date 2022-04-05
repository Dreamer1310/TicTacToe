using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeServer.Api.Controllers
{
    public class LoginController : ControllerBase
    {
        // Dummy ID for each player so they could be discrete
        // Ideally players would be saved in db and each would have their own unique ID.
        public static Int64 ID = 1;

        // Returns cookie so player could open socket using this cookie
        [HttpPost]
        public async Task<Boolean> SignIn()
        {
            try
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ID.ToString()));
                ID++;
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principals = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principals);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
