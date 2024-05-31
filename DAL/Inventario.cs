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
        [Column("PRODUCTOID")]
        public int ProductoId { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("NOMBRE_PRODUCTO")]
        public string NombreProducto { get; set; }

        [MaxLength(500)]
        [Column("DESCRIPCION_PRODUCTO")]
        public string DescripcionProducto { get; set; }

        [Required]
        [Column("PRECIOXVENTA")]
        public int PrecioXVenta { get; set; }

        [Required]
        [Column("PRECIOXCOSTO")]
        public int PrecioXCosto { get; set; }

        [Required]
        public int Stock { get; set; }

        [Column("FECHA_AGREGADO")]
        public DateTime FechaAgregado { get; set; }

        [Column("FECHA_EXPIRACION")]
        public DateTime FechaExpiracion { get; set; }

        [ForeignKey("Sucursal")]
        [Column("SUCURSALID")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        public ICollection<Catalogos> Catalogos { get; set; } = new List<Catalogos>();
        public ICollection<Envio> Envios { get; set; } = new List<Envio>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }

}
