using AutoMapper;
using CANVIA.RETO.Factura.Entities;
using CANVIA.RETO.Factura.Repository;
using CANVIA.RETO.Item.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CANVIA.RETO.Factura.Services
{
    public class ItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }


        public async Task<ItemDetalleDto> GetById(int clienteID)
        {
            var result = await _itemRepository.GetById(clienteID);
            var itemDetalle = _mapper.Map<ItemDetalleDto>(result);
            return itemDetalle;
        }


        public async Task<IEnumerable<ItemDetalleDto>> GetAll()
        {
            var result = await _itemRepository.GetAll();

            var lstItemDetalle = _mapper.Map<IEnumerable<ItemDetalleDto>>(result);

            return lstItemDetalle;
        }

        public async Task<ItemDetalleDto> Create(ItemDetalleForCreationDto itemDetalleForCreationDto)
        {
            var clienteEntity = _mapper.Map<ItemDetalle>(itemDetalleForCreationDto);

            var clienteReturn = await _itemRepository.Create(clienteEntity);

            var cliente = _mapper.Map<ItemDetalleDto>(clienteReturn);

            return cliente;

        }

        public void Update(ItemDetalleForUpdateDto itemDetalleForUpdateDto)
        {           
            var ItemDetalleEntity = _mapper.Map<ItemDetalle>(itemDetalleForUpdateDto);
            _itemRepository.Update(ItemDetalleEntity);
        }

        public void  Delete(int itemDetalleID)
        {            
            _itemRepository.Delete(itemDetalleID);            
        }
    }
}
