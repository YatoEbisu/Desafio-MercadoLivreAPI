using System;
using System.ComponentModel.DataAnnotations;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoOpiniaoReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int Nota { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }
    }
}