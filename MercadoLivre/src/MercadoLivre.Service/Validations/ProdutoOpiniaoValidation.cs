using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class ProdutoOpiniaoValidation : AbstractValidator<ProdutoOpiniao>
    {
        public ProdutoOpiniaoValidation()
        {
            RuleFor(y => y.Nota)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .InclusiveBetween(1, 5).WithMessage("O valor da {PropertyName} deve estar entre {From} e {To}");

            RuleFor(y => y.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(500).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLenght}");

            RuleFor(y => y.ProdutoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

        }
    }
}