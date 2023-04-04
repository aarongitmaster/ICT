﻿using ExcelDataReader;
using ICTTaxApi.DTOs;
using Microsoft.Net.Http.Headers;
using System.Data;


namespace ICTTaxApi.Tools
{
    public  static class ExcelMapperDevice  
    {
        public static async Task<Tuple<List<TransactionCreationDTO>,string>> Map(IFormFile file)
        {
            try
            {
                HttpResponseMessage ResponseMessage = null;
                DataSet dsexcelRecords = new DataSet();
                IExcelDataReader excelReader = null;
                List<TransactionCreationDTO> resultDTOList = null;
                string message = string.Empty;
                var filename = file.FileName;

                if (file != null)
                {
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        MemoryStream memoryStream = new MemoryStream();
                        await file.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;

                        if (memoryStream != null && memoryStream.Length > 0) {

                            if (filename.EndsWith(".xls") || filename.EndsWith(".xlsx") || filename.EndsWith(".xltx"))
                            {
                                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                                excelReader = ExcelReaderFactory.CreateOpenXmlReader(memoryStream);
                            }
                            else
                                message = "The file format is not supported.";

                            dsexcelRecords = excelReader.AsDataSet();
                            excelReader.Close();

                            if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
                            {
                                resultDTOList = new List<TransactionCreationDTO>();
                                DataTable transactionRecords = dsexcelRecords.Tables[0];
                                for (int i = 1; i < transactionRecords.Rows.Count; i++)
                                {
                                    TransactionCreationDTO newTransaction = new TransactionCreationDTO();
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
                                message = "The excel file is empty.";
                        }
                        else
                            message = "Invalid File.";
                    }
                }
                else
                    message = "Invalid File.";
                return new Tuple<List<TransactionCreationDTO>, string>(resultDTOList, message);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
