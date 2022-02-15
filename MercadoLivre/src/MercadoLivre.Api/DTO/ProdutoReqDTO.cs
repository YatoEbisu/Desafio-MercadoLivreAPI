using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoReqDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public int QntdeDisponivel { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [MaxLength(1000, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public List<CaracteristicaDTO> Caracteristicas { get; set; }
    }
}