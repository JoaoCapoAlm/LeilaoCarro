using LeilaoCarro.Data;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Helpers;
using LeilaoCarro.Models;
using LeilaoCarro.Validations;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Services
{
    public class CarroService(LeilaoContext context)
    {
        private readonly LeilaoContext _context = context;

        public async Task<CarroVM?> BuscarAsync(int id)
        {
            return await _context.Carro
                .AsNoTracking()
                .Where(x => x.Id.Equals(id) && !x.DataDeletado.HasValue)
                .Select(x => x.CarroToVM())
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CarroVM>> ListarAsync()
        {
            return await _context.Carro
                .AsNoTracking()
                .Where(x => !x.DataDeletado.HasValue)
                .Select(x => x.CarroToVM())
                .ToListAsync();
        }

        public async Task<CarroVM> CriarAssync(NovoCarroDTO dto)
        {
            var validator = new CarroValidator();
            validator.ValidateAndThrowApp(dto, "Não foi possível adicionar o carro.");

            var carro = await _context.Carro.AddAsync(new Carro
            {
                Ano = dto.Ano,
                LanceInicial = dto.LanceInicial,
                Marca = dto.Marca,
                Modelo = dto.Modelo,
                Placa = dto.Placa,
                DataHoraCadastrado = DateTime.Now
            });

            await _context.SaveChangesAsync();

            return carro.Entity.CarroToVM();
        }

        public async Task DeletarAsync(int id)
        {
            await _context.Carro.Where(x => x.Id.Equals(id))
                .ExecuteUpdateAsync(x => x.SetProperty(y => y.DataDeletado, DateTime.Now));
        }
    }
}
