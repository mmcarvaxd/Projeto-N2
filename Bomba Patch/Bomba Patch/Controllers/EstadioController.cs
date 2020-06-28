using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace BombaPatch.Controllers
{
    public class EstadioController : DefaultController<EstadiosViewModel>
    {
        public IActionResult Home()
        {
            return View();
        }

        public EstadioController()
        {
            try
            {
                DAO = new EstadioDAO();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

            GeraProximoId = true;
        }

        protected override void ValidaDados(EstadiosViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (String.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Nome não é valido");
            if(model.Capacidade <= 0)
                ModelState.AddModelError("Capacidade", "Capacidade inválida");
            if (String.IsNullOrEmpty(model.Localizacao))
                ModelState.AddModelError("Localizacao", "Digite uma localização");
        }

    }
}