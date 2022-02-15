using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("Usuarios")]
    public class Usuario : BaseEntity
    {
        public Usuario(string login, string senha)
        {
            Login = login;
            Senha = senha;
            DataCadastro = DateTime.Now;
        }
        public string Login { get; set; }
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }

        public string Role { get; set; }
    }
}