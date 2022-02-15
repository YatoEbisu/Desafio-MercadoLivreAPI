using System;
using System.Collections.Generic;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Api.DTO
{
    public class ProdutoResDTO
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int QntdeDisponivel { get; set; }
        public string Descricao { get; set; }
        public double MediaNotas { get; set; }
        public int NrTotalNotas { get; set; }
        public CategoriaResDTO Categoria { get; set; }
        public List<CaracteristicaResDTO> Caracteristicas { get; set; }
        public List<ProdutoImagemResDTO> ProdutoImagens { get; set; }
        public List<ProdutoOpiniaoResDTO> ProdutoOpinioes { get; set; }
        public List<ProdutoPerguntaResDTO> ProdutoPerguntas { get; set; }
    }
}