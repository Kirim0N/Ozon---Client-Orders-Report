using System.Globalization;
using ClientOrdersReport.Models;

namespace ClientOrdersReport.Data
{
    public class CsvOrderRepository : IOrderRepository
    {
        private string filePath;

        public CsvOrderRepository()
        {
            // file should be next to the executable
            filePath = Path.Combine(AppContext.BaseDirectory, "orders_log.csv");
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();

            if (!File.Exists(filePath))
            {
                throw new Exception("File orders_log.csv not found!");
            }

            using (StreamReader reader = new StreamReader(filePath))
            {
                string header = reader.ReadLine(); // skip header

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(',');

                    if (parts.Length != 5)
                        continue;

                    Order order = new Order();
                    order.ClientId = long.Parse(parts[0]);
                    order.OrderId = long.Parse(parts[1]);
                    order.ItemName = parts[2];
                    order.ItemQuantity = int.Parse(parts[3]);
                    order.ItemPrice = float.Parse(parts[4], CultureInfo.InvariantCulture);

                    orders.Add(order);
                }
            }

            return orders;
        }
    }
}
