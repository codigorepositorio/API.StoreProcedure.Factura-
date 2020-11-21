
using System.ComponentModel.DataAnnotations;

namespace CANVIA.RETO.Factura.Entities.DTOs
{
    
    public abstract class ClienteForValidadorDto

    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 20 characters.")]
      
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string tipo { get; set; }


        [Required(ErrorMessage = "El nombres es requerido")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string nombres { get; set; }


        [Required(ErrorMessage = "El apellidos es requerido")]
        [MaxLength(80, ErrorMessage = "Maximum length for the Name is 80 characters.")]
        public string apellidos { get; set; }


        [Required(ErrorMessage = "El documento es requerido")]
        [MaxLength(15, ErrorMessage = "Maximum length for the Name is 15 characters.")]
        public string documento { get; set; }


        [Required(ErrorMessage = "El numero es requerido")]
        [MaxLength(8, ErrorMessage = "Maximum length for the Name is 8 characters.")]
        public string numero { get; set; }


        [Required(ErrorMessage = "El direccion es requerido")]
        [MaxLength(120, ErrorMessage = "Maximum length for the Name is 120 characters.")]
        public string direccion { get; set; }


        [Required(ErrorMessage = "El telefono es requerido")]
        [MaxLength(9, ErrorMessage = "Maximum length for the Name is 9 characters.")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail no es vàlido")]
        public string email { get; set; }

  
    }
}
