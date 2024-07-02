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
            await GetPersonById("1");
        }

        private async Task GetPersonById(string id)
        {
            var _client = _httpClientFactory.CreateClient();
            var response = await _client.GetAsync($"https://swapi.dev/api/people/{id}");

            PersonModel person;
            List<FilmModel> movies = new List<FilmModel>();

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                };
                var responsData = await response.Content.ReadAsStringAsync();
                person = JsonSerializer.Deserialize<PersonModel>(responsData, options);

                foreach (var film in person.Films)
                {
                    response = await _client.GetAsync(film);
                    var responseFilm = await response.Content.ReadAsStringAsync();
                    movies.Add(JsonSerializer.Deserialize<FilmModel>(responseFilm, options));
                }
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
