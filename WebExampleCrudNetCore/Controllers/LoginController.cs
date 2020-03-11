using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebExampleCrudNetCore.Models;
using WebExampleCrudNetCore.Class;
namespace WebExampleCrudNetCore.Controllers
{
    [Route("account")]
    public class LoginController : Controller
    {
        private readonly dbproductosContext _context;
        PasswordHasher2 saltPassword = new PasswordHasher2();
        public LoginController(dbproductosContext context)
        {
            _context = context;
        }

        [Route("")]
        [Route("index")]
        [Route("~/")]
        public IActionResult Index()
        {

            /*
             * EJEMPLO DE USO PASSWORD
             * 
             * 
             * Usuario usuario = new Usuario();
            usuario.usuario = "ck";
            usuario.password = saltPassword.HashPassword("123456");
            usuario.activo = true;
            usuario.email = "ck@gemail.com";
            _context.Add(usuario);
            _context.SaveChanges();*/

            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string email, string password)
        {

            var usuario = _context.Usuario.Where(p => p.email == email).ToList();

            if (usuario.Count() > 0)
            {                
                int hashPassword = (int)saltPassword.VerifyHashedPassword(usuario.First().password, password);

                if (hashPassword == 1)
                {
                    HttpContext.Session.SetString("usuario", usuario.First().usuario);
                    return RedirectToAction("Index", "Productos");
                }
                else
                {
                    ViewBag.error = "Contraseña o usuario incorrecto";
                    return View("Index");
                }
            }
            else {
                ViewBag.error = "Cuenta Invalida";
                return View("Index");
            }

        }

        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Index");
        }
    }
}