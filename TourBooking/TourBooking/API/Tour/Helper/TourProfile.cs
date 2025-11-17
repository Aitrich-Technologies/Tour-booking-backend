using AutoMapper;
using Domain.Enums;
using Domain.Services.Tour.DTO;
using TourBooking.API.Tour.RequestObjects;

public class TourProfile : Profile
{
    public TourProfile()
    {
        // ------------------------------
        // Request → DTO
        // ------------------------------
        CreateMap<CreateTourRequest, TourDto>();
        CreateMap<UpdateTourRequest, TourDto>();
        CreateMap<UpdateTourStatusRequest, UpdateTourStatusDto>();


        // ------------------------------
        // Entity → DTO
        // ------------------------------
        CreateMap<Tourss, TourDto>()
            .ForMember(dest => dest.DestinationName,
                opt => opt.MapFrom(src =>
                    src.Destination != null ? src.Destination.Name : null))

            .ForMember(dest => dest.ImageUrl,
                opt => opt.MapFrom(src =>
                    src.Destination != null ? src.Destination.ImageUrl : null))

            .ForMember(dest => dest.CustomerName,
                opt => opt.MapFrom(src =>
                    src.Customer != null
                        ? $"{src.Customer.FirstName} {src.Customer.LastName}"
                        : null))

            .ForMember(dest => dest.ConsultantName,
                opt => opt.MapFrom(src =>
                    src.Consultant != null
                        ? $"{src.Consultant.FirstName} {src.Consultant.LastName}"
                        : null))

            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => src.Status.ToString()));


        // ------------------------------
        // DTO → Entity
        // ------------------------------
        CreateMap<TourDto, Tourss>()
            .ForMember(dest => dest.Destination, opt => opt.Ignore())  // prevent circular mapping
            .ForMember(dest => dest.Customer, opt => opt.Ignore())     // optional: avoid mapping nested navigation props
            .ForMember(dest => dest.Consultant, opt => opt.Ignore())
            .ForMember(dest => dest.Status,
                opt => opt.MapFrom(src => Enum.Parse<TourStatus>(src.Status, true)));
    }
}