using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("ProdutoImagens")]
    public class ProdutoImagem : BaseEntity
    {
        public ProdutoImagem(string nome, string path, string type, Guid produtoId)
        {
            Nome = nome;
            Path = path;
            Type = type;
            ProdutoId = produtoId;
        }
        public string Nome  { get; set; }
        public string Path  { get; set; }
        public string Type  { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}