using System;
using System.Collections.Generic;
using System.Text;

namespace Servicos
{
    public class Constantes
    {
        public Constantes() { }
        public string SecretKey { get; set; }
        public string UrlApi { get; set; }
        public string UrlSite { get; set; }
        public string StorageConn { get; set; }
        
        public enum StatusValidacaoCadastro
        {
            Cadastrado = 1,
            SemCadastro = 3
        }
    }
}
