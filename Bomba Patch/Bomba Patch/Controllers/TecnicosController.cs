using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.Controllers;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            DAO = new TecnicoDAO();
            GeraProximoId = true;
        }

        protected override void PreencheDadosParaView(string Operacao, TecnicosViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
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
        //protected override void ValidaDados(CidadeViewModel model, string operacao)
        //{
        //    base.ValidaDados(model, operacao);
        //    if (string.IsNullOrEmpty(model.Nome))
        //        ModelState.AddModelError("Nome", "Preencha o nome.");
        //}
    }
}