using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoPerguntaService
    {
        Task Insert(ProdutoPergunta obj);
        Task<List<ProdutoPergunta>> FindAllByProduto(Guid Id);
    }
}