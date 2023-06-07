using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public DateTime DataCadastro { get; set; }
    }

    public class UsuarioCadastroViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo usuário é obrigatório")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Senha { get; set; }


        public DateTime DataCadastro { get; set; }

        public Entidades.Usuario ToEntidade()
        {
            return new Entidades.Usuario
            {
                DataCadastro = DataCadastro,
                Senha = Senha,
                UserName = UserName
            };
        }
    }
}
