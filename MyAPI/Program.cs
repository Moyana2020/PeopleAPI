using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
         
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                })
               .UseStartup<Startup>()
               .UseSerilog(
                     (hostingContext, loggerConfiguration) =>
                         loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                 )
               ;
            });

    }
}
