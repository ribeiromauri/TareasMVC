using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TareasMVC.Migrations;
using TareasMVC.Models;

namespace TareasMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public UsuariosController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }
            var usuario = new IdentityUser() { Email = modelo.Email, UserName = modelo.Email };

            var resultado = await userManager.CreateAsync(usuario, password: modelo.Password);

            if (resultado.Succeeded)
            {
                //Si se cierra el navegador va seguir guardando la cookie con isPersistent: true 
                await signInManager.SignInAsync(usuario, isPersistent: true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(modelo);
            }
        }
    }
}
