using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biblioteca_core_att.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace biblioteca_core_att.Controllers
{
    public class EntregaController : Controller
    {
        private readonly Context _context;

        public EntregaController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var entregas = _context.Entregas.Include(u => u.Usuarios).Include(l => l.Livros).ToList();

            ViewData["msg"] = "Existem [" + entregas.Count + "] reservas cadastrados";

            return View(entregas);
        }

        public IActionResult Criar()
        {
            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nome");

            ViewData["LivroID"] = new SelectList(_context.Livros, "LivroID", "Nome");

            return View();
        }

        [HttpPost]
        public IActionResult Criar([Bind("EntregaID, UsuarioID, LivroID")] Entrega entrega)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(entrega);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    //log
                    ViewData["msg"] = ViewData["msg"] + " Erro ao salvar informações no banco de dados. Message [" + e + "]";
                    throw;
                }
            }

            ViewData["msg"] = "Não foi possível completar operação";
            return View(entrega);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                ViewData["msg"] = "Obrigatório informar reserva";
                return NotFound();
            }

            var entrega = _context.Entregas.Find(id);
            if (entrega == null)
            {
                ViewData["msg"] = "Usuário não encontrado";
                return NotFound();
            }

            ViewData["UsuarioID"] = new SelectList(_context.Usuarios, "UsuarioID", "Nome");
            ViewData["LivroID"] = new SelectList(_context.Livros, "LivroID", "Nome");

            return View(entrega);
        }

        [HttpPost]
        public IActionResult Editar(int id, [Bind("EntregaID, UsuarioID, LivroID")] Entrega entrega)
        {
            if (id != entrega.EntregaID)
            {
                ViewData["msg"] = "Comportamento inesperado";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entrega);
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
                    ViewData["msg"] = "Obrigatório informar reserva";
                    return NotFound();
                }
                Entrega entrega = _context.Entregas.Find(id);
                _context.Remove(entrega);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                ViewData["msg"] = ViewData["msg"] + " Erro ao salvar informações no banco de dados. Message [" + e + "]";
                throw;
            }
            return RedirectToAction("Index");
        }
    }
}