using LeilaoCarro.Models;

namespace LeilaoCarro.Data.ViewModels
{
    public record EnderecoVM(
        int Id,
        string Cep,
        EstadoVM Estado,
        string Cidade,
        string Logradouro,
        int Numero,
        string? Complemento,
        bool Ativo);

    public static class EnderecoExtensions
    {
        public static EnderecoVM EnderecoToVM(this UsuarioEndereco endereco)
        {
            return new EnderecoVM(
                endereco.Id,
                endereco.Cep,
                endereco.Estado.EstadoToVM(),
                endereco.Cidade,
                endereco.Logradouro,
                endereco.Numero,
                endereco.Complemento,
                endereco.Ativo
            );
        }
    }
}
