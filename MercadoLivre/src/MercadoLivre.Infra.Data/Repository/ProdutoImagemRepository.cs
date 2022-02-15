using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository.Base;

namespace MercadoLivre.Infra.Data.Repository
{
    public class ProdutoImagemRepository : BaseRepository<ProdutoImagem>, IProdutoImagemRepository
    {
        public ProdutoImagemRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task InsertMany(List<ProdutoImagem> objList)
        {
            _context.ProdutoImagens.AddRange(objList);
            await _context.SaveChangesAsync();
        }
    }
}