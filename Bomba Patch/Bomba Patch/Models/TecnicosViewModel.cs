using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class TecnicosViewModel : PadraoViewModel
{
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int NacionalidadeId { get; set; }

    }
}
