using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class JogadoresViewModel : PadraoViewModel
{
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Altura { get; set; }
        public int Overall { get; set; }
        public int PePreferidoId { get; set; }
        public int NacionalidadeId { get; set; }
        public int PosicaoPreferidaId { get; set; }
    }
}
