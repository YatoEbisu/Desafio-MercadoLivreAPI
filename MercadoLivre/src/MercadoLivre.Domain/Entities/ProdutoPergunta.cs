using System;

namespace MercadoLivre.Domain.Entities
{
    public class ProdutoPergunta : BaseEntity
    {
        public ProdutoPergunta()
        {
            DataCriacao = DateTime.Now;
        }
        public string Titulo { get; set; }
        public DateTime DataCriacao { get; set; }
        public Guid? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid? ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}