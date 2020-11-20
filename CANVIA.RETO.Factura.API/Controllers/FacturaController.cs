using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CANVIA.RETO.Factura.API.Controllers
{
    [Route("api/Factura")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly FacturaService _facturaService;

        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFactura()
        {
            var result = await _facturaService.GetAll();
            if (result.Count() == 0)
                return BadRequest("No existe registro de Facturas.");

            return Ok(result);
        }



        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetAllFacturaById(int Id)
        {
            var result = await _facturaService.GetById(Id);
            //if (result.Count() == 0)
            //    return BadRequest("No existe registro de Facturas.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFactura([FromBody] FacturaForCreatetion facturaForCreatetion)
        {
            try
            {
                if (facturaForCreatetion.codigoCliente == 0)
                    return BadRequest("No puede enviar un cliente con còdigo 0.");
                var result = await _facturaService.Create(facturaForCreatetion);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }

        }
    }
}
