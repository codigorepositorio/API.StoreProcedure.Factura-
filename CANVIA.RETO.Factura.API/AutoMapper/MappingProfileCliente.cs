using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Entities.DTOs;

namespace CANVIA.RETO.Factura.API.AutoMapper
{
    public class MappingProfileCliente : Profile
    {
        public MappingProfileCliente()
        {
            //GET: Cliente y ClienteDto 
            // A     =====>     //B  
            CreateMap<Cliente, ClienteDto>()
                .ForMember(b => b.codigoCliente, opt => opt.MapFrom(a => a.clienteID))
                .ForMember(b => b.tipo, opt => opt.MapFrom(a => a.TipoPersona))
                .ForMember(b => b.nombres, opt => opt.MapFrom(a => a.Nombres))
                .ForMember(b => b.apellidos, opt => opt.MapFrom(a => a.Apellidos))
                .ForMember(b => b.documento, opt => opt.MapFrom(a => a.TipoDocumento))
                .ForMember(b => b.numero, opt => opt.MapFrom(a => a.NumDocumento))
                .ForMember(b => b.direccion, opt => opt.MapFrom(a => a.Direccion))
                .ForMember(b => b.telefono, opt => opt.MapFrom(a => a.Telefono))
                .ForMember(b => b.email, opt => opt.MapFrom(a => a.Email));

            //POST: Cliente y ClienteDto 
            // A     =====>     //B  
            CreateMap<ClienteForCreationDto, Cliente>()
                  .ForMember(b => b.clienteID, opt => opt.MapFrom(a => a.codigoCliente))
                .ForMember(b => b.TipoPersona, opt => opt.MapFrom(a => a.tipo))
                .ForMember(b => b.Nombres, opt => opt.MapFrom(a => a.nombres))
                .ForMember(b => b.Apellidos, opt => opt.MapFrom(a => a.apellidos))
                .ForMember(b => b.TipoDocumento, opt => opt.MapFrom(a => a.documento))
                .ForMember(b => b.NumDocumento, opt => opt.MapFrom(a => a.numero))
                .ForMember(b => b.Direccion, opt => opt.MapFrom(a => a.direccion))
                .ForMember(b => b.Telefono, opt => opt.MapFrom(a => a.telefono))
                .ForMember(b => b.Email, opt => opt.MapFrom(a => a.email));

            //PUT: Cliente y ClienteDto 
            // A     =====>     //B  
            CreateMap<ClienteForUpdateDto, Cliente>()
                 .ForMember(b => b.clienteID, opt => opt.MapFrom(a => a.codigoCliente))
                .ForMember(b => b.TipoPersona, opt => opt.MapFrom(a => a.tipo))
                .ForMember(b => b.Nombres, opt => opt.MapFrom(a => a.nombres))
                .ForMember(b => b.Apellidos, opt => opt.MapFrom(a => a.apellidos))
                .ForMember(b => b.TipoDocumento, opt => opt.MapFrom(a => a.documento))
                .ForMember(b => b.NumDocumento, opt => opt.MapFrom(a => a.numero))
                .ForMember(b => b.Direccion, opt => opt.MapFrom(a => a.direccion))
                .ForMember(b => b.Telefono, opt => opt.MapFrom(a => a.telefono))
                .ForMember(b => b.Email, opt => opt.MapFrom(a => a.email));
        }
    }
}
