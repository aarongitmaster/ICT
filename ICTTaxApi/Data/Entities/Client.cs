using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string ClientName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
