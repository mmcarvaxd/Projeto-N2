using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class PosicoesDAO : DefaultDAO<PosicoesViewModel>
    {
        protected override SqlParameter[] CriaParametros(PosicoesViewModel model)
        {
            return new SqlParameter[] { };
        }

        protected override PosicoesViewModel MontaModel(DataRow registro)
        {
            return new PosicoesViewModel(Convert.ToInt32(registro["id"]),
                                         Convert.ToString(registro["descricao"]),
                                         Convert.ToString(registro["abreviacao"]));
        }

        protected override void SetTabela()
        {
            Tabela = "tb_posicoes";
        }
    }
}
