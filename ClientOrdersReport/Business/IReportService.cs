using ClientOrdersReport.Models;

namespace ClientOrdersReport.Business
{
    public interface IReportService
    {
        List<ClientReport> GetReports(List<long> clientIds);
    }
}
