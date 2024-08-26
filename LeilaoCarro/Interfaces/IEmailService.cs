using LeilaoCarro.Models;

namespace LeilaoCarro.Interfaces
{
    public interface IEmailService
    {
        Task EnviarGanhadorLeilao(string email, string? nome, Carro carro);
    }
}

