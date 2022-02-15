using System;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;

namespace MercadoLivre.Service.Services
{
    public class ProdutoOpiniaoService : BaseService<ProdutoOpiniao>, IProdutoOpiniaoService
    {
        private readonly IProdutoOpiniaoRepository _produtoOpiniaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutoOpiniaoService(INotifier notifier, AuthenticatedUser user, IProdutoOpiniaoRepository produtoOpiniaoRepository, IUsuarioRepository usuarioRepository) : base(notifier, user)
        {
            _produtoOpiniaoRepository = produtoOpiniaoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Insert(ProdutoOpiniao obj)
        {
            var usuario = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());
            obj.UsuarioId = usuario.Id;

            if (!Validate(obj, Activator.CreateInstance<ProdutoOpiniaoValidation>()))
                return;

            await _produtoOpiniaoRepository.Insert(obj);
        }
    }
}