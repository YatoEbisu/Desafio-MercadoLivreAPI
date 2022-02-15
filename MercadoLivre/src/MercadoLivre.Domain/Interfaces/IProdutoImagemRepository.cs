using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoImagemRepository : IBaseRepository<ProdutoImagem>
    {
        Task InsertMany(List<ProdutoImagem> objList);
    }
}