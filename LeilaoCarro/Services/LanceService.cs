using LeilaoCarro.Data;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Helpers;
using LeilaoCarro.Models;
using LeilaoCarro.Validations;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Services
{
    public class LanceService(LeilaoContext context)
    {
        private readonly LeilaoContext _context = context;

        public async Task<IEnumerable<LanceVM>> ObterPorIdCarroAssync(int id)
        {
            return await _context.Lance
                .AsNoTracking()
                .Where(x => x.IdCarro.Equals(id) && !x.Carro.DataDeletado.HasValue)
                .Select(x => x.LanceToVM())
                .ToArrayAsync();
        }

        public async Task<LanceCompletoVM?> ObterAsync(int id)
        {
            return await _context.Lance
                .AsNoTracking()
                .Where(x => x.Id.Equals(id))
                .Include(x => x.Usuario)
                .ThenInclude(x => x.UsuarioEnderecos)
                .ThenInclude(x => x.Estado)
                .Include(x => x.Carro)
                .Select(x => x.LanceCompletoToVM())
                .FirstOrDefaultAsync();
        }

        public async Task<int> AdicionarAsync(NovoLanceDTO dto)
        {
            var validator = new LanceValidator(_context);
            await validator.ValidateAndThrowAppAsync(dto, "Não foi possível adicionar o lance");

            var lance = await _context.Lance.AddAsync(new Lance()
            {
                DataHoraLance = DateTime.Now,
                IdCarro = dto.IdCarro,
                IdUsuario = dto.IdUsuario,
                Valor = dto.Valor
            });
            await _context.SaveChangesAsync();

            return lance.Entity.Id;
        }
    }
}
