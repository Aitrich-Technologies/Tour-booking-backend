using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Services.Terms.DTO;
using Domain.Services.Terms.Interface;

namespace Domain.Services.Terms
{
   public class TermsService:ITermsAndConditionService
    {
        private readonly ITermsAndConditionRepository _repository;
        private readonly IMapper _mapper;

        public TermsService(ITermsAndConditionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TermsDto>> GetTermsByTourIdAsync(Guid tourId)
        {
            var entities = await _repository.GetTermsByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<TermsDto>>(entities);
        }

        public async Task<TermsDto?> GetTermByIdAsync(Guid id)
        {
            var entity = await _repository.GetTermByIdAsync(id);
            return _mapper.Map<TermsDto>(entity);
        }

        public async Task<TermsDto> AddTermAsync(TermsDto dto)
        {
            var entity = _mapper.Map<TermsAndCondition>(dto);
            await _repository.AddTermAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TermsDto>(entity);
        }

        public async Task<TermsDto?> UpdateTermAsync(Guid id, TermsDto dto)
        {
            var entity = await _repository.GetTermByIdAsync(id);
            if (entity == null) return null;

            // Prevent changing TourId (keep original)
            var originalTourId = entity.TourId;

            _mapper.Map(dto, entity);
            entity.TourId = originalTourId; // restore original TourId

            await _repository.UpdateTermAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TermsDto>(entity);
        }



        public async Task DeleteTermAsync(Guid id)
        {
            var entity = await _repository.GetTermByIdAsync(id);
            if (entity != null)
            {
                await _repository.DeleteTermAsync(entity);
                await _repository.SaveChangesAsync();
            }
        }
    }
}

