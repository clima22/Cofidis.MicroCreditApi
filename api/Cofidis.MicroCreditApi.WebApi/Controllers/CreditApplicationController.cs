using Cofidis.MicroCreditApi.Core.Application.CreditApplication;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Cofidis.MicroCreditApi.WebApi.Controllers
{
    /// <summary>
    /// Interface para gestão de pedidos de créditos
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    //[ProducesResponseType((int)HttpStatusCode.Forbidden)]
    //[ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public class CreditApplicationController : ControllerBase
    {
        private readonly ILogger<CreditApplicationController> _logger;
        private readonly ICreditApplicationService _creditApplicationService;
        public CreditApplicationController(ILogger<CreditApplicationController> logger
            , ICreditApplicationService creditApplicationService) 
        {
            _logger = logger;
            _creditApplicationService = creditApplicationService;
        }

        /// <summary>
        /// Permite validar e criar um pedido de crédito
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreditApplicationCreateResponseDto))]
        public ActionResult<CreditApplicationCreateResponseDto> POST(CreditApplicationCreateDto request)
        {
            try
            {
                _logger.LogDebug(LogBuildReqResp.BuildReqInput(HttpContext, request));

                var response = _creditApplicationService.Create(request);

                _logger.LogDebug(LogBuildReqResp.BuildResOutput(HttpContext, response));

                if (response.Result)
                    return Created("", response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
