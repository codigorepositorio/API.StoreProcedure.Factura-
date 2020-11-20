using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;

namespace CANVIA.RETO.Factura.API.AutoMapper
{
    public class MappingProfileFactura : Profile
    {
        public MappingProfileFactura()
        {

            //POST: FacturaForCreatetion y FacturaCabecera 
            // A     =====>     //B  
            CreateMap<FacturaForCreatetion, FacturaCabecera>()
                .ForMember(b => b.FacturaCabeceraID, opt => opt.MapFrom(a => a.codigoFactura))
                .ForMember(b => b.ClienteID, opt => opt.MapFrom(a => a.codigoCliente))
                .ForMember(b => b.NumeroFactura, opt => opt.MapFrom(a => a.numeroFactura))
                .ForMember(b => b.ImporteTotal, opt => opt.MapFrom(a => a.TotalPago));


            //POST: FacturaDetalleForCreatetion y FacturaDetalle 
            // A     =====>     //B  
            CreateMap<FacturaDetalleForCreatetion, FacturaDetalle>()
                .ForMember(b => b.FacturaDetalleID, opt => opt.MapFrom(a => a.codigoDetalle))
                .ForMember(b => b.FacturaCabeceraID, opt => opt.MapFrom(a => a.codigoFactura))
                .ForMember(b => b.ItemDetalleID, opt => opt.MapFrom(a => a.codigoItem))
                .ForMember(b => b.Producto, opt => opt.MapFrom(a => a.item))
                .ForMember(b => b.Precio, opt => opt.MapFrom(a => a.precio))
                .ForMember(b => b.Cantidad, opt => opt.MapFrom(a => a.cantidad));
        }
    }
}
