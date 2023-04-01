using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.Data.Entities
{
    public class Transaction
    {
        public int Id{ get; set; }
        public int TransactionDate { get; set; }
        
        [StringLength(maximumLength: 120, ErrorMessage = "The field {0} can't have more than {1} characters")]
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int taxDocumentId { get; set; }
        public TaxDocument TaxDocument { get; set; }
    }
}
