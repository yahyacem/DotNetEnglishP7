using AutoMapper;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Models;

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
