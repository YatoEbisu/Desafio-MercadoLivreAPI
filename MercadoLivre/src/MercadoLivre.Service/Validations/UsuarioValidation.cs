using FluentValidation;
using FluentValidation.Validators;
using MercadoLivre.Domain.Entities;

namespace MercadoLivre.Service.Validations
{
    public class UsuarioValidation : AbstractValidator<Usuario>
    {
        public UsuarioValidation()
        {
            RuleFor(y => y.DataCadastro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

            RuleFor(y => y.Login)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("E-mail inválido");

            RuleFor(y => y.Senha)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(6, 12).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
        }
    }
}