using AutoMapper;

namespace ShoppingCart.Mapping
{
    public static class MapperConfig
    {
        public static IMapper Config()
        {
           // Mapper.Reset();
           var mapper = new MapperConfiguration(config =>
            {
                config.AddProfile<DomainToModelMappingProfile>();
                config.AddProfile<ModelToDomainMappingProfile>();
            });

            return mapper.CreateMapper();
        }
    }
}