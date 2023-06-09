using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo UserName é de preenchimento obrigatório."), MaxLength(255, ErrorMessage = "O tamanho máximo do campo email é de 255 caracteres")]
        public string UserName{ get; set; }
        [Required(ErrorMessage = "O campo Senha é de preenchimento obrigatório.")]
        public string Senha { get; set; }
    }
}
