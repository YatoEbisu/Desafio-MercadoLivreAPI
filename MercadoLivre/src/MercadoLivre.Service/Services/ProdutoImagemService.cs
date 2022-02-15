using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;

namespace MercadoLivre.Service.Services
{
    public class ProdutoImagemService : BaseService<ProdutoImagem>, IProdutoImagemService
    {
        private readonly IProdutoImagemRepository _produtoImagemRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public ProdutoImagemService(INotifier notifier, IProdutoImagemRepository produtoImagemRepository, IProdutoRepository produtoRepository, IUsuarioRepository usuarioRepository, AuthenticatedUser user) : base(notifier, user)
        {
            _produtoImagemRepository = produtoImagemRepository;
            _produtoRepository = produtoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<HttpStatusCode> InsertMany(List<ProdutoImagem> objList, Guid ProdutoId)
        {
            var usuario = await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == _user.Email.ToLower());
            var produto = await _produtoRepository.FindOneAsync(y => y.Id == ProdutoId);

            if (produto.UsuarioId != usuario.Id)
                return HttpStatusCode.Forbidden;

            objList.ForEach(y =>
            {
                Validate(y, Activator.CreateInstance<ProdutoImagemValidation>());
            });


            if (_notifier.HaveNotification())
                return HttpStatusCode.BadRequest;

            await _produtoImagemRepository.InsertMany(objList);

            return HttpStatusCode.OK;
        }
    }
}