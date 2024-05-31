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
        public int PagoId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Detalle { get; set; }
        [Required]
        public byte[] ComprobanteDePago { get; set; }
    }

}
