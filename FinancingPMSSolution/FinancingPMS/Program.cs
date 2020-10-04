using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;

namespace FinancingPMS
{
    public class Program
    {

        public static IConfiguration configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile(path: "appsettings.json")
                                            .Build();
        public static void Main(string[] args)
        { 
            //DateTime currentDate = DateTime.Now;
            Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(configuration)
                                    .WriteTo.File(new JsonFormatter(), 
                                    path: @"c:\logs\log.json", 
                                    shared: true,
                                    rollingInterval:RollingInterval.Day)
                                    .CreateLogger();

            try
            {
                Log.Information(messageTemplate: "Started creating web host builder");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Error(exception: ex, messageTemplate: "Error occured while creating web host builder");
            }
            finally
            {
                Log.Information(messageTemplate: "Completed creatign web host builder");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .ConfigureLogging(logging =>
             {
                 logging.ClearProviders();
                 logging.AddConsole();
                 logging.AddDebug();
             })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseSerilog();
    }
}
