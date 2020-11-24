using CANVIA.RETO.Factura.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IClienteRepository
    {
        Task<Cliente> Create(Cliente cliente);
        Task<IEnumerable<Cliente>> GetAll();
       Task<IEnumerable<Cliente>> Listar(SqlConnection con);
        Task<Cliente> GetById(int clienteID);
        void Update(Cliente cliente);
        void Delete(int clienteID);
    }
}
