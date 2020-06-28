using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class UsuarioDAO : DefaultDAO<UsuariosViewModel>
    {
        protected override SqlParameter[] CriaParametros(UsuariosViewModel model)
        {
            return new SqlParameter[]
            {
                new SqlParameter("id", model.Id),
                new SqlParameter("nome", model.Nome),
                new SqlParameter("email", model.Email),
                new SqlParameter("senha", model.Senha)
            };
        }

        protected override UsuariosViewModel MontaModel(DataRow registro)
        {
            UsuariosViewModel usuario = new UsuariosViewModel(Convert.ToInt32(registro["id"]),
                                     Convert.ToString(registro["nome"]),
                                     Convert.ToString(registro["email"]),
                                     Convert.ToString(registro["senha"]));

            return usuario;
        }

        public UsuariosViewModel GetUsuario(string email, string senha)
        {
            SqlParameter[] paramsLogin = new SqlParameter[]
            {
                new SqlParameter("login", email),
                new SqlParameter("senha", senha)
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect("spConsultaUsuario", paramsLogin);
            UsuariosViewModel user = new UsuariosViewModel();

            if(tabela.Rows.Count != 0)
            {
                user = MontaModel(tabela.Rows[0]);
            }

            return user;
        }

        protected override void SetTabela()
        {
            Tabela = "tb_usuario";
        }
    }
}
