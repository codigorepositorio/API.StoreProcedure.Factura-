using System;

namespace CANVIA.RETO.Item.DTOs
{ 
    public class ItemDetalleForCreationDto
    {
        public int codigoItem { get; set; }                
        public string descripcion { get; set; }
        public decimal precio { get; set; }
    }
}
