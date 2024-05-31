﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    [Table("Planillas")]
    public class Planillas
    {
        [Key]
        [Column("PLANILLAID")]
        public int PlanillaId { get; set; }

        public int Salario { get; set; }

        [Column("FECHA_INGRESO")]
        public DateTime FechaIngreso { get; set; }

        [ForeignKey("Puesto")]
        [Column("PUESTOID")]
        public int PuestoId { get; set; }
        public Puesto Puesto { get; set; }

        [ForeignKey("Sucursal")]
        [Column("SUCURSALID")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; }
    }

}

