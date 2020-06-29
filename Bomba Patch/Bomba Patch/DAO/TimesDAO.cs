using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class TimesDAO : DefaultDAO<TimeViewModel>
    {
        protected override SqlParameter[] CriaParametros(TimeViewModel model)
        {
            object imgByte = model.LogoEmByte;
            if (imgByte == null)
                imgByte = DBNull.Value;

            return new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome),
                new SqlParameter("sigla", model.Sigla),
                new SqlParameter("id_tecnico", model.TecnicoId),
                new SqlParameter("id_usuario", model.UsuarioId),
                new SqlParameter("estadio", model.EstadioId),
                new SqlParameter("logo", imgByte)
            };
        }

        protected override TimeViewModel MontaModel(DataRow registro)
        {
            TimeViewModel time = new TimeViewModel(Convert.ToInt32(registro["id"]),
                                     Convert.ToString(registro["nome"]),
                                     Convert.ToString(registro["sigla"]),
                                     Convert.ToInt32(registro["estadio"]),
                                     Convert.ToInt32(registro["id_tecnico"]),
                                     Convert.ToInt32(registro["id_usuario"]));

            if (registro["logo"] != DBNull.Value)
                time.LogoEmByte = registro["logo"] as byte[];

            return time;
        }

        protected override void SetTabela()
        {
            Tabela = "tb_time";
        }
    }
}
