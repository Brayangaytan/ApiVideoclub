using System.ComponentModel.DataAnnotations;

namespace ApiVideoclub.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
