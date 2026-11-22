using ClientOrdersReport.Data;
using ClientOrdersReport.Exceptions;
using ClientOrdersReport.Models;

namespace ClientOrdersReport.Business
{
    public class ReportService : IReportService
    {
        private IOrderRepository orderRepository;

        public ReportService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public List<ClientReport> GetReports(List<long> clientIds)
        {
            List<Order> allOrders = orderRepository.GetAllOrders();
            List<ClientReport> result = new List<ClientReport>();

            foreach (long clientId in clientIds)
            {
                // get all orders for this client
                List<Order> clientOrders = allOrders
                    .Where(o => o.ClientId == clientId)
                    .ToList();

                if (clientOrders.Count == 0)
                {
                    throw new ClientNotFoundException(clientId);
                }

                // calculate total sum
                float sum = clientOrders.Sum(o => o.ItemQuantity * o.ItemPrice);

                // find favorite item by total quantity
                string favorite = clientOrders
                    .GroupBy(o => o.ItemName)
                    .Select(g => new
                    {
                        Name = g.Key,
                        TotalQty = g.Sum(o => o.ItemQuantity)
                    })
                    .OrderByDescending(x => x.TotalQty)
                    .First()
                    .Name;

                ClientReport report = new ClientReport();
                report.ClientId = clientId;
                report.OrdersSum = sum;
                report.FavoriteItemName = favorite;

                result.Add(report);
            }

            return result;
        }
    }
}
