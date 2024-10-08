﻿using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeahMakeUp.Models
{
    [Table("Inventarios")]
    public class InventarioCreateViewModel
    {
        [Key]
        public int ProductoId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Codigo { get; set; }

        //public byte[]? FotoProducto { get; set; }

        [DisplayName("Producto")]
        public string NombreProducto { get; set; }

        [ForeignKey("ID_Categoria")]
        public int ID_Categoria { get; set; }
        public Categoria Categoria { get; set; }

        [Required]
        [DisplayName("Descripción")]
        public string DescripcionProducto { get; set; }

        [ForeignKey("ID_Marca")]
        public int ID_Marca { get; set; }
        public Marca Marca { get; set; }

        [Required]
        [DisplayName("Precio Venta")]
        public int PrecioXVenta { get; set; }

        [Required]
        [DisplayName("Precio Costo")]
        public int PrecioXCosto { get; set; }

        [Required]
        public int Stock { get; set; }

        private DateTime _fechaAgregado;
        [DisplayName("Fecha Agregado")]
        public DateTime FechaAgregado
        {
            get => _fechaAgregado.Date;
            set => _fechaAgregado = value.Date;
        }

        private DateTime _fechaExpiracion;
        [DisplayName("Fecha Expiracion")]
        public DateTime FechaExpiracion
        {
            get => _fechaExpiracion.Date;
            set => _fechaExpiracion = value.Date;
        }

        [DisplayName("Sucursal")]
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }

        [DisplayName("Empleado")]
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        [ForeignKey("ID_Estado")]
        public int ID_Estado { get; set; }
        public Estado Estado { get; set; }

        public ICollection<Catalogos> Catalogos { get; set; } = new List<Catalogos>();
        public ICollection<Envio> Envios { get; set; } = new List<Envio>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
