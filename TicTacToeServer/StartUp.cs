using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


            services
                .AddDataProtection()
                .SetApplicationName("Let's starts with TicTacToe");

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".AspNetCore.TicTacToe";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = false;
                });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    builder
                        .WithOrigins("http://localhost:5101", "https://localhost:5103")
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                    );
            });


            LobbyManager.InitializeGameQueues();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyConnection>("/lobby");
                endpoints.MapHub<GameConnection>("/game");
            });
        }
    }
}
