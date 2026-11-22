using ClientOrdersReport.Models;

namespace ClientOrdersReport.Presentation
{
    public class FileReportWriter : IReportWriter
    {
        public void WriteReport(List<ClientReport> reports)
        {
            // new file every time
            string fileName = "report_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";

            using (StreamWriter writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            {
                foreach (ClientReport report in reports)
                {
                    writer.WriteLine(report.ClientId + " " + report.OrdersSum.ToString("0.00") + " " + report.FavoriteItemName);
                }
            }

            Console.WriteLine("Report saved to file: " + fileName);
        }
    }
}
