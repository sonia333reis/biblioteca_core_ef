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

            ViewData["msg"] = "Existem [" + usuarios.Count + "] usuários cadastrados";

            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Criar([Bind("UsuarioID, Nome, Cpf, Idade, Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(usuario);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["msg"] = ViewData["msg"] + " Erro ao salvar informações no banco de dados. Message [" + e + "]";
                    throw;
                }
            }

            ViewData["msg"] = "Não foi possível completar operação";
            return View(usuario);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                ViewData["msg"] = "Obrigatório informar usuário";
                return NotFound();
            }

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                ViewData["msg"] = "Usuário não encontrado";
                return NotFound();
            }

            return View(usuario);
        }


        [HttpPost]
        public IActionResult Editar(int id, [Bind("UsuarioID, Nome, Cpf, Idade, Email")] Usuario usuario)
        {
            if (id != usuario.UsuarioID)
            {
                ViewData["msg"] = "Comportamento inesperado";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["msg"] = ViewData["msg"] + " Erro ao salvar informações no banco de dados. Message [" + e + "]";
                    throw;
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int? id)
        {
            try
            {
                if (id == null)
                {
                    ViewData["msg"] = "Obrigatório informar usuário";
                    return NotFound();
                }
                Usuario usuario = _context.Usuarios.Find(id);
                _context.Remove(usuario);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["msg"] = ViewData["msg"] + " Erro ao salvar informações no banco de dados. Message [" + e + "]";
                throw;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(string searchString)
        {
            ViewBag.Pesquisa = "";
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.Pesquisa = searchString;
                var usuarios = _context.Usuarios.Where(u => u.Nome.Contains(searchString));
                ViewData["msg"] = "Existem " + usuarios.Count() + " resultados para este usuário.";
                return View("Index", usuarios);
            }
            else
            {
                ViewData["msg"] = "Nenhum nome foi inserido";
                return RedirectToAction("SelectAllUsers");
            }
        }
    }
}