using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class CategoriaReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [JsonPropertyName("categoriaMaeId")]
        public Guid? CategoriaMaeId { get; set; }
    }
}