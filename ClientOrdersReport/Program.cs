using ClientOrdersReport.Business;
using ClientOrdersReport.Data;
using ClientOrdersReport.Exceptions;
using ClientOrdersReport.Models;
using ClientOrdersReport.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace ClientOrdersReport
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Need to pass client ids");
                Console.WriteLine("Example: program.exe 1010");
                Console.WriteLine("Or: program.exe console 1010 1020");
                Console.WriteLine("Or: program.exe file 1010");
                return;
            }

            string mode = "console";
            int start = 0;

            // check first argument for mode
            if (args[0] == "console" || args[0] == "file")
            {
                mode = args[0];
                start = 1;
            }

            if (start >= args.Length)
            {
                Console.WriteLine("Client ids not specified");
                return;
            }

            List<long> clientIds = new List<long>();
            for (int i = start; i < args.Length; i++)
            {
                long id;
                if (long.TryParse(args[i], out id))
                {
                    clientIds.Add(id);
                }
                else
                {
                    Console.WriteLine("Invalid id: " + args[i]);
                    return;
                }
            }

            // setup DI
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IOrderRepository, CsvOrderRepository>();
            services.AddTransient<IReportService, ReportService>();

            if (mode == "file")
            {
                services.AddTransient<IReportWriter, FileReportWriter>();
            }
            else
            {
                services.AddTransient<IReportWriter, ConsoleReportWriter>();
            }

            ServiceProvider provider = services.BuildServiceProvider();

            IReportService reportService = provider.GetRequiredService<IReportService>();
            IReportWriter writer = provider.GetRequiredService<IReportWriter>();

            // ClientNotFoundException will be thrown if client not found
            List<ClientReport> reports = reportService.GetReports(clientIds);
            writer.WriteReport(reports);
        }
    }
}
