using System;
using System.ComponentModel.DataAnnotations;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoCompraReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [Range(1, Double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que 0")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Gateway { get; set; }
    }
}