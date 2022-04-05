using EmbedIO;
using EmbedIO.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TicTacToeServer.Communication.Connection;
using TicTacToeServer.Game;
using TicTacToeServer.Lobby;

namespace TicTacToeServer
{
    public class StartUp
    {
        public IConfiguration Configuration { get; private set; }

        public StartUp(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            services.AddControllers();

            // Cookie encode/decode key
            services
                .AddDataProtection()
                .SetApplicationName("Let's starts with TicTacToe");


            // Cookie configs
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".Auth.AspNetCore.TicTacToe";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = false;
                });


            // Cors to allow every origin connect
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    builder
                        //.WithOrigins("http://localhost:5101", "https://localhost:5103", "http://10.172.22.102:5105")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        //.AllowCredentials()
                    );
            });

            // Init Lobby queues (quick games)
            LobbyManager.InitializeGameQueues();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                // Configure socket endpoints
                endpoints.MapHub<LobbyConnection>("/lobby");
                endpoints.MapHub<GameConnection>("/game");

                // Configure api endpoitns
                endpoints.MapControllerRoute(
                    name: "Api",
                    pattern: "api/{controller=Login}/{action=SignIn}/{id?}");
            });
        }
    }
}
