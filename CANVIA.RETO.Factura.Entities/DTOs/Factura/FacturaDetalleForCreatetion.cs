using System;

namespace CANVIA.RETO.Factura.Entities.DTOs
{
    public class FacturaDetalleForCreatetion
    {
        public int codigoDetalle { get; set; }
        public int codigoFactura { get; set; }
        public int codigoItem{ get; set; }
        public string item { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }

    }
}
