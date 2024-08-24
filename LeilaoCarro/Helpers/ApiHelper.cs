using System.Text.Json;

namespace LeilaoCarro.Helpers
{
    public class ApiHelper()
    {
        private readonly HttpClient _httpClient = new();

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync() ?? "";
                T result = JsonSerializer.Deserialize<T>(json)
                    ?? throw new ApplicationException("Retorno inesperado!");

                return result;
            }
            catch (Exception ex)
            {
                // Aqui você pode tratar a exceção de forma mais detalhada, se necessário.
                throw new ApplicationException($"Erro ao chamar a API: {ex.Message}", ex);
            }
        }
    }
}
