using LeilaoCarro.Models;

namespace LeilaoCarro.Data.ViewModels
{
    public record EstadoVM(
        byte Id,
        string Nome,
        string Sigla
    );

    public static class EstadoExtensions
    {
        public static EstadoVM EstadoToVM(this Estado estado)
        {
            return new EstadoVM(
                estado.Id,
                estado.Nome,
                estado.Sigla
            );
        }
    }
}
