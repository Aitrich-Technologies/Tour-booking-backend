using AutoMapper;
using Domain.Models;
using Domain.Services.Destinations.DTO;
using Domain.Services.Destinations.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Domain.Services.Destinations
{
    public class DestinationService : IDestinationService
    {
        private readonly IDestinationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DestinationService(
            IDestinationRepository repository,
            IMapper mapper,
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        private string SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return $"{baseUrl}/images/{uniqueFileName}";
        }

        public async Task<IEnumerable<DestinationResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<DestinationResponseDto>>(entities);
        }

        public async Task<DestinationResponseDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<DestinationResponseDto>(entity);
        }

        public async Task<DestinationResponseDto> AddAsync(DestinationDto dto)
        {
            var entity = _mapper.Map<Destination>(dto);

            if (dto.ImageFile != null)
                entity.ImageUrl = SaveFile(dto.ImageFile);

            await _repository.AddAsync(entity);

            return _mapper.Map<DestinationResponseDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, DestinationDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.City = dto.City;

            if (dto.ImageFile != null)
                entity.ImageUrl = SaveFile(dto.ImageFile);

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> PatchAsync(Guid id, DestinationPatchDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            if (!string.IsNullOrEmpty(dto.Name))
                entity.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.City))
                entity.City = dto.City;

            if (dto.ImageFile != null)
                entity.ImageUrl = SaveFile(dto.ImageFile);

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
