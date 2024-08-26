namespace LeilaoCarro.Data.DTO
{
    public record NovoUsuarioDTO(
        string Nome,
        string Documento,
        string Email,
        DateOnly? DataNascimento,
        string Cep,
        int Numero,
        string? Complemento
    );
}
