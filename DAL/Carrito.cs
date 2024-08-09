using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Carritos")]
    public class Carrito
    {

        [Key]
        public int CarritoId { get; set; }

        [Required]
        [MaxLength(256)]
        [ForeignKey("Cedula")]
        public string Cedula { get; set; }
        public ApplicationUser User { get; set; }  

        public int Cantidad { get; set; }
        
        public byte[]? FotoProducto { get; set; }

        [Required]
        [ForeignKey("Inventario")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }

        [Required]
        public int Subtotal { get; set; }

        [Required]
        public int PrecioTotal { get; set; }

        
    }
}
