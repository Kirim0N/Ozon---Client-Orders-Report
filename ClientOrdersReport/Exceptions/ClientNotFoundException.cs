namespace ClientOrdersReport.Exceptions
{
    // Custom exception when client is not found in orders
    public class ClientNotFoundException : Exception
    {
        public long ClientId { get; }

        public ClientNotFoundException(long clientId) 
            : base($"Client with id = {clientId} not found")
        {
            ClientId = clientId;
        }
    }
}
