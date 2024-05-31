using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Pagos")]
    public class Pago
    {
        [Key]
        [Column("PAGOID")]
        public int PagoId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Detalle { get; set; }

        [Required]
        [Column("COMPROBANTE_DE_PAGO", TypeName = "VARBINARY(MAX)")]
        public byte[] ComprobanteDePago { get; set; }
    }

}
