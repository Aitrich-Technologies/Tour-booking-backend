using AutoMapper;
using Domain.Models;
using Domain.Services.Participant.DTO;
using Domain.Services.Participant.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Participant
{
    public class ParticipantService : IParticipantService
    {
        private readonly IParticipantRepository _repository;
        private readonly IMapper _mapper;

        public ParticipantService(IParticipantRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ParticipantDto>> GetParticipantsAsync(Guid bookingId)
        {
            var entities = await _repository.GetParticipantsAsync(bookingId);
            return _mapper.Map<IEnumerable<ParticipantDto>>(entities);
        }

        public async Task<ParticipantDto?> GetParticipantByIdAsync(Guid bookingId, Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<ParticipantDto> AddParticipantAsync(Guid bookingId, ParticipantDto dto)
        {
            var entity = _mapper.Map<ParticipantInformation>(dto);
            entity.LeadId = bookingId;
            await _repository.AddParticipantAsync(entity);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<ParticipantDto?> UpdateParticipantAsync(Guid bookingId, Guid id, ParticipantDto dto)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            entity.LeadId = bookingId;

            await _repository.UpdateParticipantAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<ParticipantDto>(entity);
        }

        public async Task<bool> DeleteParticipantAsync(Guid bookingId, Guid id)
        {
            var entity = await _repository.GetParticipantByIdAsync(bookingId, id);
            if (entity == null) return false;

            await _repository.DeleteParticipantAsync(entity);
            await _repository.SaveChangesAsync();
            return true;
        }
    }

}
