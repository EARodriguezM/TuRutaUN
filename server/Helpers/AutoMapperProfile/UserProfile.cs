using AutoMapper;

using TuRutaUN.Entities.Data;
using TuRutaUN.Models.User;

namespace TuRutaUN.Helpers.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {    
            CreateMap<RegisterRequest, User>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(src => src.UserId))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.SecondName, src => src.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.FirstSurname, src => src.MapFrom(src => src.FirstSurname))
                .ForMember(dest => dest.SecondSurname, src => src.MapFrom(src => src.SecondSurname))
                .ForMember(dest => dest.Mobile, src => src.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            ;
            CreateMap<UpdateRequest, User>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(src => src.DataUserId))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.SecondName, src => src.MapFrom(src => src.SecondName))
                .ForMember(dest => dest.FirstSurname, src => src.MapFrom(src => src.FirstSurname))
                .ForMember(dest => dest.SecondSurname, src => src.MapFrom(src => src.SecondSurname))
                .ForMember(dest => dest.Mobile, src => src.MapFrom(src => src.Mobile))
                .ForMember(dest => dest.ProfilePicture, src => src.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.UserTypeId, src => src.MapFrom(src => src.UserTypeId))
            ;
            CreateMap<User, AuthenticateResponse>();      
        }
    }
}