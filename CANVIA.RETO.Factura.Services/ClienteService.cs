using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CANVIA.RETO.Factura.Services
{
    public class ClienteService : ConexionGeneral
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
            ClienteDto cliente = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _clienteRepository.GetById(clienteID, con);
                cliente = _mapper.Map<ClienteDto>(result);
            }
            return cliente;
        }

        public async Task<IEnumerable<ClienteDto>> GetAll()
        {
            IEnumerable<ClienteDto> lstCliente = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _clienteRepository.GetAll(con);
                lstCliente = _mapper.Map<IEnumerable<ClienteDto>>(result);
            }
            return lstCliente;
        }

        public async Task<ClienteForCreationDto> Create(ClienteForCreationDto clienteForCreationDto)
        {
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {                
                await con.OpenAsync();                
                var clienteEntity = _mapper.Map<Cliente>(clienteForCreationDto);
                var clienteIdReturn = await _clienteRepository.Create(clienteEntity, con);
                 clienteForCreationDto.codigoCliente = clienteIdReturn;
            }
            return clienteForCreationDto;
        }

        public bool Update(ClienteForUpdateDto clienteForUpdateDto)
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                var clienteEntity = _mapper.Map<Cliente>(clienteForUpdateDto);
                exito = _clienteRepository.Update(clienteEntity, con);
            }
            return exito;
        }

        public bool Delete(int clienteID)
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                exito = _clienteRepository.Delete(clienteID, con);
            }
            return exito;
        }
    }
}
