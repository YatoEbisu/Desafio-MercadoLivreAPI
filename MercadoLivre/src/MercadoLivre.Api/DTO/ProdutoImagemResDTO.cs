using System;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoImagemResDTO
    {
        public string Nome { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
    }
}