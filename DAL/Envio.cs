using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Envios")]
    public class Envio
    {
        [Key]
        public int EnvioId { get; set; }
        public ApplicationUser Usuario { get; set; }
        [MaxLength(50)]
        public string Cedula { get; set; }
        [MaxLength(50)]
        public string Nombre { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(300)]
        public string Provincia { get; set; }
        [MaxLength(300)]
        public string Canton { get; set; }
        [MaxLength(300)]
        public string Distrito { get; set; }
        [MaxLength(300)]
        public string Direccion { get; set; }
        [MaxLength(15)]
        public string CodigoPostal { get; set; }
    }

}