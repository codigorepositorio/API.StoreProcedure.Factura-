using CANVIA.RETO.Factura.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IItemRepository
    {
        Task<ItemDetalle> Create(ItemDetalle cliente);
        Task<IEnumerable<ItemDetalle>> GetAll();
        Task<ItemDetalle> GetById(int ItemDetalleID);
        void Update(ItemDetalle cliente);
        void Delete(int ItemDetalleID);
    }
}
