namespace LeilaoCarro.Data.DTO
{
    public record NovoCarroDTO(
        string Marca,
        string Modelo,
        string? Placa,
        short? Ano,
        decimal LanceInicial
    );
}
