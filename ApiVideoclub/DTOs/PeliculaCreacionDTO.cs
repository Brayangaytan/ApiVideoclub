using ApiVideoclub.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiVideoclub.DTOs
{
    public class PeliculaCreacionDTO
    {
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
    }
}
