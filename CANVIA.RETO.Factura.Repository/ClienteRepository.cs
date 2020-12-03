using CANVIA.RETO.Factura.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
namespace CANVIA.RETO.Factura.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        public async Task<int> Create(Cliente cliente, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Usp_Cliente_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlTransaction transaction;
            transaction = con.BeginTransaction();
            cmd.Transaction = transaction;
            cmd.Parameters.Add(new SqlParameter("@pTipoPersona", cliente.TipoPersona));
            cmd.Parameters.Add(new SqlParameter("@pNombres", cliente.Nombres));
            cmd.Parameters.Add(new SqlParameter("@pApellidos", cliente.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@pTipoDocumento", cliente.TipoDocumento));
            cmd.Parameters.Add(new SqlParameter("@pNumDocumento", cliente.NumDocumento));
            cmd.Parameters.Add(new SqlParameter("@pDireccion", cliente.Direccion));
            cmd.Parameters.Add(new SqlParameter("@pTelefono", cliente.Telefono));
            cmd.Parameters.Add(new SqlParameter("@pEmail", cliente.Email));
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@pclienteID",
                Value = cliente.clienteID,
                Direction = ParameterDirection.Output
            });

            try
            {
                await cmd.ExecuteNonQueryAsync();
                int id = (int)cmd.Parameters["@pclienteID"].Value;
                transaction.Commit();
                cliente.clienteID = id;
            }
            catch (System.Exception)
            {
                transaction.Rollback();
            }
            return cliente.clienteID;
        }
        public bool Update(Cliente cliente, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Cliente_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pClienteID", cliente.clienteID));
            cmd.Parameters.Add(new SqlParameter("@pTipoPersona", cliente.TipoPersona));
            cmd.Parameters.Add(new SqlParameter("@pNombres", cliente.Nombres));
            cmd.Parameters.Add(new SqlParameter("@pApellidos", cliente.Apellidos));
            cmd.Parameters.Add(new SqlParameter("@pTipoDocumento", cliente.TipoDocumento));
            cmd.Parameters.Add(new SqlParameter("@pNumDocumento", cliente.NumDocumento));
            cmd.Parameters.Add(new SqlParameter("@pDireccion", cliente.Direccion));
            cmd.Parameters.Add(new SqlParameter("@pTelefono", cliente.Telefono));
            cmd.Parameters.Add(new SqlParameter("@pEmail", cliente.Email));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;
        }
        public bool Delete(int clienteID, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Cliente_Delete", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pclienteID", clienteID));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;

        }

        /// <summary>
        /// Consulta: Cliente y Cliente por Id
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        public async Task<Cliente> GetById(int clienteID, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand("Usp_Cliente_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@clienteID", clienteID));
            var cliente = new Cliente();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.

            if (reader != null)
            {
                while (await reader.ReadAsync())
                {
                    cliente = clienteReader(reader);
                }
            }
            return cliente;
        }
        public async Task<IEnumerable<Cliente>> GetAll(SqlConnection con)
        {
            List<Cliente> lstCliente = null;
            SqlCommand cmd = new SqlCommand("Usp_Cliente_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;            
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.
            if (reader != null)
            {
                lstCliente = new List<Cliente>();
                while (await reader.ReadAsync())
                {
                    lstCliente.Add(clienteReader(reader));
                }
            }
            return lstCliente;
        }
        private Cliente clienteReader(SqlDataReader reader)
        {
            int posclienteID = reader.GetOrdinal("clienteID");
            int posTipoPersona = reader.GetOrdinal("TipoPersona");
            int posNombres = reader.GetOrdinal("Nombres");
            int posApellidos = reader.GetOrdinal("Apellidos");
            int posTipoDocumento = reader.GetOrdinal("TipoDocumento");
            int posNumDocumento = reader.GetOrdinal("NumDocumento");
            int posDireccion = reader.GetOrdinal("Direccion");
            int posTelefono = reader.GetOrdinal("Telefono");
            int posEmail = reader.GetOrdinal("Email");
            return new Cliente()
            {
                clienteID = reader.GetInt32(posclienteID),
                TipoPersona = reader.GetString(posTipoPersona),
                Nombres = reader.GetString(posNombres),
                Apellidos = reader.GetString(posApellidos),
                TipoDocumento = reader.GetString(posTipoDocumento),
                NumDocumento = reader.GetString(posNumDocumento),
                Direccion = reader.GetString(posDireccion),
                Telefono = reader.GetString(posTelefono),
                Email = reader.GetString(posEmail)
            };
        }
    }
}
