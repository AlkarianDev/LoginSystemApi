using AllegicWebApi.DTO;
using AutoMapper;

namespace AllegicWebApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegisterDTO, AppUser>();
        }

        
    }
}
