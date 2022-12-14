using ApiVideoclub.Validaciones;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVideoclub.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        [PrimeraLetraMayuscula]
        public string Name { get; set; }
        public List<PeliculaVideoclub> PeliculaVideoclub { get; set; }

    }
}
