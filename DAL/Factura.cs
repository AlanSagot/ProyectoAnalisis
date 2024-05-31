using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Facturas")]
    public class Factura
    {
        [Key]
        public int FacturaId { get; set; }
        public DateTime FechaActual { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public int Impuesto { get; set; }
        [ForeignKey("Inventario")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
        [ForeignKey("Pago")]
        public int PagoId { get; set; }
        public Pago Pago { get; set; }
    }
}
