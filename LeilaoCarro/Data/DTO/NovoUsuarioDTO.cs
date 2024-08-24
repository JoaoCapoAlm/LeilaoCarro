namespace LeilaoCarro.Data.DTO
{
    public record NovoUsuarioDTO(
        string Nome,
        string Documento,
        DateOnly? DataNascimento,
        string Cep,
        int Numero,
        string? Complemento
    );
}
