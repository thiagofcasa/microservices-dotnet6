using System.Net.Http.Headers;
using System.Text.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeekShopping.Web.Utils
{
    public static class HttpClientExtensions
    {
        private static MediaTypeHeaderValue contentType = 
            new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException($"Something went wrong calling the API: " +
                                            $"{response.ReasonPhrase}");
            }

            //Converter para Json
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<T>(dataAsString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        }

        public static Task<HttpResponseMessage> PostAsJson<T>
            (this HttpClient httpClient, string url, T data)
        {
            //Serializar os dados
            var dataAsString = JsonSerializer.Serialize(data);

            //montar o conteudo da request.
            var content = new StringContent(dataAsString);

            //setar propriedades 
            content.Headers.ContentType = contentType;

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(
            this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonSerializer.Serialize(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = contentType;
            return httpClient.PutAsync(url, content);
        }
    }
}
