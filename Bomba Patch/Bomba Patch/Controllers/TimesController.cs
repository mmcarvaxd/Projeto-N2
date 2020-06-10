using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BombaPatch.Controllers
{
    public class TimesController : DefaultController<TimeViewModel>
    {
        public IActionResult Home()
        {
            return View();
        }

        protected override void ValidaDados(TimeViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
            //Imagem será obrigatio apenas na inclusão.
            //Na alteração iremos considerar a que já estava salva.
            if (model.Logo == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (model.Logo != null && model.Logo.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && model.Logo == null)
                {
                    TimeViewModel cid = DAO.Consulta(model.Id);
                    model.LogoEmByte = cid.LogoEmByte;
                }
                else
                {
                    model.LogoEmByte = ConvertImageToByte(model.Logo);
                }
            }
        }

        /// <summary>
        /// Converte a imagem recebida no form em um vetor de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else

                return null;
        }

        protected override void PreencheDadosParaView(string Operacao, TimeViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);

            TecnicoDAO TDao = new TecnicoDAO();

            List<TecnicosViewModel> tecnicos = TDao.Listagem();
            tecnicos.Sort((tecA, tecB) => tecA.Nome.CompareTo(tecB.Nome));

            List<SelectListItem> listaTecnicos = new List<SelectListItem>();
            listaTecnicos.Add(new SelectListItem("Selecione um Tecnico...", "0"));
            foreach (TecnicosViewModel tecnico in tecnicos)
            {
                SelectListItem item = new SelectListItem(tecnico.Nome, tecnico.Id.ToString());
                listaTecnicos.Add(item);
            }
            ViewBag.Tecnicos = listaTecnicos;

            EstadioDAO EDao = new EstadioDAO();
            List<EstadiosViewModel> estadios = EDao.Listagem();

            List<SelectListItem> listaEstadios = new List<SelectListItem>();
            listaEstadios.Add(new SelectListItem("Selecione um Estadio...", "0"));
            foreach (EstadiosViewModel estadio in estadios)
            {
                SelectListItem item = new SelectListItem(estadio.Nome, estadio.Id.ToString());
                listaEstadios.Add(item);
            }
            ViewBag.Estadios = listaEstadios;
        }

        public TimesController()
        {
            DAO = new TimesDAO();
            GeraProximoId = true;
        }
    }
}