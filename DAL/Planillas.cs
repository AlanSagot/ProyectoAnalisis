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
        [DisplayName("Salario por Hora")]
        public double PrecioPorHora { get; set; }
        [Required]
        [DisplayName("Horas Trabajadas")]
        public int HorasTrabajadas { get; set; }
        [Required]
        [DisplayName("Horas Extra")]
        public double HorasExtra { get; set; }
        [Required]
        [DisplayName("Salario Bruto")]
        public double SalarioBruto { get; set; }
        [Required]
        [DisplayName("CCSS")]
        public double RebajoCCSS { get; set; } = 0.0917;
        [Required]
        [DisplayName("INS")]
        public double RebajoINS { get; set; } = 0.01;
        [Required]
        [DisplayName("Incapacidad INS")]
        public double RebajoIncapacidadINS { get; set; } = 0.4;
        [Required]
        [DisplayName("Incapacidad CCSS")]
        public double RebajoIncapacidadCCSS { get; set; } = 0.4;
        [DisplayName("Embargo")]
        public double DeduccionesEmbargo { get; set; }
        [Required]
        public double Feriado { get; set; }
        [Required]
        [DisplayName("Salario Neto")]
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
