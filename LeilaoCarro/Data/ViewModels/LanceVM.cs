using LeilaoCarro.Models;

namespace LeilaoCarro.Data.ViewModels
{
    public record LanceVM(
        int Id,
        decimal Valor,
        DateTime DataHoraLance
    );

    public static class LanceExtensions
    {
        public static LanceVM LanceToVM(this Lance lance)
        {
            return new LanceVM(
                lance.Id,
                lance.Valor,
                lance.DataHoraLance
            );
        }
    }
}
