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
    [Table("Roles")]
    public class Rol
    {
        [Key]
        public int Id_Rol { get; set; }

        [DisplayName("Rol")]
        public string Descripcion { get; set; }

        //Proppiedad de navegacion
        //public ICollection<Administracion> Administracion { get; set; } = new List<Administracion>();
    }
}
