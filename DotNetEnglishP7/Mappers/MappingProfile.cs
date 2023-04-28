using AutoMapper;
using Dot.Net.WebApi.Domain;

namespace DotNetEnglishP7.Mappers
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
               
            });
            return config;
        }
    }
}
