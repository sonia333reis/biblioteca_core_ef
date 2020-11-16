using System;
using System.Linq;
using biblioteca_core_att.Models;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca_core_att.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly Context _context;

        public FuncionarioController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var funcionarios = _context.Funcionarios.ToList();

            ViewData["msg"] = "Existem [" + funcionarios.Count + "] funcionários cadastrados";

            return View(funcionarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([Bind("FuncionarioID, Nome, Cpf, Idade, Email, Senha")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(funcionario);
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
            return View(funcionario);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                ViewData["msg"] = "Obrigatório informar funcionário";
                return NotFound();
            }

            var funcionario = _context.Funcionarios.Find(id);
            if (funcionario == null)
            {
                ViewData["msg"] = "Funcionário não encontrado";
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost]
        public IActionResult Editar(int id, [Bind("FuncionarioID, Nome, Cpf, Idade, Email, Senha")] Funcionario funcionario)
        {
            if (id != funcionario.FuncionarioID)
            {
                ViewData["msg"] = "Comportamento inesperado";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
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
                    ViewData["msg"] = "Obrigatório informar funcionário";
                    return NotFound();
                }
                Funcionario funcionario = _context.Funcionarios.Find(id);
                _context.Remove(funcionario);
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
                var funcionarios = _context.Funcionarios.Where(u => u.Nome.Contains(searchString));
                ViewData["msg"] = "Existem " + funcionarios.Count() + " resultados para este funcionário.";
                return View("Index", funcionarios);
            }
            else
            {
                ViewData["msg"] = "Nenhum nome foi inserido";
                return RedirectToAction("Index");
            }
        }
    }
}