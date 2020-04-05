using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AsesoresAPI.Entities;
using AsesoresAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AsesoresAPI.Controllers
{
    [Route(Constants.WS_GROUP_REGISTRO)]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private IRegistroService _registroService;
        private readonly ILogger<RegistroController> _logger;

        public RegistroController(IRegistroService RegistroService, ILogger<RegistroController> logger)
        {
            _registroService = RegistroService;
            _logger = logger;
        }

        [HttpPost(Constants.WS_REGISTRO_CLIENTE_BURO)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegistraCliente(Cliente cliente)
        {
            try 
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("Usuario: {0} IP: {1} Device: {2}", "", ip, "");//, ((uniqueDevice is null) ? "" : uniqueDevice));

                if(cliente.sistema != 1 && cliente.sistema != 2 && cliente.sistema != 3 && cliente.sistema != 4)
                    throw new Exception("ERROR AL DETERMINAR SISTEMA ...");

                dynamic respuesta = new ExpandoObject();
                /*Entities.JsonResult*/respuesta = await _registroService.RegistraCliente(cliente);

                if(respuesta.error == 1)
                    throw new Exception(respuesta.result);

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
    }
}