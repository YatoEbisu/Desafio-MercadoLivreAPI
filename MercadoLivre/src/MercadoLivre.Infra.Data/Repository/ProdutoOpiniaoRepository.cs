using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository.Base;

namespace MercadoLivre.Infra.Data.Repository
{
    public class ProdutoOpiniaoRepository : BaseRepository<ProdutoOpiniao>, IProdutoOpiniaoRepository
    {
        public ProdutoOpiniaoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}