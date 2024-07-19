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
        public string UserName { get; set; }

        [ForeignKey("UserName")]
        public ApplicationUser User { get; set; }

        public int Cantidad { get; set; }

        [Required]
        public int ProductoId { get; set; }
        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

        [Required]
        public int PrecioTotal { get; set; }

        
    }
}
