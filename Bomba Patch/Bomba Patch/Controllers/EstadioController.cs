using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.DAO;
using BombaPatch.Models;
using Microsoft.AspNetCore.Mvc;

namespace BombaPatch.Controllers
{
    public class EstadioController : DefaultController<EstadiosViewModel>
    {
        public IActionResult Home()
        {
            return View();
        }

        public EstadioController()
        {
            DAO = new EstadioDAO();
            GeraProximoId = true;
        }
    }
}