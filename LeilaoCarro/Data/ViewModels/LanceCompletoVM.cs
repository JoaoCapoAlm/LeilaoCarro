using LeilaoCarro.Data.ViewModels;

namespace LeilaoCarro.Models
{
    public record LanceCompletoVM(
        int Id,
        UsuarioVM Usuario,
        CarroVM Carro,
        decimal Valor,
        DateTime DataHoraLance
    );

    public static class LanceCompletoExtensions
    {
        public static LanceCompletoVM LanceCompletoToVM(this Lance lance)
        {
            return new LanceCompletoVM(
                lance.Id,
                lance.Usuario.UsuarioToVM(),
                lance.Carro.CarroToVM(),
                lance.Valor,
                lance.DataHoraLance
            );
        }
    }
}
