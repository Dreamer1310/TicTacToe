using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net;

namespace TicTacToePresentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();       
            services
                .AddDataProtection()
                .SetApplicationName("Let's starts with TicTacToe");

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.Cookie.Name = ".AspNetCore.TicTacToe";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = false;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "632646369145-08rsrf0m6nhvasoi9usgen4tphbolvdi.apps.googleusercontent.com";
                    options.ClientSecret = "Q_nAao25KkoIc_F-7tx-Th0a";
                    options.Scope.Add("profile");
                    options.Events.OnCreatingTicket = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetProperty("picture").GetString()));
                        return Task.CompletedTask;
                    };
                })
                .AddFacebook(options =>
                {
                    options.AppId = "274211941192206";
                    options.AppSecret = "edeed85b1b6d2fdb29a2eaf309e7299f";
                    options.Fields.Add("picture");
                    options.Events.OnCreatingTicket = context =>
                    {
                        context.Identity.AddClaim(new Claim("picture", context.User.GetProperty("picture").GetProperty("data").GetProperty("url").ToString()));
                        return Task.CompletedTask;
                    };
                }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
