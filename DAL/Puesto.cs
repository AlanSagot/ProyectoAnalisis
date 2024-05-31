using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Puestos")]
    public class Puesto
    {
        [Key]
        [Column("PUESTOID")]
        public int PuestoId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("NOMBRE_PUESTO")]
        public string NombrePuesto { get; set; }

        [Required]
        [MaxLength(300)]
        public string Departamento { get; set; }

        [Required]
        [MaxLength(50)]
        public string Horario { get; set; }

        public ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
        public ICollection<Planillas> Planillas { get; set; } = new List<Planillas>();
    }

}

