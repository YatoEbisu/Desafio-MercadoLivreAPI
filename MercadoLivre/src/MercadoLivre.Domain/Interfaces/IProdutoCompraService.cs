using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IProdutoCompraService
    {
        Task<bool> Insert(ProdutoCompra obj);
    }
}
