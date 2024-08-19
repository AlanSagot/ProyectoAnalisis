using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeahMakeUp.Models
{
    [Table("Planillas")]
    public class PlanillasCreateViewModel
    {
        [Key]
        public int PlanillaId { get; set; }
        [Required]
        public int Cedula { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string PrimerApellido { get; set; }
        [Required]
        public string SegundoApellido { get; set; }
        [Required]
        public int PuestoId { get; set; }
        public string Departamento { get; set; }
        public string NombrePuesto { get; set; }
        [Required]
        public int SucursalId { get; set; }
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

    }
}
