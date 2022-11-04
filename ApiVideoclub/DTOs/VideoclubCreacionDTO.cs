using ApiVideoclub.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiVideoclub.DTOs
{
    public class VideoclubCreacionDTO
    {
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public List<int> PeliculasIds { get; set; }
    }
}
