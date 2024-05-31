﻿using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class LeahDBContext : DbContext
    {
        public LeahDBContext() { }

        public LeahDBContext(DbContextOptions<LeahDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Catalogos> Catalogos { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Envio> Envios { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Planillas> Planillas { get; set; }
        public DbSet<Puesto> Puestos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }


    }
}
