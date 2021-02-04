using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SB.VirtualStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

//https://www.youtube.com/watch?v=4nA4OFgxRtM&t=1881s

//https://www.youtube.com/watch?v=fmvcAzHpsk8&t=8425s


// Instalar .Net Core para IIS

// https://www.youtube.com/watch?v=iaFBQcd26HE --- Tutorial
// https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-2.2.5-windows-hosting-bundle-installer