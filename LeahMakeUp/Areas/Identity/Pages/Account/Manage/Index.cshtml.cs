// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeahMakeUp.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Nombre")]
            public string Nombre { get; set; }

            [Display(Name = "Primer Apellido")]
            public string PrimerApellido { get; set; }

            [Display(Name = "Segundo Apellido")]
            public string SegundoApellido { get; set; }

            [Display(Name = "Cedula")]
            public string Cedula { get; set; }

            [MaxLength(12)]
            [Display(Name = "Telefono")]
            public string Telefono { get; set; }

            [MaxLength(200)]
            [Display(Name = "Direccion")]
            public string Direccion { get; set; }

            [MaxLength(7)]
            [Display(Name = "Codigo Postal")]
            public string CodigoPostal { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = user.Nombre,
                PrimerApellido = user.PrimerApellido,
                SegundoApellido = user.SegundoApellido,
                Cedula = user.Cedula,
                Telefono = user.Telefono,
                Direccion = user.Direccion,
                CodigoPostal = user.CodigoPostal
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.Nombre = Input.Nombre ?? user.Nombre;
            user.PrimerApellido = Input.PrimerApellido ?? user.PrimerApellido;
            user.SegundoApellido = Input.SegundoApellido ?? user.SegundoApellido;
            user.Cedula = Input.Cedula ?? user.Cedula;
            user.Telefono = Input.Telefono ?? user.Telefono;
            user.Direccion = Input.Direccion ?? user.Direccion;
            user.CodigoPostal = Input.CodigoPostal ?? user.CodigoPostal;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update profile.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count > 0)
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!removeRolesResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to remove user roles.";
                    return RedirectToPage();
                }
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            if (!deleteResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to delete user.";
                return RedirectToPage();
            }

            await _signInManager.SignOutAsync();
            StatusMessage = "Your account has been deleted.";
            return Redirect("~/");
        }
    }
}
