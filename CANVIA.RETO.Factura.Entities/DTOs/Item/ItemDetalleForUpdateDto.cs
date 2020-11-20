using System;

namespace CANVIA.RETO.Item.DTOs
{ 
    public class ItemDetalleForUpdateDto
    {
        public int codigo { get; set; }                
        public string descripcion { get; set; }
        public decimal precioU { get; set; }
    }
}
