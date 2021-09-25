using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace TicTacToeServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:5102")
                .UseStartup<StartUp>();
        }
    }
}
