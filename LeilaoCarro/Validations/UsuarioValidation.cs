using FluentValidation;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Helpers;

namespace LeilaoCarro.Validations
{
    public class UsuarioValidation : AbstractValidator<NovoUsuarioDTO>
    {
        public UsuarioValidation()
        {
            RuleFor(x => x.Cep)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O CEP é obrigatório")
                .Matches(@"^\d{2}(.?)\d{3}(-?)\d{3}$")
                .WithMessage("CEP inválido");

            RuleFor(x => x.DataNascimento)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("A data de nascimento é obrigatória")
                .Must((dto, data) =>
                {
                    var dataMinima = DateOnly.FromDateTime(DateTime.Now).AddYears(-18);
                    return data <= dataMinima;
                })
                .WithMessage("Idade mínima é de 18 anos");

            RuleFor(x => x.Documento)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O documento é obrigatório")
                .Must((dto, documento) => DocumentoHelper.IsCpfValido(documento))
                .WithMessage("Documento inválido");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("E-mail obrigatório")
                .EmailAddress()
                .WithMessage("E-mail inválido");

            RuleFor(x => x.Nome)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("O nome é obrigatório")
                .MinimumLength(3)
                .WithMessage("O nome deve ter no mínimo 3 caracteres");

            RuleFor(x => x.Numero)
                .NotNull()
                .WithMessage("O número é obrigatório");
        }
    }
}
