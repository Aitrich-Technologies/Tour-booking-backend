using AutoMapper;
using Domain.Enum;
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
            CreateMap<UserResponse,UserResponseDto>().ReverseMap();

            CreateMap<LoginRequest, LoginDto>().ReverseMap();
            // DTO → Model
            CreateMap<AddUserDto, AuthUser>().ReverseMap();

            // Model → DTO
            CreateMap<AuthUser, UserResponseDto>().ReverseMap();

            CreateMap<PatchUserRequest, PatchUserDto>().ReverseMap();



        }

    }
}
