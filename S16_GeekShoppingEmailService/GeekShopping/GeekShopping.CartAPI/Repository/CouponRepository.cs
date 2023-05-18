using GeekShopping.CartAPI.Data.ValueObjects;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;

        public CouponRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponVO> GetCoupon(string couponCode, string token)
        {
            //seta o token no header
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Get das informações no serviço de coupon.
            var response = await _client.GetAsync($"/api/v1/coupon/{couponCode}");
            
            //Pegar o que esta dentro da response pois queremos so o que esta no body
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) 
                return new CouponVO();
            
            //Se for bem sucedido vamos deseralializar o objeto em um couponVO
            return JsonSerializer.Deserialize<CouponVO>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
    }
}
