using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class JogadoresDAO : DefaultDAO<JogadoresViewModel>
    {
        protected override SqlParameter[] CriaParametros(JogadoresViewModel model)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome),
                new SqlParameter("idade", model.Idade),
                new SqlParameter("altura", model.Altura),
                new SqlParameter("pe_preferido", model.PePreferidoId),
                new SqlParameter("overall", model.Overall),
                new SqlParameter("nacionalidade", model.NacionalidadeId),
                new SqlParameter("posicao_preferida", model.PosicaoPreferidaId)
            };
        }

        protected override JogadoresViewModel MontaModel(DataRow registro)
        {
            return new JogadoresViewModel(Convert.ToInt32(registro["id"]),
                                            Convert.ToString(registro["nome"]),
                                            Convert.ToInt32(registro["idade"]),
                                            Convert.ToInt32(registro["altura"]),
                                            Convert.ToInt32(registro["overall"]),
                                            Convert.ToInt32(registro["pe_preferido"]),
                                            Convert.ToInt32(registro["nacionalidade"]),
                                            Convert.ToInt32(registro["posicao_preferida"]));
        }

        public List<JogadoresViewModel> ListagemComFiltro(string NomeJogador, int PePref, int Nacionalidade, int Posicao, int OrderBy)
        {

            var tabela = HelperDAO.ExecutaProcSelect("spListagemFiltroJogador", new SqlParameter[]
            {
                new SqlParameter("nome", NomeJogador),
                new SqlParameter("IdPe", PePref),
                new SqlParameter("IdNacionalidade", Nacionalidade),
                new SqlParameter("IdPos", Posicao),
                new SqlParameter("ordem", OrderBy),
            });

            List<JogadoresViewModel> lista = new List<JogadoresViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

        protected override void SetTabela()
        {
            Tabela = "tb_jogadores";
        }
    }
}
