using System;

namespace CANVIA.RETO.Factura.Entities
{
    public class ItemDetalle
    {
        public int ItemDetalleID { get; set; }                
        public string Producto { get; set; }
        public decimal Precio { get; set; }
    }
}
