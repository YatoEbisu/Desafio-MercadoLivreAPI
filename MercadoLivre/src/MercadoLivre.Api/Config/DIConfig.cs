using System.Text;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Infra.Data.Repository;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services;
using MercadoLivre.Service.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MercadoLivre.Api.Config
{
    public static class DIConfig
    {
        public static IServiceCollection addDI(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<ICategoriaService, CategoriaService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IProdutoImagemRepository, ProdutoImagemRepository>();
            services.AddScoped<IProdutoImagemService, ProdutoImagemService>();

            services.AddScoped<IProdutoOpiniaoRepository, ProdutoOpiniaoRepository>();
            services.AddScoped<IProdutoOpiniaoService, ProdutoOpiniaoService>();

            services.AddScoped<IProdutoPerguntaRepository, ProdutoPerguntaRepository>();
            services.AddScoped<IProdutoPerguntaService, ProdutoPerguntaService>();

            services.AddScoped<IProdutoCompraRepository, ProdutoCompraRepository>();
            services.AddScoped<IProdutoCompraService, ProdutoCompraService>();

            services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            services.AddScoped<IPagamentoService, PagamentoService>();

            services.AddScoped<INotifier, Notifier>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticatedUser>();

            return services;
        }
    }
}