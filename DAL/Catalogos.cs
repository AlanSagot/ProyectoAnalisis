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
    [Table("Catalogos")]
    public class Catalogos
    {
        [Key]
        public int CatalogoId { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Nombre del Catálogo")]

        public string NombreCatalogo { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Detalle")]
        public string DetalleCatalogo { get; set; }
        [ForeignKey("Inventario")]
        public int ProductoId { get; set; }
        public Inventario Inventario { get; set; }
    }

}
