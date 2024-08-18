﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(LeahDBContext))]
    [Migration("20240818190539_Marca_Categoria")]
    partial class Marca_Categoria
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DAL.Administrador", b =>
                {
                    b.Property<int>("AdministradorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdministradorId"));

                    b.Property<string>("NombreAdministrador")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordAdmin")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.HasKey("AdministradorId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("DAL.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegundoApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DAL.Carrito", b =>
                {
                    b.Property<int>("CarritoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarritoId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<byte[]>("FotoProducto")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PrecioTotal")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("Subtotal")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CarritoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UserId");

                    b.ToTable("Carritos");
                });

            modelBuilder.Entity("DAL.Catalogos", b =>
                {
                    b.Property<int>("CatalogoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatalogoId"));

                    b.Property<string>("DetalleCatalogo")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NombreCatalogo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("CatalogoId");

                    b.HasIndex("ProductoId");

                    b.ToTable("Catalogos");
                });

            modelBuilder.Entity("DAL.Categoria", b =>
                {
                    b.Property<int>("ID_Categoria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Categoria"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Categoria");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("DAL.Empleado", b =>
                {
                    b.Property<int>("EmpleadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmpleadoId"));

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("PlanillaId")
                        .HasColumnType("int");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PuestoId")
                        .HasColumnType("int");

                    b.Property<string>("SegundoApellido")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EmpleadoId");

                    b.HasIndex("PuestoId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("DAL.Envio", b =>
                {
                    b.Property<int>("EnvioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnvioId"));

                    b.Property<string>("Canton")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Cedula")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CodigoPostal")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Direccion")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Distrito")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Email")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("InventarioProductoId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Provincia")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EnvioId");

                    b.HasIndex("InventarioProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Envios");
                });

            modelBuilder.Entity("DAL.Estado", b =>
                {
                    b.Property<int>("ID_Estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Estado"));

                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Estado");

                    b.ToTable("Estados");
                });

            modelBuilder.Entity("DAL.Factura", b =>
                {
                    b.Property<int>("FacturaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FacturaId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaActual")
                        .HasColumnType("datetime2");

                    b.Property<int>("Impuesto")
                        .HasColumnType("int");

                    b.Property<int>("PagoId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<string>("UsuarioId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FacturaId");

                    b.HasIndex("PagoId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("DAL.Inventario", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<int?>("CategoriaID_Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("DescripcionProducto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EstadoID_Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaAgregado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaExpiracion")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("FotoProducto")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("ID_Categoria")
                        .HasColumnType("int");

                    b.Property<int>("ID_Estado")
                        .HasColumnType("int");

                    b.Property<int>("ID_Marca")
                        .HasColumnType("int");

                    b.Property<int?>("InventarioProductoId")
                        .HasColumnType("int");

                    b.Property<int?>("MarcaID_Marca")
                        .HasColumnType("int");

                    b.Property<string>("NombreProducto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrecioXCosto")
                        .HasColumnType("int");

                    b.Property<int>("PrecioXVenta")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId");

                    b.HasIndex("CategoriaID_Categoria");

                    b.HasIndex("EstadoID_Estado");

                    b.HasIndex("InventarioProductoId");

                    b.HasIndex("MarcaID_Marca");

                    b.HasIndex("SucursalId");

                    b.ToTable("Inventarios");
                });

            modelBuilder.Entity("DAL.Marca", b =>
                {
                    b.Property<int>("ID_Marca")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID_Marca"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID_Marca");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("DAL.Pago", b =>
                {
                    b.Property<int>("PagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PagoId"));

                    b.Property<byte[]>("ComprobanteDePago")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("PagoId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("DAL.Planillas", b =>
                {
                    b.Property<int>("PlanillaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanillaId"));

                    b.Property<int>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int>("PuestoId")
                        .HasColumnType("int");

                    b.Property<int>("Salario")
                        .HasColumnType("int");

                    b.Property<int>("SucursalId")
                        .HasColumnType("int");

                    b.HasKey("PlanillaId");

                    b.HasIndex("EmpleadoId")
                        .IsUnique();

                    b.HasIndex("PuestoId");

                    b.HasIndex("SucursalId");

                    b.ToTable("Planillas");
                });

            modelBuilder.Entity("DAL.Puesto", b =>
                {
                    b.Property<int>("PuestoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PuestoId"));

                    b.Property<string>("Departamento")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NombrePuesto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PuestoId");

                    b.ToTable("Puestos");
                });

            modelBuilder.Entity("DAL.Sucursal", b =>
                {
                    b.Property<int>("SucursalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SucursalId"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Horario")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SucursalId");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("DAL.Administrador", b =>
                {
                    b.HasOne("DAL.Sucursal", "Sucursal")
                        .WithMany("Administradores")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("DAL.Carrito", b =>
                {
                    b.HasOne("DAL.Inventario", "Inventario")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Inventario");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Catalogos", b =>
                {
                    b.HasOne("DAL.Inventario", "Inventario")
                        .WithMany("Catalogos")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventario");
                });

            modelBuilder.Entity("DAL.Empleado", b =>
                {
                    b.HasOne("DAL.Puesto", "Puesto")
                        .WithMany("Empleados")
                        .HasForeignKey("PuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Sucursal", "Sucursal")
                        .WithMany("Empleados")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Puesto");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("DAL.Envio", b =>
                {
                    b.HasOne("DAL.Inventario", null)
                        .WithMany("Envios")
                        .HasForeignKey("InventarioProductoId");

                    b.HasOne("DAL.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DAL.Factura", b =>
                {
                    b.HasOne("DAL.Pago", "Pago")
                        .WithMany()
                        .HasForeignKey("PagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Inventario", "Inventario")
                        .WithMany("Facturas")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.ApplicationUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId");

                    b.Navigation("Inventario");

                    b.Navigation("Pago");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DAL.Inventario", b =>
                {
                    b.HasOne("DAL.Categoria", "Categoria")
                        .WithMany("Inventarios")
                        .HasForeignKey("CategoriaID_Categoria");

                    b.HasOne("DAL.Estado", "Estado")
                        .WithMany("Inventarios")
                        .HasForeignKey("EstadoID_Estado");

                    b.HasOne("DAL.Inventario", null)
                        .WithMany("Inventarios")
                        .HasForeignKey("InventarioProductoId");

                    b.HasOne("DAL.Marca", "Marca")
                        .WithMany("Inventarios")
                        .HasForeignKey("MarcaID_Marca");

                    b.HasOne("DAL.Sucursal", "Sucursal")
                        .WithMany("Inventarios")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Estado");

                    b.Navigation("Marca");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("DAL.Planillas", b =>
                {
                    b.HasOne("DAL.Empleado", "Empleado")
                        .WithOne("Planillas")
                        .HasForeignKey("DAL.Planillas", "EmpleadoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Puesto", "Puesto")
                        .WithMany("Planillas")
                        .HasForeignKey("PuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Sucursal", "Sucursal")
                        .WithMany("Planillas")
                        .HasForeignKey("SucursalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");

                    b.Navigation("Puesto");

                    b.Navigation("Sucursal");
                });

            modelBuilder.Entity("DAL.Categoria", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("DAL.Empleado", b =>
                {
                    b.Navigation("Planillas");
                });

            modelBuilder.Entity("DAL.Estado", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("DAL.Inventario", b =>
                {
                    b.Navigation("Catalogos");

                    b.Navigation("Envios");

                    b.Navigation("Facturas");

                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("DAL.Marca", b =>
                {
                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("DAL.Puesto", b =>
                {
                    b.Navigation("Empleados");

                    b.Navigation("Planillas");
                });

            modelBuilder.Entity("DAL.Sucursal", b =>
                {
                    b.Navigation("Administradores");

                    b.Navigation("Empleados");

                    b.Navigation("Inventarios");

                    b.Navigation("Planillas");
                });
#pragma warning restore 612, 618
        }
    }
}
