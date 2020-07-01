using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Rewrite.Internal.UrlMatches;

namespace BombaPatch.Controllers
{
    public class JogadoresController : DefaultController<JogadoresViewModel>
    {
        public IActionResult Home()
        {
            return View();
        }

        public override IActionResult Index()
        {
            NacionalidadesDAO NDao = new NacionalidadesDAO();

            List<NacionalidadesViewModel> nacionalidades = NDao.Listagem();
            nacionalidades.Sort((nacionalidadesA, nacionalidadesB) => nacionalidadesA.Pais.CompareTo(nacionalidadesB.Pais));

            List<SelectListItem> listaNacionalidades = new List<SelectListItem>();
            listaNacionalidades.Add(new SelectListItem("Selecione uma Nacionalidade...", "0"));
            foreach (var nacionalidade in nacionalidades)
            {
                SelectListItem item = new SelectListItem(nacionalidade.Pais, nacionalidade.Id.ToString());
                listaNacionalidades.Add(item);
            }
            ViewBag.Nacionalidades = listaNacionalidades;

            PePreferidoDAO PePDao = new PePreferidoDAO();
            List<PePreferidoViewModel> PePreferido = PePDao.Listagem();

            List<SelectListItem> listaPesPreferido = new List<SelectListItem>();
            listaPesPreferido.Add(new SelectListItem("Selecione o pé com maior habilidade...", "0"));
            foreach (PePreferidoViewModel pePreferido in PePreferido)
            {
                SelectListItem item = new SelectListItem(pePreferido.Nome, pePreferido.Id.ToString());
                listaPesPreferido.Add(item);
            }
            ViewBag.PePreferido = listaPesPreferido;

            PosicoesDAO PosPDao = new PosicoesDAO();
            List<PosicoesViewModel> PosPreferido = PosPDao.Listagem();

            List<SelectListItem> listaPosPreferido = new List<SelectListItem>();
            listaPosPreferido.Add(new SelectListItem("Selecione a posição...", "0"));
            foreach (PosicoesViewModel posPreferido in PosPreferido)
            {
                SelectListItem item = new SelectListItem(posPreferido.Nome, posPreferido.Id.ToString());
                listaPosPreferido.Add(item);
            }
            ViewBag.PosicoesPreferidas = listaPosPreferido;

            List<SelectListItem> listaOrdem = new List<SelectListItem>();
            listaOrdem.Add(new SelectListItem("Selecione uma Ordem...", "0"));
            listaOrdem.Add(new SelectListItem("Nome Ascendente", "2"));
            listaOrdem.Add(new SelectListItem("Nome Descendente", "1"));

            ViewBag.OrderBy = listaOrdem;

            return base.Index();
        }

        public JogadoresController()
        {
            try
            {
                DAO = new JogadoresDAO();
                GeraProximoId = true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult Filtra(string NomeJogador, int PePref, int Nacionalidade, int Posicao, int OrderBy)
        {
            JogadoresDAO dao = new JogadoresDAO();
            if (String.IsNullOrEmpty(NomeJogador))
            {
                NomeJogador = "";
            }
            
            List<JogadoresViewModel> lista = dao.ListagemComFiltro(NomeJogador, PePref, Nacionalidade, Posicao, OrderBy);

            return PartialView("pvGrid", lista);
        }

        protected override void PreencheDadosParaView(string Operacao, JogadoresViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            try
            {
                NacionalidadesDAO NDao = new NacionalidadesDAO();

                List<NacionalidadesViewModel> nacionalidades = NDao.Listagem();
                nacionalidades.Sort((nacionalidadesA, nacionalidadesB) => nacionalidadesA.Pais.CompareTo(nacionalidadesB.Pais));

                List<SelectListItem> listaNacionalidades = new List<SelectListItem>();
                listaNacionalidades.Add(new SelectListItem("Selecione uma Nacionalidade...", "0"));
                foreach (var nacionalidade in nacionalidades)
                {
                    SelectListItem item = new SelectListItem(nacionalidade.Pais, nacionalidade.Id.ToString());
                    listaNacionalidades.Add(item);
                }
                ViewBag.Nacionalidades = listaNacionalidades;

                PePreferidoDAO PePDao = new PePreferidoDAO();
                List<PePreferidoViewModel> PePreferido = PePDao.Listagem();

                List<SelectListItem> listaPesPreferido = new List<SelectListItem>();
                listaPesPreferido.Add(new SelectListItem("Selecione o pé com maior habilidade...", "0"));
                foreach (PePreferidoViewModel pePreferido in PePreferido)
                {
                    SelectListItem item = new SelectListItem(pePreferido.Nome, pePreferido.Id.ToString());
                    listaPesPreferido.Add(item);
                }
                ViewBag.PePreferido = listaPesPreferido;

                PosicoesDAO PosPDao = new PosicoesDAO();
                List<PosicoesViewModel> PosPreferido = PosPDao.Listagem();

                List<SelectListItem> listaPosPreferido = new List<SelectListItem>();
                listaPosPreferido.Add(new SelectListItem("Selecione a posição...", "0"));
                foreach (PosicoesViewModel posPreferido in PosPreferido)
                {
                    SelectListItem item = new SelectListItem(posPreferido.Nome, posPreferido.Id.ToString());
                    listaPosPreferido.Add(item);
                }
                ViewBag.PosicoesPreferidas = listaPosPreferido;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        protected override void ValidaDados(JogadoresViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);

            if (String.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Nome inválido");
            if(model.Idade < 15)
                ModelState.AddModelError("Idade", "Idade minima 15 anos");
            if(model.Overall < 0 || model.Overall > 100)
                ModelState.AddModelError("Idade", "Jogadores apenas possuem um Overall entre 0 e 99");
            if (model.Altura < 150 || model.Altura > 210)
                ModelState.AddModelError("Altura", "O jogador deve possuim uma altura entre 150cm e 210cm");
            if (model.NacionalidadeId == 0)
                ModelState.AddModelError("NacionalidadeId", "Escolha uma opção");
            if (model.PePreferidoId == 0)
                ModelState.AddModelError("PePreferidoId", "Escolha uma opção");
            if (model.PosicaoPreferidaId == 0)
                ModelState.AddModelError("PosicaoPreferidaId", "Escolha uma opção");
        }
    }
}