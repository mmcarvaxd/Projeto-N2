using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BombaPatch.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BombaPatch.Controllers
{
    public class TecnicosController : DefaultController
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}