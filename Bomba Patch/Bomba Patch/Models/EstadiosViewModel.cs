using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BombaPatch.Models
{
    public class EstadiosViewModel : PadraoViewModel
    {
        public string Nome { get; set; }
        public int Capacidade { get; set; }
        public string Localizacao { get; set; }

        public EstadiosViewModel() { }
        public EstadiosViewModel(int Id, string Nome, int Capacidade, string Localizacao)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Capacidade = Capacidade;
            this.Localizacao = Localizacao;
        }
        //public IFormFile Imagem { get; set; }
        //public byte[] ImagemEmByte { get; set; }

        //public string ImagemEmBase64
        //{
        //    get
        //    {
        //        if (ImagemEmByte != null)
        //            return Convert.ToBase64String(ImagemEmByte);
        //        else
        //            return string.Empty;
        //    }
        //}
    }
}
