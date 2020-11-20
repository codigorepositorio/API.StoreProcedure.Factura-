using CANVIA.RETO.Factura.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace CANVIA.RETO.Factura.Repository
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly string conn;


        public FacturaRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("SqlServer");
        }

        public async Task<FacturaCabecera> Create(FacturaCabecera facturaCabecera)
        {
            List<FacturaDetalle> lstFacturaDetalle = new List<FacturaDetalle>();
            int id = 0;
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                try
                {
                    if (facturaCabecera.FacturaCabeceraID == 0)
                    {
                        using (SqlCommand cmd = new SqlCommand("Usp_FacturaCabecera_Create", sqlConn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@pclienteID", facturaCabecera.ClienteID));
                            cmd.Parameters.Add(new SqlParameter("@pNumeroFactura", facturaCabecera.NumeroFactura));
                            cmd.Parameters.Add(new SqlParameter("@pImporteTotal", facturaCabecera.ImporteTotal));
                            cmd.Parameters.Add(new SqlParameter
                            {
                                ParameterName = "@pFacturaCabeceraID",
                                Value = facturaCabecera.FacturaCabeceraID,
                                Direction = System.Data.ParameterDirection.Output
                            });
                            await sqlConn.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                            id = (int)cmd.Parameters["@pFacturaCabeceraID"].Value;
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("Usp_FacturaCabecera_Update", sqlConn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@pFacturaCabeceraID", facturaCabecera.FacturaCabeceraID));
                            cmd.Parameters.Add(new SqlParameter("@pclienteID", facturaCabecera.ClienteID));
                            cmd.Parameters.Add(new SqlParameter("@pImporteTotal", facturaCabecera.ImporteTotal));
                            await sqlConn.OpenAsync();
                            await cmd.ExecuteNonQueryAsync();
                        }
                    }

                    foreach (var item in facturaCabecera.FacturaDetalle)
                    {
                        if (item.FacturaDetalleID == 0 && item.FacturaCabeceraID == 0)
                        {
                            item.FacturaCabeceraID = id;
                            var result = await CreateDetalle(item);
                             lstFacturaDetalle.Add(result);                            
                        }
                        if (item.FacturaDetalleID == 0 && item.FacturaCabeceraID > 0)
                        {
                            item.FacturaCabeceraID = item.FacturaCabeceraID;
                            var result = await CreateDetalle(item);
                            lstFacturaDetalle.Add(result);
                        }

                        else
                        {

                            await UpdateDetalle(item);
                        }
                    }

                    facturaCabecera.FacturaCabeceraID = id;
                    facturaCabecera.FacturaDetalle = lstFacturaDetalle;
                    return facturaCabecera;

                }
                catch (System.Exception)
                {

                    throw;
                }

            }
        }


        private async Task<FacturaDetalle> CreateDetalle(FacturaDetalle facturaDetalle)
        {

            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                await sqlConn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("Usp_FacturaDetalle_Create", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pFacturaCabeceraID", facturaDetalle.FacturaCabeceraID));
                    cmd.Parameters.Add(new SqlParameter("@pItemDetalleID", facturaDetalle.ItemDetalleID));
                    cmd.Parameters.Add(new SqlParameter("@pProducto", facturaDetalle.Producto));
                    cmd.Parameters.Add(new SqlParameter("@pPrecio", facturaDetalle.Precio));
                    cmd.Parameters.Add(new SqlParameter("@pCantidad", facturaDetalle.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@pFacturaDetalleID", facturaDetalle.FacturaDetalleID));
                    await cmd.ExecuteNonQueryAsync();
                }
                sqlConn.Close();
                return facturaDetalle;
            }
        }



        private async Task<FacturaDetalle> UpdateDetalle(FacturaDetalle facturaDetalle)
        {

            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                await sqlConn.OpenAsync();
                using (SqlCommand cmd = new SqlCommand("Usp_FacturaDetalle_Update", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pFacturaCabeceraID", facturaDetalle.FacturaCabeceraID));
                    cmd.Parameters.Add(new SqlParameter("@pItemDetalleID", facturaDetalle.ItemDetalleID));
                    cmd.Parameters.Add(new SqlParameter("@pProducto", facturaDetalle.Producto));
                    cmd.Parameters.Add(new SqlParameter("@pPrecio", facturaDetalle.Precio));
                    cmd.Parameters.Add(new SqlParameter("@pCantidad", facturaDetalle.Cantidad));
                    cmd.Parameters.Add(new SqlParameter("@pFacturaDetalleID", facturaDetalle.FacturaDetalleID));
                    await cmd.ExecuteNonQueryAsync();
                }
                sqlConn.Close();
                return facturaDetalle;
            }
        }
    }
}
