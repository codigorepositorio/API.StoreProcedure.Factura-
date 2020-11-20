using CANVIA.RETO.Factura.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> Create(Cliente cliente);
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente> GetById(int clienteID);
        void Update(Cliente cliente);
        void Delete(int clienteID);
    }
}
