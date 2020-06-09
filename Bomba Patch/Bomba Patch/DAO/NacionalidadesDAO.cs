using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class NacionalidadesDAO : DefaultDAO<NacionalidadesViewModel>
    {
        protected override SqlParameter[] CriaParametros(NacionalidadesViewModel model)
        {
            return new SqlParameter[] { };
        }

        protected override NacionalidadesViewModel MontaModel(DataRow registro)
        {
            return new NacionalidadesViewModel(Convert.ToInt32(registro["id"]), Convert.ToString(registro["pais"]));
        }

        protected override void SetTabela()
        {
            Tabela = "tb_nacionalidade";
        }
    }
}
