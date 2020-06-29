using BombaPatch.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class TimeViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int EstadioId { get; set; }
        public int TecnicoId { get; set; }
        public int UsuarioId { get; set; }
        public IFormFile Logo { get; set; }
        public byte[] LogoEmByte { get; set; }

        public string LogoEmBase64
        {
            get
            {
                if (LogoEmByte != null)
                    return Convert.ToBase64String(LogoEmByte);
                else
                    return string.Empty;
            }
        }

        public TimeViewModel() { }
        public TimeViewModel(int Id, string Nome, string sigla, int EstadioId, int TecnicoId, int UsuarioId) {
            this.Id = Id;
            this.Nome = Nome;
            this.Sigla = sigla;
            this.EstadioId = EstadioId;
            this.TecnicoId = TecnicoId;
        }
    }
}
