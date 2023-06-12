using AutoMapper;
using Dot.Net.WebApi.Domain;
using DotNetEnglishP7.Domain;
using DotNetEnglishP7.Identity;

namespace DotNetEnglishP7.Mappers
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<BidList, BidList>();
                cfg.CreateMap<CurvePoint, CurvePoint>();
                cfg.CreateMap<Rating, Rating>();
                cfg.CreateMap<Rule, Rule>();
                cfg.CreateMap<Trade, Trade>();
                cfg.CreateMap<User, AppUser>();
                cfg.CreateMap<AppUser, User>();
                cfg.CreateMap<AppUser, AppUser>();
                cfg.CreateMap<User, User>();
                cfg.CreateMap<RegisterUser, AppUser>();
            });
            return config;
        }
    }
}
