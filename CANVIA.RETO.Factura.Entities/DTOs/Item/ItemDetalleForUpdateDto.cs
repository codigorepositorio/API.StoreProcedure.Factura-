using System;
using System.ComponentModel.DataAnnotations;

namespace CANVIA.RETO.Item.DTOs
{ 
    public class ItemDetalleForUpdateDto : ItemDetalleForValidationDto
    {
        [Required(ErrorMessage = "El codigo de Item es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El codigo de Item mayor a 0")]
        public int codigoItem { get; set; }
    }
}
