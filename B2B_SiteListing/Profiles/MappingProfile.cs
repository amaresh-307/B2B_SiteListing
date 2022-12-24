using AutoMapper;
using B2B_SiteListing.Data.Entities;
using B2B_SiteListing.Service.Models;

namespace B2B_SiteListing.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LogInDetailsViewModel, LogInDetails>().ReverseMap();
        }
    }
}
