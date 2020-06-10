using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class EstadioDAO : DefaultDAO<EstadiosViewModel>
    {
        protected override SqlParameter[] CriaParametros(EstadiosViewModel model)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome),
                new SqlParameter("capacidade", model.Capacidade),
                new SqlParameter("localizacao", model.Localizacao)
            };
        }

        protected override EstadiosViewModel MontaModel(DataRow registro)
        {
            return new EstadiosViewModel(Convert.ToInt32(registro["id"]),
                                         Convert.ToString(registro["nome"]),
                                         Convert.ToInt32(registro["capacidade"]),
                                         Convert.ToString(registro["localizacao"]));
        }

        protected override void SetTabela()
        {
            Tabela = "tb_estadio";
        }
    }
}
