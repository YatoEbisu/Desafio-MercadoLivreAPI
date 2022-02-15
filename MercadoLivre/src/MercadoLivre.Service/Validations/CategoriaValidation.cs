using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(y => y.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }   
    }
}