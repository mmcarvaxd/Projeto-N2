using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class NacionalidadesViewModel : PadraoViewModel
    {
        public string Pais { get; set; }

        public NacionalidadesViewModel(){}
        public NacionalidadesViewModel(int Id, string Pais) {
            this.Pais = Pais;
            this.Id = Id;
        }
    }
}
