using Microsoft.AspNetCore.Builder;
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

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    builder
                        .WithOrigins("http://localhost:5101")
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

            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyConnection>("/lobby");
                endpoints.MapHub<GameConnection>("/game");
            });
        }
    }
}
