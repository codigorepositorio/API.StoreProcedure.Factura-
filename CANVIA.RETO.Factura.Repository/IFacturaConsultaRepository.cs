using CANVIA.RETO.Factura.Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IFacturaConsultaRepository
    {
        Task<IEnumerable<FacturaCabeceraDto>> GetAll();

        Task<FacturaCabeceraDto> GetById( int facturaCabeceraID);
    }
}
