﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using biblioteca_core_att.Models;
using Microsoft.AspNetCore.Mvc;

namespace biblioteca_core_att.Controllers
{
    public class LivroController : Controller
    {
        private readonly Context _context;

        public LivroController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var livros = _context.Livros.ToList();

            ViewData["msg"] = "Existem [" + livros.Count + "] livros cadastrados";

            return View(livros);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar([Bind("LivroID, Nome, Autor, Lancamento")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(livro);
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
            return View(livro);
        }

        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                ViewData["msg"] = "Obrigatório informar livro";
                return NotFound();
            }

            var livro = _context.Livros.Find(id);
            if (livro == null)
            {
                ViewData["msg"] = "Livro não encontrado";
                return NotFound();
            }

            return View(livro);
        }

        [HttpPost]
        public IActionResult Editar(int id, [Bind("LivroID, Nome, Autor, Lancamento")] Livro livro)
        {
            if (id != livro.LivroID)
            {
                ViewData["msg"] = "Comportamento inesperado";
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
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
                    ViewData["msg"] = "Obrigatório informar livro";
                    return NotFound();
                }
                Livro livro = _context.Livros.Find(id);
                _context.Remove(livro);
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
                var livros = _context.Livros.Where(u => u.Nome.Contains(searchString));
                ViewData["msg"] = "Existem " + livros.Count() + " resultados para este livro.";
                return View("Index", livros);
            }
            else
            {
                ViewData["msg"] = "Nenhum nome foi inserido";
                return RedirectToAction("SelectAllUsers");
            }
        }
    }
}