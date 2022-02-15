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
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(INotifier notifier, ICategoriaRepository categoriaRepository, AuthenticatedUser user) : base(notifier, user)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task Insert(Categoria obj)
        {
            Validate(obj, Activator.CreateInstance<CategoriaValidation>());

            if (await _categoriaRepository.ExistsInDatabaseAsync(y => y.Nome == obj.Nome))
                Notify("Categoria ja cadastrada");

            if(obj.CategoriaMaeId != null)
                if (!await _categoriaRepository.ExistsInDatabaseAsync(y => y.Id == obj.CategoriaMaeId))
                    Notify("Categoria mae inexistente na base de dados");

            if (_notifier.HaveNotification())
                return;

            await _categoriaRepository.Insert(obj);
        }
    }
}