using DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeahMakeUp.Models
{
    [Table("Empleado")]
    public class EmpleadoCreateViewModel
    {
        [Key]
        [Required]
        public int EmpleadoId { get; set; }

        [DisplayName("Cédula")]
        public int Cedula { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [MaxLength(70)]
        public string Email { get; set; }

        [Required]
        [MaxLength(30)]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [MaxLength(300)]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        private DateTime _fechaContratacion;
        [DisplayName("Fecha de Contratación")]
        public DateTime FechaContratacion {
            get => _fechaContratacion.Date;
            set => _fechaContratacion = value.Date;
        }

        private DateTime _fechaNacimiento;
        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento {
            get => _fechaNacimiento.Date;
            set => _fechaNacimiento = value.Date;
        }

        public int Vacaciones { get; set; }

        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        public string Sexo { get; set; }

        [Required]
        [DisplayName("Estado")]
        public int ID_Estado { get; set; }
        public string Tipo { get; set; }

        public int PuestoId { get; set; }
        public string Departamento { get; set; }

        [DisplayName("Puesto")]
        public string NombrePuesto { get; set; }

        public int SucursalId { get; set; }

        public ICollection<Planillas> Planillas { get; set; } = new List<Planillas>();

        public ICollection<Incapacidades> Incapacidades { get; set; } = new List<Incapacidades>();
        public List<SelectListItem> SexoOptions { get; internal set; }
    }
}