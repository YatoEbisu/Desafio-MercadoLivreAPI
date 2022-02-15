using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("Caracteristicas")]
    public class Caracteristica : BaseEntity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Guid? ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}