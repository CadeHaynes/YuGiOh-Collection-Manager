using System.Text.Json;

using YGOCM_BACKEND.DTOs;

namespace YGOCM_BACKEND.Services
{
    /*
     * This class is used to connect to the YGOProDeck API.
     * 
     * Currently this class does the following:
     */
    public class YgoProDeckService
    {
        // Set up a new HttpClient
        readonly HttpClient _httpClient;

        public YgoProDeckService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Pulls a single card from the YGOProDeck API
        public async Task<YgoProDeckCard?> GetCardAsync(string cardName)
        {
            var url = $"https://db.ygoprodeck.com/api/v7/cardinfo.php?name={Uri.EscapeDataString(cardName)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(content);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<YgoProDeckResponse>(content, options);

            return result?.Data.FirstOrDefault();
        }

        // Pulls the first X cards from the YGOProDeck API, if X is not specified (or negative), all cards are returned
        public async Task<IEnumerable<YgoProDeckCard>?> GetXCardsAsync(int count = -1)
        {
            var url = $"https://db.ygoprodeck.com/api/v7/cardinfo.php";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<YgoProDeckResponse>(content, options);

            if (count < 0)
            {
                return result?.Data;
            }
            else
            {
                return result?.Data.Take(count);
            }
        }
    }
}
