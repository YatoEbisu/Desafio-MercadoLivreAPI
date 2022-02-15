using MercadoLivre.Domain.Entities;
using MercadoLivre.Service.Services;
using Microsoft.EntityFrameworkCore;

namespace MercadoLivre.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Caracteristica> Caracteristicas { get; set; }
        public DbSet<ProdutoImagem> ProdutoImagens { get; set; }
        public DbSet<ProdutoOpiniao> ProdutoOpinioes { get; set; }
        public DbSet<ProdutoPergunta> ProdutoPerguntas { get; set; }
        public DbSet<ProdutoCompra> ProdutoCompras { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
    }
}