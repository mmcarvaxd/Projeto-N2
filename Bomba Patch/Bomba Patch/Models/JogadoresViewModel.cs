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

        public JogadoresViewModel() { }
        public JogadoresViewModel(int Id, string Nome, int Idade, int Altura, int Overall, int PePreferidoId, int NacionalidadeId, int PosicaoPreferidaId)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Idade = Idade;
            this.Altura = Altura;
            this.Overall = Overall;
            this.PePreferidoId = PePreferidoId;
            this.PosicaoPreferidaId = PosicaoPreferidaId;
            this.NacionalidadeId = NacionalidadeId;
        }
    }
}
