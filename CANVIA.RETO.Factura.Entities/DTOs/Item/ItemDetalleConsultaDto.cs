using System;

namespace CANVIA.RETO.Item.DTOs
{ 
    public class ItemDetalleConsultaDto
    {
        public int codigoFactura { get; set; }                
        public int codigoDetalle { get; set; }
        public int codigoItem { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int cantidad{ get; set; }
        public decimal subTotal { get; set; }

    }
}

