using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using LeilaoCarro.Models;
using LeilaoCarro.Interfaces;
using System.Text;
using System.Web;

namespace LeilaoCarro.Services
{
    public class EmailService(IOptions<EmailSettings> emailSettings) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettings.Value;

        private async Task SendEmail(string assunto, string corpo, MailAddress emailDestino)
        {
            var emailOrigem = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName);

            var smtp = new SmtpClient
            {
                Host = _emailSettings.SmtpServer,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password)
            };

            using (var message = new MailMessage(emailOrigem, emailDestino)
            {
                Subject = assunto,
                Body = corpo,
                BodyEncoding = Encoding.UTF8,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }

        public async Task EnviarGanhadorLeilao(string email, string? nome, Carro carro)
        {
            var corpo = new StringBuilder("<h1>Parabéns, você conseguiu um carro no leilão!</h1>");
            corpo.AppendLine("<p><strong>Dados do carro:</strong></p>");
            corpo.AppendLine($"<p><strong>Marca:</strong> {HttpUtility.HtmlEncode(carro.Marca)}</p>");
            corpo.AppendLine($"<p><strong>Modelo:</strong> {HttpUtility.HtmlEncode(carro.Modelo)}</p>");
            corpo.AppendLine($"<p><strong>Placa:</strong> {HttpUtility.HtmlEncode(carro.Placa)}</p>");
            corpo.AppendLine($"<p><strong>Ano:</strong> {HttpUtility.HtmlEncode(carro.Ano)}</p>");

            var destinatario = new MailAddress(email, nome);
            await SendEmail("Leilão de carros", corpo.ToString(), destinatario);
        }
    }
}
