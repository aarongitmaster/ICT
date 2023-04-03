using AutoMapper;
using ICTTaxApi.DTOs;
using ICTTaxApi.Services;
using Microsoft.AspNetCore.Mvc;
using ICTTaxApi.Tools;

namespace ICTTaxApi.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    public class ICTTransactionController : ControllerBase
    {
        private readonly IICTTaxService service;
        private readonly ILogger<ICTTransactionController> logger;

        public ICTTransactionController(IICTTaxService service,
            ILogger<ICTTransactionController> logger, IMapper mapper)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        [HttpGet("transactions",Name ="GetTransactions")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TransactionDTO>>> Get()
        {
            try
            {
                var transactionList = await service.GetTransactions();

                if (transactionList == null)
                {
                    return NotFound();
                }

                return transactionList;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType( StatusCodes.Status201Created)]
        public async Task<ActionResult> Post(IFormFile file)
        {
            try
            {
                var (DTOlist, message) = await ExcelMapperDevice.Map(file);

                var model = DTOlist;

                if (model != null)
                {

                    //if (await TryUpdateModelAsync<List<TransactionDTO>>(model))
                    //{
                        this.logger.LogInformation("Start request add file {0} to system.");

                        service.AddTransactions(DTOlist, file.FileName);
                    //}
                    //else
                      //  return BadRequest("An error ocurred while validating the request. Somes fields are in incorrect format.");
                    //}

                    this.logger.LogInformation("File successfully updated.");

                    return CreatedAtRoute("GetTransactions",null);
                }
                else
                    return BadRequest(message);
                
            }
            catch (Exception ex)
            {
                this.logger.LogError("An error ocurred while adding transactions.{0}", ex.Message);
                return null;
            }
        }

        [HttpGet("client/{clientname}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<TransactionDTO>),StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TransactionDTO>>> Get([FromRoute]string clientname)
        {
            try
            {
                var transactionList = await service.GetClientTransactions(clientname);

                if (transactionList == null)
                {
                    return NotFound();
                }

                return transactionList;
            }
            catch (Exception ex)
            {
                this.logger.LogError("An error ocurred while retrieving client transactions for client: {0}.{1}", clientname,ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TransactionSummaryDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionSummaryDTO>> GetSummary()
        {
            try
            {
                var summary = await service.GetSummary();

                if (summary == null)
                {
                    return NotFound();
                }

                return summary;
            }
            catch (Exception ex)
            {
                this.logger.LogError("An error ocurred while retrieving the summary of transactions.{0}", ex.Message);
                return BadRequest();
            }
        }
    }
}
