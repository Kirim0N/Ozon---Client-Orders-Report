using ClientOrdersReport.Models;

namespace ClientOrdersReport.Data
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
    }
}
