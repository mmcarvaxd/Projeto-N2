using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class PePreferidoDAO : DefaultDAO<PePreferidoViewModel>
    {
        protected override SqlParameter[] CriaParametros(PePreferidoViewModel model)
        {
            return new SqlParameter[] { };
        }

        protected override PePreferidoViewModel MontaModel(DataRow registro)
        {
            return new PePreferidoViewModel(Convert.ToInt32(registro["id"]), Convert.ToString(registro["descricao"]));
        }

        protected override void SetTabela()
        {
            Tabela = "tb_pe_preferido";
        }
    }
}
