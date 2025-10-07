using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services.TourNote.DTO;
using Domain.Services.TourNote.Interface;
using Domain.Models;


namespace Domain.Services.TourNote
{
    public class NoteService : INoteService
    {

        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<NoteDto>> GetNotesByTourIdAsync(Guid tourId)
        {
            var notes = await _noteRepository.GetNotesByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }
        public async Task<NoteDto> AddNotesAsync(NoteDto noteDto)
        {
            var noteEntity = _mapper.Map<Notes>(noteDto);
            var createdNote = await _noteRepository.AddNotesAsync(noteEntity);
            return _mapper.Map<NoteDto>(createdNote);
        }

        public async Task<NoteDto?> GetNotesByIdAsync(Guid id)
        {
            var note = await _noteRepository.GetNotesByIdAsync(id);
            return note == null ? null : _mapper.Map<NoteDto>(note);
        }

        public async Task<NoteDto?> UpdateNotesAsync(NoteDto noteDto)
        {
            var noteEntity = _mapper.Map<Notes>(noteDto);
            var updatedNote = await _noteRepository.UpdateNotesAsync(noteEntity);
            return updatedNote == null ? null : _mapper.Map<NoteDto>(updatedNote);
        }

        public async Task<bool> DeleteNotesAsync(Guid id)
        {
            return await _noteRepository.DeleteNotesAsync(id);
        }
    }
}
