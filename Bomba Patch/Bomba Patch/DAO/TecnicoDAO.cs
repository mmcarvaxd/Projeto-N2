using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class TecnicoDAO : DefaultDAO<TecnicosViewModel>
    {
        protected override SqlParameter[] CriaParametros(TecnicosViewModel model)
        {
            return new SqlParameter[]{
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome),
                new SqlParameter("idade", model.Idade),
                new SqlParameter("nacionalidade", model.NacionalidadeId),
            };
        }

        protected override TecnicosViewModel MontaModel(DataRow registro)
        {
            return new TecnicosViewModel(Convert.ToInt32(registro["id"]), 
                                         Convert.ToString(registro["nome"]),
                                         Convert.ToInt32(registro["idade"]),
                                         Convert.ToInt32(registro["nacionalidade"]));
        }

        protected override void SetTabela()
        {
            Tabela = "tb_tecnicos";
        }
    }
}
