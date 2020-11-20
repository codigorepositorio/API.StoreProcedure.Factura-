using System;

namespace CANVIA.RETO.Factura.Entities
{
    public class FacturaDetalle
    {
        public int FacturaDetalleID { get; set; }
        public int FacturaCabeceraID { get; set; }
        public int ItemDetalleID { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }

    }
}
