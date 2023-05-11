using ACMEIndustries.Models;
using AutoMapper;
using WebAPI.Models;

namespace WebAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
