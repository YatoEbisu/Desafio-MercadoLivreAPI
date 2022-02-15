using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MercadoLivre.Domain.Entities;
using MercadoLivre.Domain.Interfaces;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Services.Base;
using MercadoLivre.Service.Utils;
using MercadoLivre.Service.Validations;

namespace MercadoLivre.Service.Services
{
    public class UsuarioService : BaseService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, INotifier notifier, AuthenticatedUser user) : base(notifier, user)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task Insert(Usuario obj)
        {
            Validate(obj, Activator.CreateInstance<UsuarioValidation>());

            if(await _usuarioRepository.ExistsInDatabaseAsync(y => y.Login == obj.Login))
                Notify("Login ja cadastrado");

            if (_notifier.HaveNotification())
                return;

            obj.Senha = HashSenha(obj.Senha);

            await _usuarioRepository.Insert(obj);
        }

        public async Task<Usuario> VerifyLogin(string login, string senha)
        {
            return await _usuarioRepository.FindOneAsync(y => y.Login.ToLower() == login.ToLower() && y.Senha == HashSenha(senha));
        }

        private string HashSenha(string senha)
        {
            var hash = new Hash(SHA512.Create());
            return hash.CriptografarSenha(senha);
        }
    }
}