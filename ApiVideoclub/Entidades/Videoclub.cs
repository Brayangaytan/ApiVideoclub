using ApiVideoclub.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace ApiVideoclub.Entidades
{
    public class Videoclub
    {
        public int Id { get; set; }
        [Required]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength:250)]
        public string Name { get; set; }
        public List<Reseña> Reseñas { get; set; }
        public List<PeliculaVideoclub> PeliculaVideoclub { get; set; }
    }
}
