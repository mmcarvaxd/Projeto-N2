using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class PePreferidoViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public PePreferidoViewModel() { }

        public PePreferidoViewModel(int Id, string Nome)
        {
            this.Id = Id;
            this.Nome = Nome;
        }
    }

}
