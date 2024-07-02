using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Codigo { get; set; }
        public byte[]? FotoProducto{ get; set; }
        [DisplayName("Nombre del Producto")]
        public string NombreProducto { get; set; }
        [MaxLength(500)]
        public string Categoria { get; set; }
        [Required]
        [DisplayName("Descripcion del Producto")]

        public string DescripcionProducto { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public int PrecioXVenta { get; set; }
        [Required]
        public int PrecioXCosto { get; set; }
        [Required]
        public int Stock { get; set; }
        [DisplayName("Fecha Agregado")]
        public DateTime FechaAgregado { get; set; }
        [DisplayName("Fecha Expiracion")]
        public DateTime FechaExpiracion { get; set; }
        [DisplayName("Sucursal")]
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
        public ICollection<Catalogos> Catalogos { get; set; } = new List<Catalogos>();
        public ICollection<Envio> Envios { get; set; } = new List<Envio>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }

}
