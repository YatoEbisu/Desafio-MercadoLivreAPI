using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace MercadoLivre.Infra.Data.Repository
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task Insert(Produto obj)
        {
            var transaction = _context.Database.BeginTransaction();

            _context.Produtos.Add(obj);
            _context.Caracteristicas.AddRange(obj.Caracteristicas);
            await _context.SaveChangesAsync();

            transaction.Commit();
        }

        public async Task<Produto> FindOneAsync(Expression<Func<Produto, bool>> predicate)
        {
            return await _context.Produtos.Where(predicate)
                .Include(y => y.ProdutoOpinioes)
                .Include(y => y.ProdutoPerguntas)
                .Include(y => y.ProdutoImagens)
                .Include(y => y.Caracteristicas)
                .Include(y => y.Categoria).ThenInclude(x => x.CategoriaMae)
                .FirstOrDefaultAsync();
        }
    }
}