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
                if (model.Count == 0)
                {
                    for (int i = 0; i < 11; i++)
                    {
                        model.Add(new TimeJogadorViewModel(timeId));
                    }
                }
                PreencheDadosParaView(model);
                return View("Form", model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool ValidaJogadores(List<TimeJogadorViewModel> model)
        {
            List<string> erros = new List<string>();
            bool hasGoleiro = false;
            bool hasErros = false;
            List<int> nmrscamiseta = new List<int>();
            List<int> jogadores = new List<int>();

            foreach (var item in model)
            {
                jogadores.Add(item.JogadorId);
                nmrscamiseta.Add(item.NmrCamiseta);
            }

            foreach (var item in model)
            {
                if (item.PosicaoId == 1)
                {
                    if (hasGoleiro)
                    {
                        ViewBag.hasDupGoleiro = true;
                        hasErros = true;
                    }
                    else
                    {
                        hasGoleiro = true;
                    }
                }

                if(item.NmrCamiseta <= 0 || item.NmrCamiseta > 1000)
                {
                    ViewBag.hasInvalidNumber = true;
                    hasErros = true;
                }

                int camisetarep = nmrscamiseta.FindAll(nrm => nrm == item.NmrCamiseta).Count;
                if (camisetarep > 1)
                {
                    ViewBag.hasCamisetaRepetida = true;
                    hasErros = true;
                }

                int jogrep = jogadores.FindAll(jog => jog == item.JogadorId).Count;
                if (jogrep > 1)
                {
                    ViewBag.hasJogadorRepetido = true;
                    hasErros = true;
                }
            }

            ViewBag.goleiroMissing = !hasGoleiro;
            if (!hasGoleiro)
                hasErros = true;

            ViewBag.hasErros = hasErros;
            return hasErros;
        }

        public IActionResult Salvar(List<TimeJogadorViewModel> model)
        {
            if(ValidaJogadores(model))
            {
                PreencheDadosParaView(model);
                return View("Form", model);
            }

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

        protected void PreencheDadosParaView(List<TimeJogadorViewModel> model)
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