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
    [Table("Empleados")]
    public class Empleado
    {
        [Key]
        [Required]
        public int EmpleadoId { get; set; }

        [DisplayName("Cédula")]
        public int Cedula {  get; set; }

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

        [DisplayName("Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; }

        [DisplayName("Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        public int Vacaciones { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sexo { get; set; }

        public int Edad { get; set; }

        [Required]
        [ForeignKey("Estado")]
        public int ID_Estado { get; set; }
        public Estado Estado { get; set; }

        [ForeignKey("Puesto")]
        public int PuestoId { get; set; }
        public Puesto Puesto { get; set; }
        public string NombrePuesto { get; set; }

        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        public ICollection<Planillas> Planillas { get; set; } = new List<Planillas>();

        public ICollection<Incapacidades> Incapacidades { get; set; } = new List<Incapacidades>();
    }
}
