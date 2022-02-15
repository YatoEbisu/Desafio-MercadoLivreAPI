using System;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Domain.Interfaces
{
    public interface IPagamentoService
    {
        Task Insert(Pagamento obj);
    }
}