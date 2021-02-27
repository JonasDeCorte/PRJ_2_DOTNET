using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using projecten2.Data;

namespace projecten2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (ApplicationDbContext context = new ApplicationDbContext()) {
             //   context.Database.EnsureDeleted();
               // context.Database.EnsureCreated();
              
            //Console.WriteLine("Database created");
             //   }
           
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

