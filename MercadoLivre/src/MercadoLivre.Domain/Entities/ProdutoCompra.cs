using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MercadoLivre.Domain.Entities
{
    [Table("ProdutoCompras")]
    public class ProdutoCompra : BaseEntity
    {
        public int Quantidade { get; set; }
        public Guid? UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Guid? ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public GatewayType Gateway { get; set; }
        public StatusType Status { get; set; }
    }

    public enum GatewayType
    {
        Paypal = 1,
        PagSeguro = 2
    }
    public enum StatusType
    {
        Iniciada = 1,
        Completada = 2,
    }
}