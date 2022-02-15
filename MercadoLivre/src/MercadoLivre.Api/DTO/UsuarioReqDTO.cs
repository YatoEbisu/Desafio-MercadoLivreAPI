using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoLivre.Api.DTO
{
    public class UsuarioReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail de login em formato inválido.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Senha { get; set; }
    }
}