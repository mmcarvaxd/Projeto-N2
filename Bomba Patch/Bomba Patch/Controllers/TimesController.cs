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
        public override IActionResult Index()
        {
            PreparaComboUsuarios();
            PreparaComboOrder();
            var lista = DAO.Listagem();
            return View(lista);
        }
        protected override void ValidaDados(TimeViewModel model, string operacao)
        {
            base.ValidaDados(model, operacao);
            if (string.IsNullOrEmpty(model.Nome))
                ModelState.AddModelError("Nome", "Preencha o nome.");
            if (string.IsNullOrEmpty(model.Sigla) || model.Sigla.Length != 3)
                ModelState.AddModelError("Sigla", "Sigla Inválida");
            if(model.TecnicoId == 0)
            {
                ModelState.AddModelError("TecnicoId", "Técnico Inválido");
            }
            if (model.EstadioId == 0)
            {
                ModelState.AddModelError("EstadioId", "Estádio Inválido");
            }
            
            if (model.Logo == null && operacao == "I")
                ModelState.AddModelError("Logo", "Escolha uma imagem.");
            if (model.Logo != null && model.Logo.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Logo", "Imagem limitada a 2 mb.");
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

        private void PreparaComboUsuarios()
        {
            UsuarioDAO UDao = new UsuarioDAO();
            List<UsuariosViewModel> usuarios = UDao.Listagem();

            List<SelectListItem> listaUsuarios = new List<SelectListItem>();
            listaUsuarios.Add(new SelectListItem("Selecione um Usuario...", "0"));
            foreach (UsuariosViewModel usuario in usuarios)
            {
                SelectListItem item = new SelectListItem(usuario.Nome, usuario.Id.ToString());
                listaUsuarios.Add(item);
            }

            ViewBag.Usuarios = listaUsuarios;
        }

        private void PreparaComboOrder()
        {
            List<SelectListItem> listaOrdem = new List<SelectListItem>();
            listaOrdem.Add(new SelectListItem("Selecione uma Ordem...", "0"));
            listaOrdem.Add(new SelectListItem("Maior Overall", "1"));
            listaOrdem.Add(new SelectListItem("Menor Overall", "2"));
            listaOrdem.Add(new SelectListItem("Nome Ascendente", "3"));
            listaOrdem.Add(new SelectListItem("Nome Descendente", "4"));

            ViewBag.OrderBy = listaOrdem;
        }

        public IActionResult Filtra(string nomeTime, string siglaTime, int OrderBy, int UsuarioId)
        {
            TimesDAO dao = new TimesDAO();
            if (String.IsNullOrEmpty(nomeTime))
            {
                nomeTime = "";
            }
            if (String.IsNullOrEmpty(siglaTime))
            {
                siglaTime = "";
            }
            List<TimeViewModel> lista = dao.ListagemComFiltro(nomeTime, siglaTime, OrderBy, UsuarioId);

            return PartialView("pvGrid", lista);
        }

        protected override void PreencheDadosParaView(string Operacao, TimeViewModel model)
        {
            base.PreencheDadosParaView(Operacao, model);
            try
            {
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

                model.UsuarioId = Convert.ToInt32(ViewBag.id_usuario);
                ViewBag.Estadios = listaEstadios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TimesController()
        {
            try
            {
                DAO = new TimesDAO();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            GeraProximoId = true;
        }

        public IActionResult VisualizarTime(int id)
        {
            TimesDAO dao = new TimesDAO();
            TimeViewModel time = dao.Consulta(id);
            if (time != null)
            {
                TimeJogadorDAO daotj = new TimeJogadorDAO();
                List<TimeJogadorViewModel> timejogadores = daotj.Listagem(id);
                if (timejogadores.Count == 0)
                {
                    ViewBag.ListJogadores = null;
                }
                else
                {
                    timejogadores.Sort((a, b) => a.PosicaoId.CompareTo(b.PosicaoId));

                    NacionalidadesDAO ndao = new NacionalidadesDAO();
                    List<NacionalidadesViewModel> nacionalidades = ndao.Listagem();

                    PePreferidoDAO pdao = new PePreferidoDAO();
                    List<PePreferidoViewModel> pes = pdao.Listagem();

                    PosicoesDAO posdao = new PosicoesDAO();
                    List<PosicoesViewModel> pos = posdao.Listagem();

                    List<TimeJogadorCompletoViewModel> list = new List<TimeJogadorCompletoViewModel>();

                    JogadoresDAO jdao = new JogadoresDAO();
                    foreach (TimeJogadorViewModel tj in timejogadores)
                    {
                        JogadoresViewModel jogador = jdao.Consulta(tj.JogadorId);
                        list.Add(new TimeJogadorCompletoViewModel(
                                                        tj.Id,
                                                        tj.JogadorId,
                                                        tj.PosicaoId,
                                                        tj.TimeId,
                                                        tj.NmrCamiseta,
                                                        jogador.Nome, jogador.Idade,
                                                        jogador.Altura, jogador.Overall,
                                                        pes.Find(pe => pe.Id == jogador.PePreferidoId).Nome,
                                                        nacionalidades.Find(nac => nac.Id == jogador.NacionalidadeId).Pais,
                                                        pos.Find(posic => posic.Id == tj.PosicaoId).Abreviacao));
                    }
                    ViewBag.ListJogadores = list;
                }

                TecnicoDAO tdao = new TecnicoDAO();
                TecnicosViewModel tecnico = tdao.Consulta(time.TecnicoId);
                ViewBag.tecnico = tecnico;

                UsuarioDAO udao = new UsuarioDAO();
                UsuariosViewModel usuario = udao.Consulta(time.UsuarioId);
                ViewBag.usuario = usuario;

                return View("View", time);
            }
            else
            {
                return RedirectToAction("Index", "Times");
            }
        }
    }
}