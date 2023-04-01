namespace ICTTaxApi.Data.Entities
{
    public class TaxDocument
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
        public int Total{ get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
