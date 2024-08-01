using AutoMapper;
using RealStateApp.Core.Application.Mappings.Profiles;

namespace RealStateApp.Core.Application.Mappings
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new GeneralProfile());
                // ...
            });

            return config;
        }
    }
}
