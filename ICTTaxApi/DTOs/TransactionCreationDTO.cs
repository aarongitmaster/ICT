using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.DTOs
{
    public class TransactionCreationDTO
    {
        [Required]
        public string TransactionDate { get; set; }
        [Required]
        public string ClientName { get; set; }
        [StringLength(maximumLength: 120, ErrorMessage = "The field {0} can't have more than {1} characters")]
        public string Description { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string FileUrl { get; set; }
        [Required]
        public string FileName { get; set; }
        
        public string UploadedDate { get; set; }

        public int TaxDocumentId { get; set; }

        public int ClientId { get; set; }
    }
}
