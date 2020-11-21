using System;
using System.ComponentModel.DataAnnotations;

namespace CANVIA.RETO.Item.DTOs
{ 
    public abstract class ItemDetalleForValidationDto
    {
        [Required(ErrorMessage = "La descripcion es requerido")]
        [MaxLength(80, ErrorMessage = "Maximum length for the Name is 80 characters.")]
        public string descripcion { get; set; }

        [Required(ErrorMessage = "El numero es requerido")]
        [Range(1, int.MaxValue, ErrorMessage = "El preccio mayor a 0")]
        public decimal precio { get; set; }

    }
}

