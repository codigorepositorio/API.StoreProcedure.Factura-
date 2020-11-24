using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Repository;

namespace CANVIA.RETO.Factura.Services
{
    public class brCliente : ConexionGeneral
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public brCliente(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ClienteDto>> Listar()
        {
            IEnumerable<ClienteDto> lstCliente = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    await con.OpenAsync();
                    var result = await _clienteRepository.Listar(con);
                     lstCliente = _mapper.Map<IEnumerable<ClienteDto>>(result);
                }
                catch (Exception ex)
                {
                    LogCentralizado(ex);
                }
            }
            return lstCliente;
        }

    }
}
