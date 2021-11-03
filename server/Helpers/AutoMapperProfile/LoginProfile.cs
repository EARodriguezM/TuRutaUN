using AutoMapper;

using TuRutaUN.Entities.Login; 
using TuRutaUN.Models.LoginUser;

namespace server.Helpers.AutoMapperProfile
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {    
            CreateMap<AuthenticateRequest, LoginUser>()
                .ForMember(dest => dest.Username, src => src.MapFrom(src => src.Username))
            ;

            CreateMap<LoginUser, TuRutaUN.Entities.Data.User>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(src => src.LoginUserId))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Username+"@unal.edu.co"))
                .ForMember(dest => dest.Status, src => src.MapFrom(src => src.Status))
                .ForMember(dest => dest.LastUpdate, src => src.MapFrom(src => src.LastUpdate))
            ;

            CreateMap<RegisterRequest, LoginUser>()
                .ForMember(dest => dest.Username, src => src.MapFrom(src => src.Username))
                .ForMember(dest => dest.LoginUserId, src => src.MapFrom(src => src.LoginUserId))
            ;
              
        }
    }
}