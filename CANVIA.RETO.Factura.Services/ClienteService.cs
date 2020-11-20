using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace CANVIA.RETO.Factura.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }


        public async Task<ClienteDto> GetById(int clienteID)
        {
            var result = await _clienteRepository.GetById(clienteID);
            var cliente = _mapper.Map<ClienteDto>(result);
            return cliente;
        }


        public async Task<IEnumerable<ClienteDto>> GetAll()
        {
            var result = await _clienteRepository.GetAll();
            var resultCliente = _mapper.Map<IEnumerable<ClienteDto>>(result);
            return resultCliente;
        }

        public async Task<ClienteDto> Create(ClienteForCreationDto clienteForCreationDto)
        {
            var clienteEntity = _mapper.Map<Cliente>(clienteForCreationDto);

            var clienteReturn = await _clienteRepository.Create(clienteEntity);

            var cliente = _mapper.Map<ClienteDto>(clienteReturn);

            return cliente;

        }

        public async Task<string> Update(ClienteForUpdateDto clienteForUpdateDto)
        {
            var serchCliente = await _clienteRepository.GetAll();
            var serchClienteById = serchCliente.Where(c => c.clienteID.Equals(clienteForUpdateDto.Codigo));

            if (serchClienteById.Count() == 0)
            {
                return "El Cliente con Còdigo : " + clienteForUpdateDto.Codigo + "No existe";
            }

            var result = await _clienteRepository.GetById(clienteForUpdateDto.Codigo);

            if (result.clienteID == 0)
            {
                return "El Cliente con Còdigo : " + result.clienteID + "No existe";
            }

            var clienteEntity = _mapper.Map<Cliente>(clienteForUpdateDto);

            _clienteRepository.Update(clienteEntity);

            return "El Cliente con Còdigo : " + result.clienteID + "Modificado";

        }

        public async Task<string> Delete(int clienteID)
        {
            var result = await _clienteRepository.GetById(clienteID);
            if (result.clienteID == 0)
            {
                return "El Cliente con Còdigo : " + result.clienteID + " No existe.";
            }

            _clienteRepository.Delete(result.clienteID);

            return "El Cliente con Còdigo : " + result.clienteID + " Eliminado.";
        }

    }
}
