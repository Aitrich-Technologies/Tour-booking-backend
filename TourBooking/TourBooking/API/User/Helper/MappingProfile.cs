using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services.Users.DTO;
using TourBooking.API.User.RequestObjects;

namespace TourBooking.API.User.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // -----------------------------
            // Request ↔ DTO
            // -----------------------------
            CreateMap<AddUserRequest, AddUserDto>().ReverseMap();
            CreateMap<LoginRequest, LoginDto>().ReverseMap();
            CreateMap<PatchUserRequest, PatchUserDto>().ReverseMap();
            CreateMap<ForgotPasswordRequest, ForgotPasswordDto>().ReverseMap();
            CreateMap<ResetPasswordRequest, ResetPasswordDto>().ReverseMap();

            // -----------------------------
            // DTO ↔ Entity
            // -----------------------------
            CreateMap<AddUserDto, AuthUser>().ReverseMap();
            CreateMap<UserResponseDto, AuthUser>()
                .ForMember(dest => dest.Role,
                           opt => opt.MapFrom(src => Enum.Parse<UserRole>(src.Role, true)));

            // -----------------------------
            // Entity ↔ Response
            // -----------------------------
            CreateMap<AuthUser, UserResponseDto>()
                .ForMember(dest => dest.Role,
                           opt => opt.MapFrom(src => src.Role.ToString()));

            CreateMap<UserResponse, UserResponseDto>().ReverseMap();
        }
    }

}
