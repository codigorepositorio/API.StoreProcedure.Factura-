using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CANVIA.RETO.Factura.Entities.DTOs
{
    public class FacturaForCreatetion
    {
        public int codigoFactura { get; set; }

        [Required(ErrorMessage = "El codigo de Cliente es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El codigo de Cliente mayor a 0")]
        public int codigoCliente { get; set; }

        [Required(ErrorMessage = "Nùmero Factura de requerido")]
        [MaxLength(11, ErrorMessage = "Maximo 11 caracteres.")]
        public string numeroFactura { get; set; }

        [Required(ErrorMessage = "El codigo de Cliente es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El importeTotal de la Factura mayor a 0")]
        public decimal importeTotal { get; set; }

        [Required(ErrorMessage = "Detalle de Factura es requerido")]        
        public ICollection<FacturaDetalleForCreatetion> itemDetalles { get; set; }
    }
}
