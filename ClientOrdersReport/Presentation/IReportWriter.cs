using ClientOrdersReport.Models;

namespace ClientOrdersReport.Presentation
{
    public interface IReportWriter
    {
        void WriteReport(List<ClientReport> reports);
    }
}
