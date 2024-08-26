using System.Net.Mail;
using LeilaoCarro.Data;
using LeilaoCarro.Services;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Jobs
{
    public class LeiloarCarroJob(LeilaoContext context, EmailService emailService)
    {
        private readonly LeilaoContext _context = context;
        private readonly EmailService _emailService = emailService;
        public async Task LeiloarCarros()
        {
            var carros = await _context.Carro
                .Include(x => x.Lances)
                .ThenInclude(x => x.Usuario)
                .Where(x => !x.DataDeletado.HasValue
                    && !x.DataHoraLeiloado.HasValue
                    && x.Lances.Any()
                ).ToListAsync();

            foreach (var carro in carros)
            {
                var user = carro.Lances.MaxBy(x => x.Valor)?.Usuario;
                
                if(!string.IsNullOrWhiteSpace(user?.Email))
                    await _emailService.EnviarGanhadorLeilao(user.Email, user.Nome, carro);

                carro.DataHoraLeiloado = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    }
}
