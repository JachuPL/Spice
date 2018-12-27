using AutoMapper;
using Spice.AutoMapper.Profiles.Field;
using Spice.AutoMapper.Profiles.Plant;
using Spice.AutoMapper.Profiles.Species;

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
                cfg.AddProfile<SpeciesProfile>();
            });
        }

        public static IMapper CreateMapper() => _configuration.CreateMapper();
    }
}