using ClientOrdersReport.Models;

namespace ClientOrdersReport.Presentation
{
    public class ConsoleReportWriter : IReportWriter
    {
        public void WriteReport(List<ClientReport> reports)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            foreach (ClientReport report in reports)
            {
                // output with 2 decimal places
                Console.WriteLine(report.ClientId + " " + report.OrdersSum.ToString("0.00") + " " + report.FavoriteItemName);
            }
        }
    }
}
