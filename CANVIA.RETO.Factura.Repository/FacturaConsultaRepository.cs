using CANVIA.RETO.Factura.Entities.DTOs;
using CANVIA.RETO.Item.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CANVIA.RETO.Factura.Repository
{
    public class FacturaConsultaRepository : IFacturaConsultaRepository
    {

        private readonly string conn;


        public FacturaConsultaRepository(IConfiguration configuration)
        {
            conn = configuration.GetConnectionString("SqlServer");
        }
        public async Task<IEnumerable<FacturaCabeceraDto>> GetAll()
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Factura_GetAll", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var facturaCabeceraDto = new List<FacturaCabeceraDto>();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            facturaCabeceraDto.Add(facturaCabeceraReader(reader));
                        }
                    }

                    return facturaCabeceraDto;
                }
            }
        }



        public async Task<FacturaCabeceraDto> GetById(int facturaCabeceraID)
        {
            using (SqlConnection sqlConn = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_Factura_GetbyIdFactura", sqlConn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@facturaCabeceraID", facturaCabeceraID));
                    var facturaCabeceraDto = new FacturaCabeceraDto();
                    var lstItemDetalleDto = new List<ItemDetalleConsultaDto>();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            facturaCabeceraDto = facturaCabeceraReader(reader);
                        }

                        await reader.NextResultAsync();

                        while (await reader.ReadAsync())
                        {
                            lstItemDetalleDto.Add(ItemDetalleDtoReader(reader));
                        }
                        facturaCabeceraDto.itemDetalles = lstItemDetalleDto;
                    }

                    return facturaCabeceraDto;
                }
            }
        }



        private FacturaCabeceraDto facturaCabeceraReader(SqlDataReader reader)
        {
            int posFacturaCabecera = reader.GetOrdinal("FacturaCabeceraID");
            int posClienteID = reader.GetOrdinal("ClienteID");
            int posNumeroFactura = reader.GetOrdinal("NumeroFactura");
            int posImporteTotal = reader.GetOrdinal("ImporteTotal");
            int posTipoPersona = reader.GetOrdinal("TipoPersona");
            int posFechaHora = reader.GetOrdinal("FechaHora");
            return new FacturaCabeceraDto()
            {
                codigoFactura = reader.GetInt32(posFacturaCabecera),
                codigoCliente = reader.GetInt32(posClienteID),
                NumeroFactura = reader.GetString(posNumeroFactura),
                ImporteTotal = reader.GetDecimal(posImporteTotal),
                tipo = reader.GetString(posTipoPersona),
                Fecha = reader.GetDateTime(posFechaHora),
            };
        }


        private ItemDetalleConsultaDto ItemDetalleDtoReader(SqlDataReader reader)
        {
            int posFacturaCabecera = reader.GetOrdinal("FacturaCabeceraID");
            int posFacturaDetalle = reader.GetOrdinal("FacturaDetalleID");
            int posItemDetalleID = reader.GetOrdinal("ItemDetalleID");
            int posProducto = reader.GetOrdinal("Producto");
            int postPrecio = reader.GetOrdinal("Precio");
            int postCantidad = reader.GetOrdinal("Cantidad");
            int postsubTotal = reader.GetOrdinal("Subtotal");

            return new ItemDetalleConsultaDto()
            {
                codigoFactura = reader.GetInt32(posFacturaCabecera),
                codigoDetalle = reader.GetInt32(posFacturaDetalle),
                codigoItem = reader.GetInt32(posItemDetalleID),
                descripcion = reader.GetString(posProducto),
                precio = reader.GetDecimal(postPrecio),
                cantidad = reader.GetInt32(postCantidad),
                subTotal = reader.GetDecimal(postsubTotal)
            };

        }
    }
}
