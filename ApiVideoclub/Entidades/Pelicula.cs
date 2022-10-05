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

        [Range(1900,2022, ErrorMessage = "El campo estreno no esta dentro del rango")]
        [NotMapped]
        public int estreno { get; set; }

        public List<Videoclub> videoclubs { get; set; }

        [NotMapped]
        public int Menor { get; set; }
        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                var primeraLetra = Name[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] {nameof(Name)});
                }
            }

            if (Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo mayor",
                    new String[] { nameof(Menor) });
            }
        }
    }
}
