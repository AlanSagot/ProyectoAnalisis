using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Catalogos")]
    public class Catalogos
    {
        [Key]
        [Column("CATALOGOID")]
        public int CatalogoId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("NOMBRE_CATALOGO")]
        public string NombreCatalogo { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("DETALLE_CATALOGO")]
        public string DetalleCatalogo { get; set; }

        [ForeignKey("Inventario")]
        [Column("PRODUCTOID")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }
    }

}
