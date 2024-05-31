using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Usuarios")]
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int UsuarioId { get; set; }
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
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(300)]
        public string Direccion { get; set; }
        [MaxLength(10)]
        public string CodigoPostal { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

}
