using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Inventarios")]
    public class Inventario
    {
        [Key]
        public int ProductoId { get; set; }
        [Required]
        [MaxLength(150)]
        public string NombreProducto { get; set; }
        [MaxLength(500)]
        public string DescripcionProducto { get; set; }
        [Required]
        public int PrecioXVenta { get; set; }
        [Required]
        public int PrecioXCosto { get; set; }
        [Required]
        public int Stock { get; set; }
        public DateTime FechaAgregado { get; set; }
        public DateTime FechaExpiracion { get; set; }
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<Catalogos> Catalogos { get; set; } = new List<Catalogos>();
        public ICollection<Envio> Envios { get; set; } = new List<Envio>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }

}
