using System.Threading.Tasks;
using CANVIA.RETO.Factura.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

using System;
using CANVIA.RETO.Item.DTOs;

namespace CANVIA.RETO.Factura.API.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : ControllerBase
    {
  
        private readonly ItemService _itemService;

        public ItemController(ItemService itemService )
        {
            
            _itemService = itemService;
        }

        [HttpGet("{codigo:int}", Name = "itemCreate")]
        public async Task<IActionResult> GetByIdItem(int codigo)
        {
            var result = await _itemService.GetById(codigo);
            if (result.codigoITem == 0 && result.producto == null)
                return BadRequest("El Item no existe.");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllItem()
        {
            var result = await _itemService.GetAll();
            if (result.Count() == 0)
                return BadRequest("No existe registro de Items.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateItem([FromBody] ItemDetalleForCreationDto  itemDetalleForCreationDto)
        {
            try
            {

                var result = await _itemService.Create(itemDetalleForCreationDto);
                return CreatedAtRoute("itemCreate", new { codigo = result.codigoITem }, result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message.ToString());
            }

        }

        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] ItemDetalleForUpdateDto itemDetalleForUpdateDto )
        {
            try
            {
                if (itemDetalleForUpdateDto.descripcion == null)
                    return BadRequest("No puede enviar una descripcion vacia.");

                var result = await _itemService.Update(itemDetalleForUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<IActionResult> DeleteItem(int codigo)
        {
            try
            {
                var result = await _itemService.Delete(codigo);
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
