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
                    var lstItemDetalleDto = new List<ItemDetalleDto>();
                    await sqlConn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            facturaCabeceraDto = facturaCabeceraReader(reader);
                        }

                        while (await reader.NextResultAsync())
                        {
                            lstItemDetalleDto.Add(ItemDetalleDtoReader(reader));
                        }
                    }
                    return facturaCabeceraDto;
                }
            }
        }



        private FacturaCabeceraDto facturaCabeceraReader(SqlDataReader reader)
        {
            return new FacturaCabeceraDto()
            {
                FacturaCabeceraID = reader.GetInt32(0),
                ClienteID = reader.GetInt32(1),
                NumeroFactura = reader.GetString(2),
                ImporteTotal = reader.GetDecimal(3),
                tipo = reader.GetString(4),
                Fecha = reader.GetDateTime(5)
                };
        }


        private ItemDetalleDto ItemDetalleDtoReader(SqlDataReader reader)
        {
            return new ItemDetalleDto()
            {
                codigoFactura = reader.GetInt32(0),
                codigoDetalle = reader.GetInt32(1),
                codigoITem = reader.GetInt32(2),
                producto = reader.GetString(3),
                precio = reader.GetDecimal(4),
                cantidad = reader.GetInt32(5),
                subTotal = reader.GetDecimal(6),

            };
        }


        //public int codigoFactura { get; set; }
        //public int codigoDetalle { get; set; }
        //public int codigoITem { get; set; }
        //public string producto { get; set; }
        //public decimal precio { get; set; }
        //public int cantidad { get; set; }
        //public decimal subTotal { get; set; }
    }
}
