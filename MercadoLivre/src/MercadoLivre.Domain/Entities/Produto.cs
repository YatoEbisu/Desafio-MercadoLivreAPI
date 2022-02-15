using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("Produtos")]
    public class Produto : BaseEntity

    {
        public Produto(string nome, decimal valor, int qntdeDisponivel, string descricao, Guid categoriaId, List<Caracteristica> caracteristicas)
        {
            Nome = nome;
            Valor = valor;
            QntdeDisponivel = qntdeDisponivel;
            Descricao = descricao;
            DataCadastro = DateTime.Now;
            CategoriaId = categoriaId;
            Caracteristicas = caracteristicas;
        }

        public Produto()
        {
        }

        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int QntdeDisponivel { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public List<Caracteristica> Caracteristicas { get; set; }
        public List<ProdutoImagem> ProdutoImagens { get; set; }
        public List<ProdutoOpiniao> ProdutoOpinioes { get; set; }
        public List<ProdutoPergunta> ProdutoPerguntas { get; set; }

        [NotMapped]
        public double MediaNotas { get; set; } 
        
        [NotMapped]
        public int NrTotalNotas { get; set; }

    }
}