namespace ClientOrdersReport.Models
{
    public class ClientReport
    {
        public long ClientId { get; set; }
        public float OrdersSum { get; set; }
        public string FavoriteItemName { get; set; }

        public ClientReport()
        {
            FavoriteItemName = "";
        }
    }
}
