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
        [Required]
        [MaxLength(250)]
        public string Detalle { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
        [MaxLength(20)]
        public string Cedula { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Telefono { get; set; }
        [MaxLength(300)]
        public string Direccion { get; set; }
        [MaxLength(10)]
        public string CodigoPostal { get; set; }
        public int Impuesto { get; set; }
        [ForeignKey("Inventario")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }
    }

}
