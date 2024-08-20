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
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int ID_Categoria { get; set; }

        [DisplayName("Categoría")]
        public string Descripcion { get; set; }

        //Propiedad de navegacion
        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}
