using CANVIA.RETO.Factura.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CANVIA.RETO.Factura.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly string conn;
        public ClienteRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("SqlServer");
        }

        public async Task<Cliente> Create(Cliente cliente)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Cliente_Create", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
                        Direction = System.Data.ParameterDirection.Output
                    });
                    await sqlConn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int id = (int)cmd.Parameters["@pclienteID"].Value;

                    await sqlConn.CloseAsync();

                    cliente.clienteID = id;
                    return cliente;
                }

            }

        }

        public void Update(Cliente cliente)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Cliente_Update", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                    cmd.Parameters.Add(new SqlParameter("@pClienteID", cliente.clienteID));
                    cmd.Parameters.Add(new SqlParameter("@pTipoPersona", cliente.TipoPersona));
                    cmd.Parameters.Add(new SqlParameter("@pNombres", cliente.Nombres));
                    cmd.Parameters.Add(new SqlParameter("@pApellidos", cliente.Apellidos));
                    cmd.Parameters.Add(new SqlParameter("@pTipoDocumento", cliente.TipoDocumento));
                    cmd.Parameters.Add(new SqlParameter("@pNumDocumento", cliente.NumDocumento));
                    cmd.Parameters.Add(new SqlParameter("@pDireccion", cliente.Direccion));
                    cmd.Parameters.Add(new SqlParameter("@pTelefono", cliente.Telefono));
                    cmd.Parameters.Add(new SqlParameter("@pEmail", cliente.Email));
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public void Delete(int clienteID)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Cliente_Delete", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pclienteID", clienteID));                    
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        /// <summary>
        /// Consulta: Cliente y Cliente por Id
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Cliente>> GetAll()
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Cliente_GetAll", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var cliente = new List<Cliente>();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cliente.Add(clienteReader(reader));
                        }
                    }

                    return cliente;
                }
            }
        }
        public async Task<Cliente> GetById(int clienteID)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Cliente_GetById", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClienteID", clienteID));
                    var cliente = new Cliente();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cliente = clienteReader(reader);
                        }
                    }

                    return cliente;
                }
            }
        }


        private Cliente clienteReader(SqlDataReader reader)
        {
            return new Cliente()
            {
                clienteID = (int)reader["ClienteID"],
                TipoPersona = (string)reader["TipoPersona"],                 
                Nombres = (string)reader["Nombres"],
                Apellidos = (string)reader["Apellidos"],
                TipoDocumento = (string)reader["TipoDocumento"],
                NumDocumento = (string)reader["NumDocumento"],
                Direccion = (string)reader["Direccion"],
                Telefono = (string)reader["Telefono"],
                Email = (string)reader["Email"],
            };
        }




    }
}
