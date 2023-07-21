using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace WebApi.Utility.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForLoginDto, User>();
        }
    }
}
