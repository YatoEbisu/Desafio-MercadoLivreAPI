using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("ProdutoOpinioes")]
    public class ProdutoOpiniao : BaseEntity
    {
        public int Nota { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public Guid? ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public Guid? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}