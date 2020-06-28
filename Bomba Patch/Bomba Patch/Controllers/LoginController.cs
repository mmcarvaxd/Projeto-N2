using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BombaPatch.Controllers
{
    public class LoginController : Controller
    {
        bool GeraProximoId = true;
        UsuarioDAO DAO = new UsuarioDAO();

        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index", "Home");
        }

        public IActionResult Index()
        {
            if(!HelperController.VerificaUserLogado(HttpContext.Session))
            {
                return View(new UsuariosViewModel());
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }

        public IActionResult FazLogin(UsuariosViewModel usuario)
        {
            UsuariosViewModel user = (DAO as UsuarioDAO).GetUsuario(usuario.Email, usuario.Senha);
            if (user.Id != 0)
            {
                HttpContext.Session.SetString("Logado", "true");
                HttpContext.Session.SetString("nome_usuario", user.Nome);
                HttpContext.Session.SetString("id_usuario", Convert.ToString(user.Id));
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("index");
            }
        }

        public IActionResult Create()
        {
            ViewBag.Operacao = "I";
            UsuariosViewModel model = Activator.CreateInstance(typeof(UsuariosViewModel)) as UsuariosViewModel;
            PreencheDadosParaView("I", model);
            return View("Form", model);
        }

        protected void PreencheDadosParaView(string Operacao, UsuariosViewModel model)
        {
            if (GeraProximoId && Operacao == "I")
                model.Id = DAO.ProximoId();
        }

        public IActionResult Salvar(UsuariosViewModel model, string Operacao)
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
                    {
                        DAO.Update(model);
                        ViewBag.nome_usuario = model.Nome;
                        HttpContext.Session.SetString("nome_usuario", model.Nome);
                    }
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

        protected void ValidaDados(UsuariosViewModel model, string operacao)
        {
            if (operacao == "I" && DAO.Consulta(model.Id) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.Id) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.Id < 0)
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

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!HelperController.VerificaUserLogado(HttpContext.Session))
            {
                ViewBag.Logado = false;
                base.OnActionExecuting(context);
            }
            else
            {
                ViewBag.Logado = true;
                ViewBag.nome_usuario = HelperController.getUserName(HttpContext.Session);
                ViewBag.id_usuario = HelperController.getId(HttpContext.Session);
                base.OnActionExecuting(context);
            }
        }
    }
}
