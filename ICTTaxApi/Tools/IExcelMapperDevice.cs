using ExcelDataReader;
using ICTTaxApi.DTOs;
using Microsoft.Net.Http.Headers;
using System.Data;


namespace ICTTaxApi.Tools
{
    public interface IExcelMapperDevice
    {
        Task<List<TransactionDTO>> Map(IFormFile file);
    }
}
