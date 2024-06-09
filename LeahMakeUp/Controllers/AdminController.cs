using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeahMakeUp.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;

namespace ProyectoVeterinaria.Controllers
{
    public class AdminController : Controller
    {
        private readonly AuthDbContext _authDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(AuthDbContext authDbContext, RoleManager<IdentityRole> roleManager, 
            IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            _authDbContext = authDbContext;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore < ApplicationUser>) _userStore;
            _userManager = userManager;
        }
        //Trae Lista de Usuarios
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return View(usuarios);
        }

        //Crear Usuario Inicio
        [Authorize(Roles = "Admin")]
        public IActionResult CrearUsuario()
        {
            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(listaRoles, "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CrearUsuario(AdminCrearUsuarioViewModel usuarioModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                await _userStore.SetUserNameAsync(user, usuarioModel.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, usuarioModel.Email, CancellationToken.None);
                user.Nombre = usuarioModel.Nombre;
                user.PrimerApellido = usuarioModel.PrimerApellido;
                user.SegundoApellido = usuarioModel.SegundoApellido;
                user.Cedula = usuarioModel.Cedula;
                user.Estado = "Activo";
                user.Telefono = usuarioModel.Telefono;
                user.Direccion = usuarioModel.Direccion;
                user.CodigoPostal = usuarioModel.CodigoPostal;
                user.FechaRegistro = DateTime.UtcNow; 

                var result = await _userManager.CreateAsync(user, usuarioModel.Password);

                if (result.Succeeded)
                {
                    string NormalizeRoleName = _roleManager.Roles.FirstOrDefault(r => r.Id == usuarioModel.IdRol).NormalizedName;
                    var resultRole = await _userManager.AddToRoleAsync(user, NormalizeRoleName);

                    return RedirectToAction("Index", "Admin");
                }
            }
            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(listaRoles, "Id", "Name");
            return View(usuarioModel);
        }
        //Crear Usuario Final

        //Editar desde Admin
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditarUsuario(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var model = new AdminEditarUsuarioViewModel
            {
                Id = usuario.Id,
                Email = usuario.Email,
                Nombre = usuario.Nombre,
                PrimerApellido = usuario.PrimerApellido,
                SegundoApellido = usuario.SegundoApellido,
                Cedula = usuario.Cedula,
                Estado = usuario.Estado.ToString(),
                Telefono = usuario.Telefono,
                Direccion = usuario.Direccion,
                CodigoPostal = usuario.CodigoPostal,
                EstadoOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "Activo", Text = "Activo" },
            new SelectListItem { Value = "Inactivo", Text = "Inactivo" }
        }
            };

            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(listaRoles, "Id", "Name");

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditarUsuario(AdminEditarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByIdAsync(model.Id);
                if (usuario == null)
                {
                    return NotFound();
                }

                usuario.Email = model.Email;
                usuario.Nombre = model.Nombre;
                usuario.PrimerApellido = model.PrimerApellido;
                usuario.SegundoApellido = model.SegundoApellido;
                usuario.Cedula = model.Cedula;
                usuario.Estado = model.Estado; 
                usuario.Telefono = model.Telefono;
                usuario.Direccion = model.Direccion;
                usuario.CodigoPostal = model.CodigoPostal;

                var result = await _userManager.UpdateAsync(usuario);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            var listaRoles = _roleManager.Roles;
            ViewData["Roles"] = new SelectList(listaRoles, "Id", "Name");

            return View(model);
        }
        //Eliminar Usuario
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EliminarUsuario(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var rolesForUser = await _userManager.GetRolesAsync(usuario);
            if (rolesForUser.Any())
            {
                foreach (var item in rolesForUser.ToList())
                {
                    var result = await _userManager.RemoveFromRoleAsync(usuario, item);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return RedirectToAction("Index");
                    }
                }
            }

            var resultDelete = await _userManager.DeleteAsync(usuario);
            if (resultDelete.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in resultDelete.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return RedirectToAction("Index");
        }


    }
}
