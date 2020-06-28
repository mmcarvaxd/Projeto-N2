using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BombaPatch.Controllers;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;

namespace BombaPatch.Controllers
{
    public class TecnicosController : DefaultController<TecnicosViewModel>
    {
        public IActionResult Home()
        {
            return View();
        }

        public TecnicosController()
        {
            try
            {
                DAO = new TecnicoDAO();
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            GeraProximoId = true;
        }

        protected override void PreencheDadosParaView(string Operacao, TecnicosViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            try
            {
                NacionalidadesDAO dao = new NacionalidadesDAO();
                List<NacionalidadesViewModel> nacionalidades = dao.Listagem();
                nacionalidades.Sort((nacionalidadesA, nacionalidadesB) => nacionalidadesA.Pais.CompareTo(nacionalidadesB.Pais));

                List<SelectListItem> listaNacionalidades = new List<SelectListItem>();
                listaNacionalidades.Add(new SelectListItem("Selecione uma Nacionalidade...", "0"));
                foreach (var nacionalidade in nacionalidades)
                {
                    SelectListItem item = new SelectListItem(nacionalidade.Pais, nacionalidade.Id.ToString());
                    listaNacionalidades.Add(item);
                }
                ViewBag.Nacionalidades = listaNacionalidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        protected override void ValidaDados(TecnicosViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (model.Idade < 18)
                ModelState.AddModelError("Idade", "Idade minima 18 anos");
            if (model.NacionalidadeId == 0)
                ModelState.AddModelError("NacionalidadeId", "Escolha uma opção");
            if (String.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Nome inválido");
        }
    }
}