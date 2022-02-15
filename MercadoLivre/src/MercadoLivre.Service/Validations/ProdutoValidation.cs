using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation()
        {
            RuleFor(y => y.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.QntdeDisponivel)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .MaximumLength(1000).WithMessage("O campo {PropertyName} deve ter no máximo {MaxLength} caracteres");

            RuleFor(y => y.CategoriaId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");


        }
    }
}