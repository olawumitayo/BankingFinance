using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace TheCoreBanking.Finance
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
       .Build();




        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(Configuration)
            //    .CreateLogger();

            ColumnOptions columnOptions = new ColumnOptions();
            // Don't include the Properties XML column.
            columnOptions.Store.Remove(StandardColumn.Properties);
            // Do include the log event data as JSON.
            columnOptions.Store.Add(StandardColumn.LogEvent);
            // Add additional VerificationCode column
            columnOptions.AdditionalDataColumns = new Collection<DataColumn>
                 {
            new DataColumn {DataType = typeof (string), ColumnName = "VerificationCode"},
            };

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           .Enrich.FromLogContext()
           .WriteTo.MSSqlServer(Configuration.GetConnectionString("DefaultConnection"), "Logs", autoCreateSqlTable: true, columnOptions: columnOptions, schemaName: "GeneralSetup")

           .CreateLogger();

            //If something goes wrong and you want to find out.
            //Serilog.Debugging.SelfLog.Enable(msg =>
            //{
            //    Debug.Print(msg);
            //    Debugger.Break();
            //});
            try
            {
                Log.Information("Host starting...");

                BuildWebHost(args).Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
                  .UseStartup<Startup>()
                  .UseConfiguration(Configuration)
                  .UseSerilog()
                  .Build();
    }
    }

