using System.ComponentModel.DataAnnotations;

namespace ICTTaxApi.DTOs
{
    public class TransactionResponseDTO: PaginationFilter
    {
        public List<TransactionDTO> Transactions { get; set; }

        public TransactionResponseDTO(int pageNumber,
                int pageSize,
                int lastPage,
                int totalPages) 
            :base(pageNumber, pageSize,1, lastPage, totalPages)
        {
        }

    }
}
