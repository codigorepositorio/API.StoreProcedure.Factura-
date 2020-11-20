using CANVIA.RETO.Factura.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IFacturaRepository
    {
        Task<FacturaCabecera> Create(FacturaCabecera facturaCabecera);        
    }
}
