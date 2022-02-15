using FluentValidation;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class ProdutoPerguntaValidation : AbstractValidator<ProdutoPergunta>
    {
        public ProdutoPerguntaValidation()
        {
            RuleFor(y => y.Titulo)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.ProdutoId)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
        }
    }
}