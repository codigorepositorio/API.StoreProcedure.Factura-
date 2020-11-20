using System.Threading.Tasks;
using CANVIA.RETO.Factura.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CANVIA.RETO.Factura.Entities.DTOs;
using System;

namespace CANVIA.RETO.Factura.API.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClienteController(ClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{codigo:int}", Name = "clienteCreate")]
        public async Task<IActionResult> GetByIdCliente(int codigo)
        {
            var result = await _clienteService.GetById(codigo);
            if (result.Codigo == 0 && result.nombres == null)
                return BadRequest("El cliente no existe.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCliente()
        {
            var result = await _clienteService.GetAll();
            if (result.Count() == 0)
                return BadRequest("No existe registro de clientes.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteForCreationDto clienteForCreationDto)
        {
            try
            {

                var result = await _clienteService.Create(clienteForCreationDto);
                return CreatedAtRoute("clienteCreate", new { codigo = result.Codigo }, result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteForUpdateDto clienteForUpdateDto)
        {
            try
            {
                if (clienteForUpdateDto.nombres == null)
                    return BadRequest("No puede enviar un cliente nulo.");

                var result = await _clienteService.Update(clienteForUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<IActionResult> DeleteCliente(int codigo)
        {
            try
            {
                var result = await _clienteService.Delete(codigo);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
