using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Planillas")]
    public class Planillas
    {
        [Key]
        public int PlanillaId { get; set; }
        [Required]
        public double PrecioPorHora { get; set; }
        [Required]
        public int HorasTrabajadas { get; set; }
        [Required]
        public double HorasExtra { get; set; }
        [Required]
        public double SalarioBruto { get; set; }
        [Required]
        public double RebajoCCSS { get; set; } = 0.0917;
        [Required]
        public double RebajoINS { get; set; } = 0.01;
        [Required]
        public double RebajoIncapacidadINS { get; set; } = 0.4;
        [Required]
        public double RebajoIncapacidadCCSS { get; set; } = 0.4;
        public double DeduccionesEmbargo { get; set; }
        [Required]
        public double Feriado { get; set; }
        [Required]
        public double SalarioNeto { get; set; } 

        [ForeignKey("Puesto")]
        public int PuestoId { get; set; }
        public Puesto Puesto { get; set; }

        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
    }
}
