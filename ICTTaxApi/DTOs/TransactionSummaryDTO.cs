namespace ICTTaxApi.DTOs
{
    public class TransactionSummaryDTO
    {
        public int TotalClients { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public int FileCount { get; set; }
    }
}
