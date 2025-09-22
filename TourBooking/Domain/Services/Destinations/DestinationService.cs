using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Destinations.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace Domain.Services.Destinations
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;
        private readonly IMapper _mapper;

        public DestinationService(IDestinationRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DestinationResponseDto>> GetAllAsync()
        {
            var destinations = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DestinationResponseDto>>(destinations);
        }

        public async Task<DestinationResponseDto?> GetByIdAsync(Guid id)
        {
            var dest = await _repository.GetByIdAsync(id);
            return dest == null ? null : _mapper.Map<DestinationResponseDto>(dest);
        }

        public async Task<DestinationResponseDto> AddAsync(DestinationDto dto)
        {
            var dest = new Destination
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                City = dto.City
            };

            if (dto.ImageFile != null)
            {
                using var ms = new MemoryStream();
                await dto.ImageFile.CopyToAsync(ms);
                dest.ImageData = ms.ToArray();
            }

            await _repository.AddAsync(dest);
            return _mapper.Map<DestinationResponseDto>(dest);
        }
        private bool IsValidField(string? value)
        {
            // Only update if value is not null, not empty, and not the Swagger default placeholder "string"
            return !string.IsNullOrWhiteSpace(value) && value != "string";
        }

        public async Task<bool> UpdateAsync(Guid id, DestinationDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            // Update only if valid (not null, not empty, not "string")
            if (IsValidField(dto.Name))
                existing.Name = dto.Name;

            if (IsValidField(dto.City))
                existing.City = dto.City;

            if (dto.ImageFile != null)
            {
                using var ms = new MemoryStream();
                await dto.ImageFile.CopyToAsync(ms);
                existing.ImageData = ms.ToArray();
            }

            return await _repository.UpdateAsync(existing);
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> PatchAsync(Guid id, DestinationPatchDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            if (IsValidField(dto.Name))
                entity.Name = dto.Name;

            if (IsValidField(dto.City))
                entity.City = dto.City;

            if (dto.ImageFile != null)
            {
                using var ms = new MemoryStream();
                await dto.ImageFile.CopyToAsync(ms);
                entity.ImageData = ms.ToArray();
            }

            return await _repository.UpdateAsync(entity);
        }


    }
}
