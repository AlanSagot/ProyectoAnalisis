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
    [Table("Incapacidades")]
    public class Incapacidades
    {
        [Key]
        public int IncapacidadId { get; set; }
        public string Institucion { get; set; } 
        public string Motivo { get; set; }
        public double Porcentaje {  get; set; }
        [DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [DisplayName("Fecha Final")]
        public DateTime FechaFin { get; set; }

        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
    }
}
