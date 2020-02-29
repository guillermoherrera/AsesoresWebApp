using AsesoresAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AsesoresAPI.Controllers
{
    [ApiController]
    [Route(Constants.WS_GROUP_CARTERA)]
    public class CarteraController : ControllerBase
    {
        private ICarteraService _carteraService;
        private readonly ILogger<CarteraController> _logger;

        private bool headerUser = false;

        //[FromHeader(Name = "apiKey")]
        //[Required]
        //public string apiKey { get; set; }

        public CarteraController(ICarteraService carteraService, ILogger<CarteraController> logger)
        {
            _carteraService = carteraService;
            _logger = logger;
        }

        [HttpGet(Constants.WS_CARTERA)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCartera([FromHeader] int producoId)
        {
            try
            {
                //[FromRoute] int productoId, 
                //var usuarioId = Request.Headers[Constants.FirebaseUserId];
                
               var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");//, ((uniqueDevice is null) ? "" : uniqueDevice));

                var respuesta = await _carteraService.GetCartera("", "");

                if (respuesta == null)
                    throw new Exception("NO SE OBTUVIERON RESULTADOS.");

                return Ok(respuesta);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                return Helpers.ExceptionErrors.Exception(e.Class, string.Format("{0}.", e.Message), e);
            }
            catch (Exception e)
            {
                return Helpers.ExceptionErrors.Exception(Helpers.ExceptionErrors.RESPONSE_INTERNAL_SERVER_ERROR, string.Format("ERROR {0}.", e.Message), e);
            }
        }

        [HttpGet(Constants.WS_CARTERA_CONTRATO_DETALLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarteraContratoDetalle([FromRoute] string usuarioId, [FromRoute] int productoId, [FromRoute] long contratoId, [FromHeader] string uniqueDevice)
        {
            return Ok();
        }

        [HttpGet(Constants.WS_CARTERA_CREDITO_DETALLE)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCarteraCreditoDetalle([FromRoute] string usuarioId, [FromRoute] int productoId, [FromRoute] long contratoId, [FromRoute] long creditoId, [FromHeader] string uniqueDevice)
        {
            return Ok();
        }


        /*
        [HttpGet(Constants.WS_CARTERA_GRUPOS)]
        public async Task<IActionResult> GetGrupos([FromRoute]string usuarioId)
        {
            try
            {
                var cartera = await _carteraService.GetGrupos(usuarioId);
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();

                _logger.LogInformation("Usuario: {0}  IP: {1} .", usuarioId, ip);

                if (cartera == null)
                    return BadRequest(new { message = "NO SE OBTUVIERON REGISTROS." });

                return Ok(cartera);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        */

        /*
        [HttpGet(Constants.WS_CARTERA_GRUPOS_DETALLE)]
        public async Task<IActionResult> GetGruposDetalle([FromRoute] string usuarioId, string grupoId)
        {

        }
        */

        //private void ContainUser()
        //{
        //    if (!Request.Headers.ContainsKey(Constants.FirebaseUserId))
        //        throw new Exception
            
        //}
    }
}