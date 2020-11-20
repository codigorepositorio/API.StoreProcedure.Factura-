using System;

namespace CANVIA.RETO.Item.DTOs
{ 
    public class ItemDetalleDto
    {
        public int codigoFactura { get; set; }                
        public int codigoDetalle { get; set; }
        public int codigoITem { get; set; }
        public string producto { get; set; }
        public decimal precio { get; set; }
        public int cantidad{ get; set; }
        public decimal subTotal { get; set; }

    }
}

