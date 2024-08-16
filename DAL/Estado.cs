using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Estados")]
    public class Estado
    {
        [Key]
        public int ID_Estado { get; set; }
        public string Tipo { get; set; }

        //Propiedad de navegacion
        public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
    }
}
