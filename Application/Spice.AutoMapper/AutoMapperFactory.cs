using AutoMapper;
using Spice.AutoMapper.Profiles.Field;
using Spice.AutoMapper.Profiles.Plant;

namespace Spice.AutoMapper
{
    public static class AutoMapperFactory
    {
        private static readonly MapperConfiguration _configuration;

        static AutoMapperFactory()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PlantProfile>();
                cfg.AddProfile<FieldProfile>();
            });
        }

        public static IMapper CreateMapper() => _configuration.CreateMapper();
    }
}