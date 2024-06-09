using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LeahMakeUp.Models
{
    public class AdminEditarUsuarioViewModel
    {
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }

        [Required]
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; }

        [Required]
        [DefaultValue(1)]
        public string Estado { get; set; }

        [MaxLength(20)]
        public string Telefono { get; set; }

        [MaxLength(300)]
        public string Direccion { get; set; }

        [MaxLength(10)]
        public string CodigoPostal { get; set; }

        [Required]
        public string RoleId { get; set; }

        public List<SelectListItem> EstadoOptions { get; set; }

    }

}
