using LeilaoCarro.Models;

namespace LeilaoCarro.Data.ViewModels
{
    public record CarroVM
    (
        int Id,
        string Marca,
        string Modelo,
        string? Placa,
        short? Ano,
        DateTime? DataHoraLeiloado,
        decimal LanceInicial,
        DateTime DataHoraCadastrado
    );

    public static class CarroExtensions
    {
        public static CarroVM CarroToVM(this Carro carro)
        {
            return new CarroVM(
                carro.Id,
                carro.Marca,
                carro.Modelo,
                carro.Placa,
                carro.Ano,
                carro.DataHoraLeiloado,
                carro.LanceInicial,
                carro.DataHoraCadastrado
            );
        }
    }
}
