using CANVIA.RETO.Factura.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CANVIA.RETO.Factura.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly string conn;
        public ItemRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("SqlServer");
        }

        public async Task<ItemDetalle> Create(ItemDetalle itemDetalle)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ItemDetalle_Create", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@pProducto", itemDetalle.Producto));
                    cmd.Parameters.Add(new SqlParameter("@pPrecio", itemDetalle.Precio));
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@pItemDetalleID",
                        Value = itemDetalle.ItemDetalleID,
                        Direction = System.Data.ParameterDirection.Output
                    });
                    await sqlConn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                    int id = (int)cmd.Parameters["@pItemDetalleID"].Value;

                    await sqlConn.CloseAsync();

                    itemDetalle.ItemDetalleID = id;
                    return itemDetalle;
                }

            }

        }

        public void Update(ItemDetalle itemDetalle)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ItemDetalle_Update", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pItemDetalleID", itemDetalle.ItemDetalleID));
                    cmd.Parameters.Add(new SqlParameter("@pProducto", itemDetalle.Producto));
                    cmd.Parameters.Add(new SqlParameter("@pPrecio", itemDetalle.Precio));        
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public void Delete(int itemDetalleID)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ItemDetalle_Delete", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pItemDetalleID", itemDetalleID));
                    sqlConn.Open();
                    cmd.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        /// <summary>
        /// Consulta: ItemDetalle y ItemDetalle por Id
        /// </summary>
        /// <param name="clienteID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemDetalle>> GetAll()
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ItemDetalle_GetAll", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var lstItemDetalle = new List<ItemDetalle>();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            lstItemDetalle.Add(itemDetalleReader(reader));
                        }
                    }

                    return lstItemDetalle;
                }
            }
        }
        public async Task<ItemDetalle> GetById(int itemDetalleID)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_ItemDetalle_GetById", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pItemDetalleID", itemDetalleID));
                    var itemDetalle = new ItemDetalle();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            itemDetalle = itemDetalleReader(reader);
                        }
                    }
                    return itemDetalle;
                }
            }
        }


        private ItemDetalle itemDetalleReader(SqlDataReader reader)
        {
            return new ItemDetalle()
            {
                ItemDetalleID = (int)reader["ItemDetalleID"],
                Producto = (string)reader["Producto"],
                Precio = (decimal)reader["Precio"],
            };
        }
    }
}
