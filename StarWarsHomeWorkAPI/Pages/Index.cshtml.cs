using System.Linq.Expressions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarWarsHomeWorkAPI.Models;

namespace StarWarsHomeWorkAPI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            await GetPersonById();
        }

        private async Task GetPersonById()
        {
            var _client = _httpClientFactory.CreateClient();
            var response = await _client.GetAsync("http://swapi.dev/api/people/1");

            List<PersonModel> people;

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var responsData = await response.Content.ReadAsStringAsync();
                people = JsonSerializer.Deserialize<List<PersonModel>>(responsData, options);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
