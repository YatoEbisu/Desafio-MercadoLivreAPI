using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("Categorias")]
    public class Categoria : BaseEntity
    {
        public string Nome { get; set; }
        public Guid? CategoriaMaeId { get; set; }
        public Categoria CategoriaMae { get; set; }
    }
}