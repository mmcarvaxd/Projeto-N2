using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bomba_Patch.Controllers
{
    public class TecnicosController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}