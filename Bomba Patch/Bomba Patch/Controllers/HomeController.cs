using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BombaPatch.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
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
