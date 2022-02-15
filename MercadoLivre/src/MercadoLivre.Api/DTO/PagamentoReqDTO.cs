using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class PagamentoReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid ProdutoCompraId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid TransacaoId { get; set; }

        [JsonIgnore]
        public StatusCompra Status { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [JsonPropertyName("Status")]
        public string _Status { get; set; }
    }
}