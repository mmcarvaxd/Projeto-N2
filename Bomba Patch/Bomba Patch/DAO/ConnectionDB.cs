using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.DAO
{
    public class ConnectionDB
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=sql5059.site4now.net;Initial Catalog=DB_A62D73_mmcarvaxd;user id=DB_A62D73_mmcarvaxd_admin; password=12mt3476";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
