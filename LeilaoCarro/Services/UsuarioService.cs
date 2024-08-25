using FluentValidation;
using LeilaoCarro.Data;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Enums;
using LeilaoCarro.Helpers;
using LeilaoCarro.Models;
using LeilaoCarro.Validations;
using Microsoft.EntityFrameworkCore;

namespace LeilaoCarro.Services
{
    public class UsuarioService(LeilaoContext context)
    {
        private readonly LeilaoContext _context = context;

        public async Task<UsuarioVM?> BuscarAsync(int id)
        {
            return await _context.Usuario
                .Include(x => x.UsuarioEnderecos)
                .ThenInclude(x => x.Estado)
                .Where(x => x.Id.Equals(id))
                .Select(x => x.UsuarioToVM())
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddUsuario(NovoUsuarioDTO dto)
        {
            var validador = new UsuarioValidation();
            validador.ValidateAndThrowApp(dto, "Não foi possível criar o usuário");

            var cepReplaced = dto.Cep.Replace(".", "").Replace("-", "");
            var cepHelper = new CepHelper();
            var cepInfo = await cepHelper.GetCepAsync(cepReplaced);

            if (!string.IsNullOrWhiteSpace(cepInfo.erro) && cepInfo.erro == "true")
                throw new ArgumentException("CEP inválido!");

            EstadoEnum estado = (EstadoEnum)Enum.Parse(typeof(EstadoEnum), cepInfo.uf ?? "");

            await _context.Database.BeginTransactionAsync();
            try
            {
                var usuario = await _context.Usuario.AddAsync(new Usuario()
                {
                    DataNascimento = dto.DataNascimento,
                    Documento = dto.Documento,
                    Nome = dto.Nome
                });
                await _context.SaveChangesAsync();

                var endereco = new UsuarioEndereco()
                {
                    IdUsuario = usuario.Entity.Id,
                    Cep = cepReplaced,
                    IdEstado = (byte)estado,
                    Cidade = cepInfo.localidade ?? "",
                    Logradouro = cepInfo.logradouro ?? "",
                    Numero = dto.Numero,
                    Complemento = dto.Complemento,
                    Ativo = true
                };

                await _context.UsuarioEndereco.AddAsync(endereco);

                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return usuario.Entity.Id;

                //return await _context.Usuario.AsNoTracking()
                //    .Where(x => x.Id.Equals(usuario.Entity.Id))
                //    .Include(x => x.UsuarioEnderecos)
                //    .ThenInclude(x => x.Estado)
                //    .Select(x => x.UsuarioToVM())
                //    .FirstAsync();
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                Console.WriteLine(ex.Message + ex.InnerException?.Message);
                throw;
            }
        }
    }
}
