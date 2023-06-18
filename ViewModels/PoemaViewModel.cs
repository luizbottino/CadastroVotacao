using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PoemaCadastroViewModel
    {
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

    }

    public class PoemaListaViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int TotalVotos { get; set; }
        public int IdUsuario { get; set; }
        public bool JaVotou {get; set; }
        public DateTime DataCadastro { get; set; }

    }


}
