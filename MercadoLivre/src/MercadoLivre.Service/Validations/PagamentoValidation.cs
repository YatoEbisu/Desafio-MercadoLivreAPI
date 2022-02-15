using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class PagamentoValidation : AbstractValidator<Pagamento>
    {
        public PagamentoValidation()
        {
            RuleFor(y => y.ProdutoCompraId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.TransacaoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}