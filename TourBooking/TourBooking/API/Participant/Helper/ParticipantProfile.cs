using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using TourBooking.API.Participant.RequestObjects;

public class ParticipantProfile : Profile
{
    public ParticipantProfile()
        {
            // Create
            CreateMap<AddParticipantRequest, ParticipantDto>();

            // Update – map only non-null values
            CreateMap<UpdateParticipantRequest, ParticipantDto>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // Patch – same non-null logic
            CreateMap<PatchParticipantRequest, ParticipantDto>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));

            // Entity → DTO
            CreateMap<ParticipantInformation, ParticipantDto>();

            // DTO → Entity
            CreateMap<ParticipantDto, ParticipantInformation>();
        }
    }

