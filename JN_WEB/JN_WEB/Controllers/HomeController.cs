using JN_WEB.Entities;
using JN_WEB.Interface;
using JN_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JN_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController(IUsuarioModel usuarioModel, IToolsModel toolsModel) : Controller
    {
        //------------------------------------- Iniciar Sesion
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Usuario ent)
        {
            ent.Contrasenna = toolsModel.Encrypt(ent.Contrasenna!);

            var resp = usuarioModel.IniciarSesion(ent);

            if(resp.Codigo == 1)
            {
                return RedirectToAction("Home", "Home");
            }
            else
            {
                ViewBag.danger = resp.Mensaje;
                return View();
            }
        }
        //------------------------------------- Registrar Usuario
        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegistrarUsuario(Usuario ent)
        {
            ent.Contrasenna = toolsModel.Encrypt(ent.Contrasenna!);

            var resp = usuarioModel.RegistrarUsuario(ent);

            if (resp.Codigo == 1)
            {
                TempData["success"] = resp.Mensaje;
                return RedirectToAction("Login", "Home");
            }
            else
            {
                ViewBag.danger = resp.Mensaje;
                return View();
            }
        }
        //------------------------------------- Recuperar
        [HttpGet]
        public IActionResult RecuperarAcceso()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RecuperarAcceso(Usuario ent)
        {
            return View();
        }
        //------------------------------------- Home
        public IActionResult Home()
        {
            return View();
        }

        
    }
}