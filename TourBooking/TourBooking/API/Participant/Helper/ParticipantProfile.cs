using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using TourBooking.API.Participant.RequestObjects;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
    {
        // Add
        CreateMap<AddParticipantRequest, ParticipantDto>();

        // Update (prevent null overwrite)
        CreateMap<UpdateParticipantRequest, ParticipantDto>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));

        // DTO ↔ Entity
        CreateMap<ParticipantDto, ParticipantInformation>().ReverseMap();

        // Patch (already correct)
        CreateMap<PatchParticipantRequest, ParticipantDto>()
            .ForAllMembers(opts =>
                opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
