using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace MercadoLivre.Infra.Data.Repository
{
    public class PagamentoRepository : BaseRepository<Pagamento>, IPagamentoRepository
    {
        public PagamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Pagamento> FindOneAsync(Expression<Func<Pagamento, bool>> predicate)
        {
            return await _context.Pagamentos.FirstOrDefaultAsync(predicate);
        }
    }
}