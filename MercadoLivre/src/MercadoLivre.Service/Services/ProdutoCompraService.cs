using System;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MercadoLivre.Service.Services
{
    public class ProdutoCompraService : BaseService<ProdutoCompra>, IProdutoCompraService
    {
        private readonly IProdutoCompraRepository _produtoCompraRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutoCompraService(INotifier notifier, AuthenticatedUser user, IProdutoRepository produtoRepository, IProdutoCompraRepository produtoCompraRepository, IUsuarioRepository usuarioRepository) : base(notifier, user)
        {
            _produtoCompraRepository = produtoCompraRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<bool> Insert(ProdutoCompra obj)
        {
            if (!Validate(obj, Activator.CreateInstance<ProdutoCompraValidation>()))
                return false;

            var usuarioLogado = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());
            var produto = await _produtoRepository.FindOneAsync(y => y.Id == obj.ProdutoId);
            var usuarioProduto = await _usuarioRepository.FindOneAsync(y => y.Id == produto.UsuarioId);

            if (obj.Quantidade > produto.QntdeDisponivel)
            {
                Notify("Quantidade indisponivel em estoque");
                return false;
            }

            obj.UsuarioId = usuarioLogado.Id;
            obj.Status = StatusType.Iniciada;
            produto.QntdeDisponivel -= obj.Quantidade;

            var email = new Email(
                usuarioProduto.Login,
                "Nova compra",
                 "Um usuario deseja comprar seu produto");

            email.SendEmail();

            await _produtoCompraRepository.Insert(obj);
            await _produtoRepository.Update(produto);

            return true;
        }

    }
}