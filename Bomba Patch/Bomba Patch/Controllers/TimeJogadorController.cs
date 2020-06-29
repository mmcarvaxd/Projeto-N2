using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BombaPatch.Controllers
{
    public class TimeJogadorController : Controller
    {
        public IActionResult Create(int timeId)
        {
            try
            {
                ViewBag.Operacao = "I";
                TimeJogadorDAO DAO = new TimeJogadorDAO();
                List<TimeJogadorViewModel> model = DAO.Listagem(timeId);
                if(model.Count == 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        model.Add(new TimeJogadorViewModel(timeId));
                    }
                }                
                PreencheDadosParaView("I", model);
                return View("Form", model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IActionResult Salvar(List<TimeJogadorViewModel> model)
        {
            using (var transacao = new System.Transactions.TransactionScope())
            {
                TimeJogadorDAO DAO = new TimeJogadorDAO();
                DAO.DeleteAll(model[0].TimeId);

                foreach (TimeJogadorViewModel elemento in model)
                {
                    DAO.Insert(elemento);
                }
                transacao.Complete();
            }
            return RedirectToAction("Index", "Times");
        }

        protected void PreencheDadosParaView(string Operacao, List<TimeJogadorViewModel> model)
        {
            PosicoesDAO DAOP = new PosicoesDAO();
            List<PosicoesViewModel> posicoes = DAOP.Listagem();

            posicoes.Sort((tecA, tecB) => tecA.Nome.CompareTo(tecB.Nome));

            List<SelectListItem> listaPos = new List<SelectListItem>();
            listaPos.Add(new SelectListItem("Selecione uma Posição...", "0"));
            foreach (PosicoesViewModel posicao in posicoes)
            {
                SelectListItem item = new SelectListItem(posicao.Nome, posicao.Id.ToString());
                listaPos.Add(item);
            }
            ViewBag.Posicoes = listaPos;

            JogadoresDAO DAOJ = new JogadoresDAO();
            List<JogadoresViewModel> jogadores = DAOJ.Listagem();

            jogadores.Sort((tecA, tecB) => tecA.Nome.CompareTo(tecB.Nome));

            List<SelectListItem> listajogador = new List<SelectListItem>();
            listajogador.Add(new SelectListItem("Selecione um Jogador...", "0"));
            foreach (JogadoresViewModel jogador in jogadores)
            {
                SelectListItem item = new SelectListItem(jogador.Nome, jogador.Id.ToString());
                listajogador.Add(item);
            }
            ViewBag.Jogadores = listajogador;
        }
    }
}