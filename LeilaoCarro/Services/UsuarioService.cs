using FluentValidation;
using LeilaoCarro.Data;
using LeilaoCarro.Data.DTO;
using LeilaoCarro.Data.ViewModels;
using LeilaoCarro.Enums;
using LeilaoCarro.Exceptions;
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

            var cepReplaced = dto.Cep.Replace(".", string.Empty).Replace("-", string.Empty);
            var cepHelper = new CepHelper();
            var cepInfo = await cepHelper.GetCepAsync(cepReplaced);

            if (!string.IsNullOrWhiteSpace(cepInfo.erro) && cepInfo.erro == "true")
                throw new ArgumentException("CEP inválido!");

            EstadoEnum estado = (EstadoEnum)Enum.Parse(typeof(EstadoEnum), cepInfo.uf ?? string.Empty);

            await _context.Database.BeginTransactionAsync();
            try
            {
                var usuario = await _context.Usuario.AddAsync(new Usuario()
                {
                    DataNascimento = dto.DataNascimento,
                    Documento = dto.Documento,
                    Email = dto.Email,
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
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                throw new AppException("Não foi possível criar o usuário", ex, System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
