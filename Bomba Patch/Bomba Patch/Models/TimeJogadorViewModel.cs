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

        public TimeJogadorViewModel() { }
        public TimeJogadorViewModel(int time)
        {
            this.TimeId = time;
        }
        public TimeJogadorViewModel(int Id, int JogadorId, int PosicaoId, int TimeId, int NmrCamiseta)
        {
            this.Id = Id;
            this.JogadorId = JogadorId;
            this.TimeId = TimeId;
            this.PosicaoId = PosicaoId;
            this.NmrCamiseta = NmrCamiseta;
        }
    }
}
