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
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        [Column("EMPLEADOID")]
        public int EmpleadoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("PRIMER_APELLIDO")]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("SEGUNDO_APELLIDO")]
        public string SegundoApellido { get; set; }

        [Required]
        [MaxLength(30)]
        public string Cedula { get; set; }

        [Required]
        [MaxLength(70)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(300)]
        public string Direccion { get; set; }

        [Column("FECHA_CONTRATACION")]
        public DateTime FechaContratacion { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; }

        [ForeignKey("Puesto")]
        [Column("PUESTOID")]
        public int PuestoId { get; set; }
        public Puesto Puesto { get; set; }

        [ForeignKey("Planilla")]
        [Column("PLANILLAID")]
        public int PlanillaId { get; set; }
        public Planillas Planillas { get; set; }

        [ForeignKey("Sucursal")]
        [Column("SUCURSALID")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }
}
