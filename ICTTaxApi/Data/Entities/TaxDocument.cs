using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.Data.Entities
{
    public class TaxDocument
    {
        public int Id { get; set; }
        [Required]
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }
        public decimal Total{ get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
