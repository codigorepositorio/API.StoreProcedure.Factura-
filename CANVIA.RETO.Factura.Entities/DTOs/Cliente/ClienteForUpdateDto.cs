using System.ComponentModel.DataAnnotations;

namespace CANVIA.RETO.Factura.Entities.DTOs
{
    public class ClienteForUpdateDto : ClienteForValidadorDto
    {
        [Required(ErrorMessage = "El codigo de Cliente es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El codigo de Cliente mayor a 0")]
        public int codigoCliente { get; set; }
    }
}
