using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.Data.Entities
{
    public class Transaction
    {
        public int Id{ get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [StringLength(maximumLength: 120, ErrorMessage = "The field {0} can't have more than {1} characters")]
        public string? Description { get; set; }
        [Required]
        [RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int TaxDocumentId { get; set; }
        public TaxDocument TaxDocument { get; set; }
    }
}
