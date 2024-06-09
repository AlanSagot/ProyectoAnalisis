using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeahMakeUp.Models
{
    public class AdminCrearUsuarioViewModel
    {

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }
        [Required]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }
        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; }
        [Required]
        [DefaultValue(1)]
        public int Estado { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(300)]
        public string Direccion { get; set; }
        [MaxLength(10)]
        public string CodigoPostal { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El largo maximo es de 100")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar password")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Tipo de Rol")]
        public string IdRol { get; set; }
    }
}
