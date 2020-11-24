using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Item.DTOs;

namespace CANVIA.RETO.Factura.API.AutoMapper
{
    public class MappingProfileItem : Profile
    {
        public MappingProfileItem()
        {
            //GET: ItemDetalle y ItemDetalleDto 
            // A     =====>     //B  
            CreateMap<ItemDetalle, ItemDetalleDto>()
                .ForMember(b => b.codigoItem, opt => opt.MapFrom(a => a.ItemDetalleID))
                .ForMember(b => b.descripcion, opt => opt.MapFrom(a => a.Producto))
                .ForMember(b => b.precio, opt => opt.MapFrom(a => a.Precio));

            //POST:  ItemDetalleForCreationDto y ItemDetalle
            // A     =====>     //B  
            CreateMap<ItemDetalleForCreationDto, ItemDetalle>()
                .ForMember(b => b.ItemDetalleID, opt => opt.MapFrom(a => a.codigoItem))
                .ForMember(b => b.Producto, opt => opt.MapFrom(a => a.descripcion))
                .ForMember(b => b.Precio, opt => opt.MapFrom(a => a.precio));


            //PUT: ItemDetalleForUpdateDto y ItemDetalle
            // A     =====>     //B  
            CreateMap<ItemDetalleForUpdateDto, ItemDetalle>()
                .ForMember(b => b.ItemDetalleID, opt => opt.MapFrom(a => a.codigoItem))
                .ForMember(b => b.Producto, opt => opt.MapFrom(a => a.descripcion))
                .ForMember(b => b.Precio, opt => opt.MapFrom(a => a.precio));
        }
    }
}
