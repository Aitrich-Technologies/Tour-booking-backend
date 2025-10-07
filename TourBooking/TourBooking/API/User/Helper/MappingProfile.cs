using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.User.DTO;
using TourBooking.API.User.RequestObjects;

namespace TourBooking.API.User.Helper
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            // Request ↔ DTO
            CreateMap<AddUserRequest, AddUserDto>().ReverseMap();

            CreateMap<UserResponse, UserResponseDto>().ReverseMap();

            CreateMap<LoginRequest, LoginDto>().ReverseMap();
            // DTO → Model
            CreateMap<AddUserDto, AuthUser>().ReverseMap();

            // Model → DTO
            CreateMap<AuthUser, UserResponseDto>().ReverseMap();

            CreateMap<PatchUserRequest, PatchUserDto>().ReverseMap();

            CreateMap<ForgotPasswordDto,ForgotPasswordRequest>().ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordDto>().ReverseMap();

            // RequestDto -> Entity (string to enum)
            CreateMap<UserResponseDto, AuthUser>()
                .ForMember(dest => dest.Role,
                           opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role, true)));

            // Entity -> ResponseDto (enum to string)
            CreateMap<AuthUser, UserResponseDto>()
                .ForMember(dest => dest.Role,
                           opt => opt.MapFrom(src => src.Role.ToString()));

        }

    }
}
