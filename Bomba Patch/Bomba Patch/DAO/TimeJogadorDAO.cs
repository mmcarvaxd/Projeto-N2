using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class TimeJogadorDAO
    {
        string Tabela = "tb_time_jogador";

        public virtual List<TimeJogadorViewModel> Listagem(int TimeId)
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("tabela", Tabela),
                 new SqlParameter("filtro", $"id_time = ${TimeId}"),
                 new SqlParameter("Ordem", "1") // 1 é o primeiro campo da tabela,
                                // ou seja, a chave primária
            };
            var tabela = HelperDAO.ExecutaProcSelect("spListagem", p);
            List<TimeJogadorViewModel> lista = new List<TimeJogadorViewModel>();
            foreach (DataRow registro in tabela.Rows)
            {
                lista.Add(MontaModel(registro));
            }
            return lista;
        }

        protected SqlParameter[] CriaParametros(TimeJogadorViewModel model)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("id_jogador", model.JogadorId),
                new SqlParameter("id_posicao", model.PosicaoId),
                new SqlParameter("id_time", model.TimeId),
                new SqlParameter("nmr_camiseta", model.NmrCamiseta)
            };
        }

        protected TimeJogadorViewModel MontaModel(DataRow registro)
        {
            return new TimeJogadorViewModel(Convert.ToInt32(registro["id"]),
                                            Convert.ToInt32(registro["id_jogador"]),
                                            Convert.ToInt32(registro["id_posicao"]),
                                            Convert.ToInt32(registro["id_time"]),
                                            Convert.ToInt32(registro["nmr_camiseta"]));
        }

        public void Insert(TimeJogadorViewModel model)
        {
            HelperDAO.ExecutaProc("spInsert_" + Tabela, CriaParametros(model));
        }

        public void DeleteAll(int TimeId)
        {
            var paramter = new SqlParameter[]
            {
                new SqlParameter("id", TimeId)
            };

            HelperDAO.ExecutaProc("spDeleteTimeJogador", paramter);
        }
    }
}
