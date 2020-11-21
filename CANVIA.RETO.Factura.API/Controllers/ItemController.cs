using System.Threading.Tasks;
using CANVIA.RETO.Factura.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using System;
using CANVIA.RETO.Item.DTOs;
using LoggerServices;

namespace CANVIA.RETO.Factura.API.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly ItemService _itemService;
        private readonly ILoggerManager _logger;
        public ItemController(ItemService itemService, ILoggerManager logger)
        {
            _itemService = itemService;
            _logger = logger;
        }

        [HttpGet("{codigo:int}", Name = "itemCreate")]
        public async Task<IActionResult> GetByIdItem(int codigo)
        {
            var result = await _itemService.GetById(codigo);

            if (result.codigoItem == 0)
            {
                _logger.LogInfo($"Item con id: {codigo} no existe en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItem()
        {
            var result = await _itemService.GetAll();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de Items en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDetalleForCreationDto itemDetalleForCreationDto)
        {
            if (itemDetalleForCreationDto == null)
            {
                _logger.LogError("El objeto itemDetalleForCreationDto enviado desde el cliente es nulo.");
                return BadRequest("No puede enviar un Item nulo.");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Estado de modelo no válido para el objeto itemDetalleForCreationDto");
                return UnprocessableEntity(ModelState);
            }

            var result = await _itemService.Create(itemDetalleForCreationDto);
            return CreatedAtRoute("itemCreate", new { codigo = result.codigoItem }, result);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDetalleForUpdateDto itemDetalleForUpdateDto)
        {

            if (itemDetalleForUpdateDto == null)
            {
                _logger.LogError("El objeto itemDetalleForUpdateDto enviado desde el cliente es nulo.");
                return BadRequest("No puede enviar un Item nulo.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the itemDetalleForUpdateDto object");
                return BadRequest(ModelState);
            }

            var result = await _itemService.GetById(itemDetalleForUpdateDto.codigoItem);

            if (result.codigoItem == 0)
            {
                _logger.LogInfo($"Item con id: {itemDetalleForUpdateDto.codigoItem} no existe en la base de datos");
                return NotFound();
            }

            _itemService.Update(itemDetalleForUpdateDto);
            return NoContent();
        }

        [HttpDelete("{codigo:int}")]
        public async Task<IActionResult> DeleteItem(int codigo)
        {

            var result = await _itemService.GetById(codigo);

            if (result.codigoItem == 0)
            {
                _logger.LogInfo($"Cliente con id: {codigo} no existe en la base de datos");
                return NotFound();
            }
            _itemService.Delete(codigo);
            return NoContent();
        }
    }
}
