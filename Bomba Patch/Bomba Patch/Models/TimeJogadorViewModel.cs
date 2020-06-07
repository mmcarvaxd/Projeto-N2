using BombaPatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class TimeJogadorViewModel : PadraoViewModel
{
        public int JogadorId { get; set; }
        public int TimeId { get; set; }
        public int PosicaoId { get; set; }
        public int NmrCamiseta { get; set; }
    }
}
