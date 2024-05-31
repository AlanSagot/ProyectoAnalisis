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
        [Column("FACTURAID")]
        public int FacturaId { get; set; }

        [Column("FECHA_ACTUAL")]
        public DateTime FechaActual { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int Impuesto { get; set; }

        [ForeignKey("Inventario")]
        [Column("PRODUCTOID")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }

        [ForeignKey("Usuario")]
        [Column("USUARIOID")]
        public int UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }

        [ForeignKey("Pago")]
        [Column("PAGOID")]
        public int PagoId { get; set; }
        public Pago Pago { get; set; }
    }
}
