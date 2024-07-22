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
        public string Cedula { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string PrimerApellido { get; set; }
        [Required]
        public string SegundoApellido { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public DateTime FechaContratacion { get; set; }
        [Required]
        public int PuestoId { get; set; }
        public string Departamento { get; set; }
        public string NombrePuesto { get; set; }
        [Required]
        public int SucursalId { get; set; }
        [Required]
        public int Salario { get; set; }
    }
}
