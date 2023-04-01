using ExcelDataReader;
using ICTTaxApi.DTOs;
using Microsoft.Net.Http.Headers;
using System.Data;


namespace ICTTaxApi.Tools
{
    public  static class ExcelMapperDevice  
    {
        public static async Task<Tuple<List<TransactionDTO>,string>> Map(IFormFile file)
        {
            try
            {
                HttpResponseMessage ResponseMessage = null;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader excelReader = null;
                List<TransactionDTO> resultDTOList = null;
                string message = string.Empty;

                if (file != null)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        var fileContent = reader.ReadToEnd();
                        var parsedContentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
                        var fileName = parsedContentDisposition.FileName.Value;

                        if (memoryStream != null && memoryStream.Length > 0)
                        {
                            if (fileName.EndsWith(".xls") || fileName.EndsWith(".xlsx"))
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream);
                            else
                                message = "The file format is not supported.";

                            dsexcelRecords = excelReader.AsDataSet();
                            excelReader.Close();

                            if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                            {
                                resultDTOList = new List<TransactionDTO>();
                                DataTable transactionRecords = dsexcelRecords.Tables[0];
                                for (int i = 0; i < transactionRecords.Rows.Count; i++)
                                {
                                    TransactionDTO newTransaction = new TransactionDTO();
                                    newTransaction.TransactionDate = Convert.ToString(transactionRecords.Rows[i][0]);
                                    newTransaction.ClientName = Convert.ToString(transactionRecords.Rows[i][1]);
                                    newTransaction.Description = Convert.ToString(transactionRecords.Rows[i][2]);
                                    newTransaction.Amount = Convert.ToDecimal(transactionRecords.Rows[i][3]);
                                    resultDTOList.Add(newTransaction);
                                }

                                if (resultDTOList.Count == 0)
                                    message = "Something Went Wrong!, The Excel file uploaded has failed.";
                            }
                            else
                                message = "Selected file is empty.";
                        }
                        else
                            message = "Invalid File.";
                    }
                }
                else
                    message = "Invalid File.";
                return new Tuple<List<TransactionDTO>, string>(resultDTOList, message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
