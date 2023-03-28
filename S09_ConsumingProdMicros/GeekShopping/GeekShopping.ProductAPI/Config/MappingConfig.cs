using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfoig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            });
            return mappingConfoig;
        }
    }
}
