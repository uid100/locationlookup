using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace locationlookup.Models
{
    public class Location
    {
        public Location()
        {
            if(Zip is null)
            {
                City = "City";
                State = "State";
                Zip = "00000";
            }
        }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state_short")]
        public string Abbrev { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("postal_code")]
        public string Zip { get; set; }
        public Location JsonsSerializer { get; private set; }

        public static async Task<Location> Lookup(string zip)
        {
            Location location = new Location();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
            string requestUrl = $"https://zip.getziptastic.com/v2/us/{zip}";

            response = await client.GetAsync(requestUrl);

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                location.Zip = zip;
                location = JsonSerializer.Deserialize<Location>(result.ToString());
            }
            return location;
        }
    }
}
