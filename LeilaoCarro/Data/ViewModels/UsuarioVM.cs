using LeilaoCarro.Models;

namespace LeilaoCarro.Data.ViewModels
{
    public record UsuarioVM(
        int Id,
        string Nome,
        string Documento,
        DateOnly? DataNascimento,
        EnderecoVM[] Endereco
    );

    public static class UsuarioExtensions
    {
        public static UsuarioVM UsuarioToVM(this Usuario usuario)
        {
            return new UsuarioVM(
                usuario.Id,
                usuario.Nome,
                usuario.Documento,
                usuario.DataNascimento,
                usuario.UsuarioEnderecos.Select(x => x.EnderecoToVM()).ToArray()
            );
        }
    }
}
