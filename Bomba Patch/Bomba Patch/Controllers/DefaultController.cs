using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace BombaPatch.Controllers
{
    public class DefaultController : Controller
    {
        public class PadraoController<T> : Controller where T : DefaultViewModel
        {
            protected DefaultDAO<T> DAO { get; set; }
            protected bool GeraProximoId { get; set; }
            public IActionResult Index()
            {
                var lista = DAO.Listagem();
                return View(lista);
            }
            public IActionResult Create(int id)
            {
                ViewBag.Operacao = "I";
                T model = Activator.CreateInstance(typeof(T)) as T;
                PreencheDadosParaView("I", model);
                return View("Form", model);
            }

            protected virtual void PreencheDadosParaView(string Operacao, T model)
            {
                if (GeraProximoId && Operacao == "I")
                    model.Id = DAO.ProximoId();
            }
            public IActionResult Salvar(T model, string Operacao)
            {
                try
                {
                    ValidaDados(model, Operacao);
                    if (ModelState.IsValid == false)
                    {
                        ViewBag.Operacao = Operacao;
                        PreencheDadosParaView(Operacao, model);
                        return View("Form", model);
                    }
                    else
                    {
                        if (Operacao == "I")
                            DAO.Insert(model);
                        else
                            DAO.Update(model);
                        return RedirectToAction("index");
                    }
                }
                catch (Exception erro)
                {
                    ViewBag.Erro = "Ocorreu um erro: " + erro.Message;
                ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View("Form", model);
                }
            }
            protected virtual void ValidaDados(T model, string operacao)
            {
                if (operacao == "I" && DAO.Consulta(model.Id) != null)
                    ModelState.AddModelError("Id", "Código já está em uso!");
                if (operacao == "A" && DAO.Consulta(model.Id) == null)
                    ModelState.AddModelError("Id", "Este registro não existe!");
                if (model.Id <= 0)
                    ModelState.AddModelError("Id", "Id inválido!");
            }
            public IActionResult Edit(int id)
            {
                try
                {
                    ViewBag.Operacao = "A";
                    var model = DAO.Consulta(id);
                    if (model == null)
                        return RedirectToAction("index");
                    else
                    {
                        PreencheDadosParaView("A", model);
                        return View("Form", model);
                    }
                }
                catch
                {
                    return RedirectToAction("index");
                }
            }
            public IActionResult Delete(int id)
            {
                try
                {
                    DAO.Delete(id);
                    return RedirectToAction("index");
                }
                catch
                {
                    return RedirectToAction("index");
                }
            }
        }
    }
}