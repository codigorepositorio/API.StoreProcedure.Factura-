using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Factura.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Services
{
    public class FacturaService
    {
        private readonly IFacturaRepository _facturaCabeceraRepository;
        private readonly IFacturaConsultaRepository _facturaConsultaRepository;
        private readonly IMapper _mapper;

        public FacturaService(IFacturaRepository facturaCabeceraRepository,
                                IFacturaConsultaRepository facturaConsultaRepository
                                ,IMapper mapper)
        {
            _facturaCabeceraRepository = facturaCabeceraRepository;
            _facturaConsultaRepository = facturaConsultaRepository;
            _mapper = mapper;
        }

        public async Task<FacturaCabecera> Create(FacturaForCreatetion facturaForCreatetion)
        {
            var clienteEntity = _mapper.Map<FacturaCabecera>(facturaForCreatetion);

            var clienteReturn = await _facturaCabeceraRepository.Create(clienteEntity);            

            return clienteReturn;

        }


        public async Task<IEnumerable<FacturaCabeceraDto>> GetAll()
        {

            var lstfacturaReturn = await _facturaConsultaRepository.GetAll();

            return lstfacturaReturn;

        }
        public async Task<FacturaCabeceraDto> GetById(int clienteID)
        {
            var ObjfacturaReturn = await _facturaConsultaRepository.GetById(clienteID);
           // var result = _mapper.Map<FacturaCabeceraDto>(ObjfacturaReturn);
            return ObjfacturaReturn;
        }


    }
}
