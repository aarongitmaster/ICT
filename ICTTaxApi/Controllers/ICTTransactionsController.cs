using AutoMapper;
using ICTTaxApi.DTOs;
using ICTTaxApi.Services;
using Microsoft.AspNetCore.Mvc;
using ICTTaxApi.Tools;

namespace ICTTaxApi.Controllers
{
    [ApiController]
    [Route("api/client/")]
    public class ICTTransactionController : ControllerBase
    {
        private readonly IICTTaxService service;
        private readonly ILogger<ICTTransactionController> logger;
        private readonly IMapper mapper;

        public ICTTransactionController(IICTTaxService service,
            ILogger<ICTTransactionController> logger, IMapper mapper)
        {
            this.service = service;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpGet("transactions",Name ="GetTransactions")]
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
        public async Task<ActionResult> Post(IFormFile file)
        {
            try
            {
                var (model, message) = await ExcelMapperDevice.Map(file);

                if (model != null)
                {
                    TryUpdateModelAsync(model);

                    if (ModelState.IsValid)
                    {
                        service.AddTransactions(model, "pruebas");
                    }
                    else
                        return BadRequest("An error ocurred while validating the request. Somes fields are in incorrect format.");


                    return CreatedAtRoute("GetTransactions",null);
                }
                else
                    return BadRequest(message);
                
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpGet("{clientname}")]
        public async Task<ActionResult<List<TransactionDTO>>> Get(string clientname)
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
                return BadRequest();
            }
        }

        [HttpGet("summary")]
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
                return BadRequest();
            }
        }
    }
}
