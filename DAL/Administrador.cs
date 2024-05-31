using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Administradores")]
    public class Administrador
    {
        [Key]
        [Column("ADMINISTRADORID")]
        public int AdministradorId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("NOMBRE_ADMINISTRADOR")]
        public string NombreAdministrador { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("PASSWORD_ADMIN")]
        public string PasswordAdmin { get; set; }

        [ForeignKey("Sucursal")]
        [Column("SUCURSALID")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
