using CANVIA.RETO.Factura.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public interface IClienteRepository
    {
        Task<int> Create(Cliente cliente, SqlConnection con);
        Task<IEnumerable<Cliente>> GetAll(SqlConnection con);        
        Task<Cliente> GetById(int clienteID, SqlConnection con);
        bool Update(Cliente cliente, SqlConnection con);
        bool Delete(int clienteID, SqlConnection con);
    }
}
