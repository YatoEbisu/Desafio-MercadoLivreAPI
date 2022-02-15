using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;

namespace MercadoLivre.Service.Services
{
    public class ProdutoPerguntaService : BaseService<ProdutoPergunta>, IProdutoPerguntaService
    {
        private readonly IProdutoPerguntaRepository _produtoPerguntaRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutoPerguntaService(INotifier notifier, AuthenticatedUser user, IProdutoPerguntaRepository produtoPerguntaRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository) : base(notifier, user)
        {
            _produtoPerguntaRepository = produtoPerguntaRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task Insert(ProdutoPergunta obj)
        {
            if (!Validate(obj, Activator.CreateInstance<ProdutoPerguntaValidation>()))
                return;

            var usuarioLogado = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());
            var produto = await _produtoRepository.FindOneAsync(y => y.Id == obj.ProdutoId);

            var usuarioProduto = await _usuarioRepository.FindOneAsync(y => y.Id == produto.UsuarioId);

            var email = new Email(
                usuarioProduto.Login,
                "Nova pergunta",
                obj.Titulo + 
                $"\n /Detail/{produto.Id}");

            email.SendEmail();

            obj.UsuarioId = usuarioLogado.Id;

            await _produtoPerguntaRepository.Insert(obj);
        }

        public async Task<List<ProdutoPergunta>> FindAllByProduto(Guid Id)
        {
            return (await _produtoPerguntaRepository.FindAsync(y => y.ProdutoId == Id)).ToList();
        }
    }
}