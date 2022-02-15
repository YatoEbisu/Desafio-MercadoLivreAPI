using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("Pagamentos")]
    public class Pagamento : BaseEntity
    {
        public Pagamento()
        {
            DataProcessamento = DateTime.Now;
        }
        public Guid ProdutoCompraId { get; set; }
        public Guid TransacaoId { get; set; }
        public ProdutoCompra ProdutoCompra { get; set; }
        public DateTime DataProcessamento { get; set; }
        public StatusCompra Status { get; set; }
    }

    public enum StatusCompra
    {
        Sucesso = 1,
        Falha = 2
    }
}