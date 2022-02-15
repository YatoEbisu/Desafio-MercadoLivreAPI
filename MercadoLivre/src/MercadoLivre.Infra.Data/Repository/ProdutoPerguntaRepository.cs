using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Context;
using MercadoLivre.Infra.Data.Repository.Base;

namespace MercadoLivre.Infra.Data.Repository
{
    public class ProdutoPerguntaRepository : BaseRepository<ProdutoPergunta>, IProdutoPerguntaRepository
    {
        public ProdutoPerguntaRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}