using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Sucursal")]
    public class Sucursal
    {
        [Key]
        [Column("SUCURSALID")]
        public int SucursalId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Direccion { get; set; }

        [Required]
        [MaxLength(50)]
        public string Horario { get; set; }

        public ICollection<Planillas> Planillas { get; set; } = new List<Planillas>();
        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
        public ICollection<Administrador> Administradores { get; set; } = new List<Administrador>();
        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}

