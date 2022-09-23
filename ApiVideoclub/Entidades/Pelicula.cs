using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiVideoclub.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }

        [Range(1900,2022, ErrorMessage = "El campo estreno no esta dentro del rango")]
        [NotMapped]
        public int estreno { get; set; }

        public List<Videoclub> videoclubs { get; set; }
    }
}
