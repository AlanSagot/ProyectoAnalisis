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
        public int AdministradorId { get; set; }
        [Required]
        [MaxLength(50)]
        public string NombreAdministrador { get; set; }
        [Required]
        [MaxLength(50)]
        public string PasswordAdmin { get; set; }
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
