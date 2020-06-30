using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class TimeJogadorCompletoViewModel
    {
        public int Id { get; set; }
        public int JogadorId { get; set; }
        public int TimeId { get; set; }
        public int PosicaoId { get; set; }
        public int NmrCamiseta { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Altura { get; set; }
        public int Overall { get; set; }
        public string PePreferido { get; set; }
        public string Nacionalidade { get; set; }
        public string posicao { get; set; }

        public TimeJogadorCompletoViewModel() { }

        public TimeJogadorCompletoViewModel(int Id, int JogadorId, int PosicaoId, int TimeId, int NmrCamiseta, string Nome, int Idade, int Altura, int Overall, string PePreferido, string Nacionalidade, string posicao)
        {
            this.Id = Id;
            this.JogadorId = JogadorId;
            this.TimeId = TimeId;
            this.PosicaoId = PosicaoId;
            this.NmrCamiseta = NmrCamiseta;
            this.Nome = Nome;
            this.Idade = Idade;
            this.Altura = Altura;
            this.Overall = Overall;
            this.PePreferido = PePreferido;
            this.Nacionalidade = Nacionalidade;
            this.posicao = posicao;
        }
    }
}
