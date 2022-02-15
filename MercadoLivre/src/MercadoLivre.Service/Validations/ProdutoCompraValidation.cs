using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class ProdutoCompraValidation : AbstractValidator<ProdutoCompra>
    {
        public ProdutoCompraValidation()
        {
            RuleFor(y => y.Quantidade)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}