using FluentValidation;
using FluentValidation.Results;
using MercadoLivre.Service.Notifications;
using MercadoLivre.Service.Utils;

namespace MercadoLivre.Service.Services.Base
{
    public class BaseService<T>
    {
        protected readonly INotifier _notifier;
        protected readonly AuthenticatedUser _user;
        public BaseService(INotifier notifier, AuthenticatedUser user)
        {
            _notifier = notifier;
            _user = user;

        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string msg)
        {
            _notifier.Handle(new Notification(msg));
        }
        protected bool Validate(T obj, AbstractValidator<T> validator)
        {
            if (obj == null)
            {
                Notify("Registros não detectados!");
                return false;
            }
            var result = validator.Validate(obj);
            if (result.IsValid) return true;

            Notify(result);
            return false;
        }
    }
}