using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biblioteca_core_att.Models;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca_core_att.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Context _context;

        public UsuarioController(Context context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuarios.ToList();

            ViewData["msg"] = "Existem [" +usuarios.Count+"] usuários cadastrados";

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar()
        {

        }

        public IActionResult Editar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Editar()
        {
            
        }

        public void Excluir()
        {
            
        }
    }
}