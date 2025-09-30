using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using TourBooking.API.Participant.RequestObjects;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        // Request → DTO
        CreateMap<AddParticipantRequest, ParticipantDto>();
        CreateMap<UpdateParticipantRequest, ParticipantDto>();

        // DTO → Entity
        CreateMap<ParticipantDto, ParticipantInformation>().ReverseMap();

        // For PATCH, map only non-null fields
        CreateMap<PatchParticipantRequest, ParticipantDto>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
