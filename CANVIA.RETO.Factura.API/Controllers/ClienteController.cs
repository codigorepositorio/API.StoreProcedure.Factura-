using System.Threading.Tasks;
using CANVIA.RETO.Factura.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CANVIA.RETO.Factura.Entities.DTOs;
using LoggerServices;

namespace CANVIA.RETO.Factura.API.Controllers
{

    [ApiController]
    [Route("api/cliente")]  
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _clienteService;
        private readonly ILoggerManager _logger;
        private readonly brCliente _brCliente;

        public ClienteController(ClienteService clienteService, ILoggerManager logger, brCliente brCliente)
        {
            _clienteService = clienteService;
            _logger = logger;
            _brCliente = brCliente;
        }



        [HttpGet("{id}", Name = "clienteCreate")]
        public async Task<IActionResult> GetByIdCliente(int id)
        {
            var result = await _clienteService.GetById(id);

            if (result.codigoCliente == 0)
            {
                _logger.LogInfo($"Cliente con id: {id} no existe en la base de datos");
                return NotFound();
            }
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCliente()
        {
            var result = await _clienteService.GetAll();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de clientes en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllClienteAdo()
        {
            var result = await _brCliente.Listar();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de clientes en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCliente([FromBody] ClienteForCreationDto clienteForCreationDto)
        {
            if (clienteForCreationDto == null)
            {
                _logger.LogError("El objeto clienteForCreationDto enviado desde el cliente es nulo.");
                return BadRequest("No puede enviar un cliente nulo.");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Estado de modelo no válido para el objeto EmployeeForCreationDto");
                return UnprocessableEntity(ModelState);
            }
            var result = await _clienteService.Create(clienteForCreationDto);
            return CreatedAtRoute("clienteCreate", new { id = result.codigoCliente }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteForUpdateDto clienteForUpdateDto)
        {
            if (clienteForUpdateDto == null)
            {
                _logger.LogError("Estado de modelo no válido para el objeto clienteForUpdateDto");
                return BadRequest("No puede enviar un cliente nulo.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the clienteForUpdateDto object");
                return BadRequest(ModelState);
            }

            var result = await _clienteService.GetById(clienteForUpdateDto.codigoCliente);

            if (result.codigoCliente == 0)
            {
                _logger.LogInfo($"Cliente con id: {clienteForUpdateDto.codigoCliente} no existe en la base de datos");
                return NotFound();
            }

            _clienteService.Update(clienteForUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var result = await _clienteService.GetById(id);

            if (result.codigoCliente == 0)
            {
                _logger.LogInfo($"Cliente con id: {id} no existe en la base de datos");
                return NotFound();
            }
            _clienteService.Delete(result.codigoCliente);
            return NoContent();
        }
    }
}
