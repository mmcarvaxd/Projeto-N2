using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class TimeViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int EstadioId { get; set; }
        public int TecnicoId { get; set; }

    }
}
