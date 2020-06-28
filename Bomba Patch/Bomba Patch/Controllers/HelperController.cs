using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BombaPatch.Controllers
{
    public class HelperController
    {
        public static bool VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }

        public static string getUserName(ISession session)
        {
            string logado = session.GetString("nome_usuario");
            return logado;
        }

        public static string getId(ISession session)
        {
            string logado = session.GetString("id_usuario");
            return logado;
        }
    }
}