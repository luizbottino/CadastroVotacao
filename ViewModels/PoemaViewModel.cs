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
    public class PoemaExcelViewModel
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Dependencia { get; set; }
        public string Regiao { get; set; }
        public string Titulo { get; set; }
        public string ApresentacaoAfetiva { get; set; }
        public string ReceitaPrato { get; set; }
        public string Foto { get; set; }
        public string Video { get; set; }
    }
    public class PoemaListaViewModel : PoemaCadastroViewModel
    {
        public int TotalVotos { get; set; }
        public bool JaVotou { get; set; }
        public int IdPoema { get; set; }
    }

    public class PoemaRankingViewModel
    {
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public int TotalVotos { get; set; }
    }
}
