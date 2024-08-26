using System.Threading;
using FluentValidation;
using LeilaoCarro.Data;
using LeilaoCarro.Data.DTO;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Validations
{
    public class LanceValidator : AbstractValidator<NovoLanceDTO>
    {
        public LanceValidator(LeilaoContext context)
        {
            bool carroInapto = false;
            RuleFor(x => x.IdCarro)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ID do carro é obrigatório")
                .CustomAsync(async (id, validationContext, cancellationToken) =>
                {
                    var carro = await context.Carro
                        .AsNoTracking()
                        .Where(x => x.Id.Equals(id) && !x.DataDeletado.HasValue)
                        .FirstOrDefaultAsync(cancellationToken);
                    
                    if(carro is null)
                    {
                        validationContext.AddFailure("Carro não encontrado");
                        carroInapto = true;
                        return;
                    }

                    if (carro.DataHoraLeiloado.HasValue)
                    {
                        validationContext.AddFailure("Carro já leiloado");
                        carroInapto = true;
                    }
                });

            RuleFor(x => x.IdUsuario)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("ID do usuário é obrigatório")
                .MustAsync(async (id, cancellationToken) =>
                {
                    return await context.Usuario
                        .AsNoTracking()
                        .Where(y => y.Id.Equals(id))
                        .AnyAsync(cancellationToken);
                }).WithMessage("Unuário não encontrado");

            RuleFor(x => x.Valor)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Valor do lance é obrigatório")
                .MustAsync(async (dto, valor, cancellationToken) =>
                {
                    var lanceList = await context.Lance
                        .AsNoTracking()
                        .Where(y => y.IdCarro.Equals(dto.IdCarro))
                        .Select(x => x.Valor)
                        .ToListAsync(cancellationToken);

                    var maiorLance = lanceList.DefaultIfEmpty(0).Max();

                    if(maiorLance > 0)
                        return valor > maiorLance;

                    var lanceInicial = await context.Carro
                        .AsNoTracking()
                        .Where(y => y.Id.Equals(dto.IdCarro))
                        .Select(y => y.LanceInicial)
                        .FirstOrDefaultAsync(cancellationToken);

                    return valor >= lanceInicial;
                }).When(x => !carroInapto)
                .WithMessage("Já existe um lance maior");
        }
    }
}
