using FluentValidation;
using LeilaoCarro.Data.DTO;

namespace LeilaoCarro.Validations
{
    public class CarroValidator : AbstractValidator<NovoCarroDTO>
    {
        public CarroValidator()
        {
            RuleFor(x => x.Ano)
                .LessThanOrEqualTo((short)DateTime.Now.Year)
                .When(x => x.Ano.HasValue)
                .WithMessage("Ano inválido. Deve ser menor ou igual ao ano atual.");

            RuleFor(x => x.LanceInicial)
                .NotEmpty()
                .WithMessage("Lance inicial inválido. Deve ser maior que zero.");

            RuleFor(x => x.Marca)
                .NotEmpty()
                .WithMessage("Marca obrigatório");

            RuleFor(x => x.Modelo)
                .NotEmpty()
                .WithMessage("Modelo obrigatório");

            RuleFor(x => x.Placa)
                .Matches(@"^([A-Za-z]{3})((-?[\d]{4})|([\d][A-Za-z][\d]{2}))$")
                .When(x => !string.IsNullOrEmpty(x.Placa))
                .WithMessage("Placa de carro inválida. Aceito apenas placas no padrão brasileiras.");
        }
    }
}
