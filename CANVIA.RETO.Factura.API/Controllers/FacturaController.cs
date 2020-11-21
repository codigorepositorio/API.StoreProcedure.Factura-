using System.Linq;
using System.Threading.Tasks;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Services;
using LoggerServices;
using Microsoft.AspNetCore.Mvc;

namespace CANVIA.RETO.Factura.API.Controllers
{
    [Route("api/Factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _facturaService;
        private readonly ILoggerManager _logger;

        public FacturaController(FacturaService facturaService, ILoggerManager logger)
        {
            _facturaService = facturaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFactura()
        {
            var result = await _facturaService.GetAll();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de Factura en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }



        [HttpGet("{Id:codigo}", Name = "facturaCreate")]
        public async Task<IActionResult> GetAllFacturaById(int codigo)
        {
            var result = await _facturaService.GetById(codigo);

            if (result.codigoFactura == 0)
            {
                _logger.LogInfo($"Factura con id: {codigo} no existe en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }


    [HttpPost]
    public async Task<IActionResult> CreateFactura([FromBody] FacturaForCreatetion facturaForCreatetion)
    {

        if (facturaForCreatetion == null)
        {
            _logger.LogError("El objeto facturaForCreatetion enviado desde el cliente es nulo.");
            return BadRequest("No puede enviar una Factura nulo.");
        }
        if (!ModelState.IsValid)
        {
            _logger.LogError("Estado de modelo no válido para el objeto facturaForCreatetion");
            return UnprocessableEntity(ModelState);
        }

        var result = await _facturaService.Create(facturaForCreatetion);
        return CreatedAtRoute("facturaCreate", new { codigo = result.FacturaCabeceraID }, result);

    }
}
}
