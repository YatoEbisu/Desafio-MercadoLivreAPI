using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using FluentValidation;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;

namespace MercadoLivre.Service.Services
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public ProdutoService(INotifier notifier, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository, AuthenticatedUser user) : base(notifier, user)
        {
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Insert(Produto obj)
        {
            var usuario = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());
            obj.UsuarioId = usuario.Id;

            var produtos = await _produtoRepository.FindAsync(y => y.UsuarioId == usuario.Id);
            if(produtos.Count() > 0)
            {
                Notify("Usuario ja possui um produto cadastrado"); 
                return;
            }

            Validate(obj, Activator.CreateInstance<ProdutoValidation>());

            if(obj.Valor <= 0)
                Notify("Valor deve ser maior que 0");

            if(obj.Caracteristicas.Count < 3)
                Notify("O produto deve ter pelo menos 3 caracteristicas");

            if(obj.QntdeDisponivel < 0)
                Notify("QntdeDisponivel deve ser maior ou igual a 0");

            if (_notifier.HaveNotification())
                return;

            obj.Caracteristicas.ForEach(y => {
                y.ProdutoId = obj.Id;
            });

            await _produtoRepository.Insert(obj);
        }

        public async Task<Produto> Get(Guid Id)
        {
            var produto = await _produtoRepository.FindOneAsync(y => y.Id == Id);
            double x = 0;
            produto.ProdutoOpinioes.ForEach(y => x += y.Nota);
            produto.MediaNotas = x / produto.ProdutoOpinioes.Count;
            produto.NrTotalNotas = produto.ProdutoOpinioes.Count;
            return produto;
        }
    }
}