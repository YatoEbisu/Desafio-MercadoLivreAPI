using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoImagemService
    {
        Task<HttpStatusCode> InsertMany(List<ProdutoImagem> objList, Guid ProdutoId);
    }
}