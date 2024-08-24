using LeilaoCarro.Models;

namespace LeilaoCarro.Helpers
{
    public class CepHelper
    {
        private const string _url = "https://viacep.com.br/ws/{0}/json/";

        public async Task<CepResponse> GetCepAsync(string cep)
        {
            var apiHelper = new ApiHelper();
            return await apiHelper.GetAsync<CepResponse>(string.Format(_url, cep));
        }
    }
}
