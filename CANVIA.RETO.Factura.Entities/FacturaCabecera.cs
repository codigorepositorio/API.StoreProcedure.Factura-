using System;
using System.Collections.Generic;

namespace CANVIA.RETO.Factura.Entities
{
    public class FacturaCabecera
    {
        public int FacturaCabeceraID { get; set; }
        public string NumeroFactura { get; set; }
        public decimal ImporteTotal { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteID { get; set; }

        public ICollection<FacturaDetalle> FacturaDetalle { get; set; }
    }
}
