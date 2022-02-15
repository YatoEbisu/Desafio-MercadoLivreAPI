using System;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoService
    {
        Task Insert(Produto obj);
        Task<Produto> Get(Guid Id);
    }
}