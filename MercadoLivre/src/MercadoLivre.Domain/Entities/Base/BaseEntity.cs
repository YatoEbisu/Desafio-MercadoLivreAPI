using System;
using System.ComponentModel.DataAnnotations;

namespace MercadoLivre.Domain.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
    }
}