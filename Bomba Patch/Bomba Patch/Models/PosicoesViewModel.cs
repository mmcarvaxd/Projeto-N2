using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class PosicoesViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Abreviacao { get; set; }

        public PosicoesViewModel() { }
        public PosicoesViewModel(int Id, string Nome, string Abreviacao)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Abreviacao = Abreviacao;
        }
    }
}
