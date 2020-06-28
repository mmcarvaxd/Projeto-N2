using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class UsuariosViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public UsuariosViewModel() { }
        public UsuariosViewModel(int Id, string Nome, string Email, string Senha)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Email = Email;
            this.Senha = Senha;
        }
    }
}
