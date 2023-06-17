using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class UsuarioViewModel
    {

        public UsuarioViewModel() { }
        public UsuarioViewModel(Entidades.Usuario part)
        {
            Id = part.Id;
            Nome = part.Nome;
            Email = part.Email;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
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
        
        [Required(ErrorMessage = "O campo CPF é obrigatório")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O campo UF é obrigatório")]
        public string UF { get; set; }

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
                UserName = UserName,
                Senha = Senha,
                Nome =  Nome,
                CPF = CPF,
                Email = Email,
                UF = UF
            };
        }
    }
}
