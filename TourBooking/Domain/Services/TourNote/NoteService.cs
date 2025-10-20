using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services.TourNote.DTO;
using Domain.Services.TourNote.Interface;
using Domain.Models;
using Domain.Services.Notification.Interface;
using Domain.Enums;


namespace Domain.Services.TourNote
{
    public class NoteService : INoteService
    {

        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public NoteService(INoteRepository noteRepository, IMapper mapper,INotificationService notificationService)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }
        public async Task<IEnumerable<NoteDto>> GetNotesByTourIdAsync(Guid tourId)
        {
            var notes = await _noteRepository.GetNotesByTourIdAsync(tourId);
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }
        //public async Task<NoteDto> AddNotesAsync(NoteDto noteDto)
        //{
        //    var noteEntity = _mapper.Map<Notes>(noteDto);
        //    var createdNote = await _noteRepository.AddNotesAsync(noteEntity);
        //    return _mapper.Map<NoteDto>(createdNote);
        //}
        //public async Task<NoteDto> AddNotesAsync(NoteDto noteDto)
        //{
        //    var noteEntity = _mapper.Map<Notes>(noteDto);
        //    var createdNote = await _noteRepository.AddNotesAsync(noteEntity);

        //    // ✅ Send notifications if NOT_TO_BE_PRINTED
        //    if (createdNote.Status == NotesStatus.NOT_TO_BE_PRINTED)
        //    {
        //        var message = $"New Note (ID: {createdNote.Id}) is marked as NOT_TO_BE_PRINTED for Tour {createdNote.TourId}.";
        //        await _notificationService.SendNotificationAsync("AGENCY", message);
        //        await _notificationService.SendNotificationAsync("CONSULTANT", message);
        //    }

        //    return _mapper.Map<NoteDto>(createdNote);
        //}
        //public async Task<NoteDto> AddNotesAsync(NoteDto noteDto)
        //{
        //    var noteEntity = _mapper.Map<Notes>(noteDto);
        //    var createdNote = await _noteRepository.AddNotesAsync(noteEntity);

        //    var TourDetails=await _noteRepository.GetNotesByIdAsync(createdNote.Id);

        //    // ✅ Send real-time notifications if NOT_TO_BE_PRINTED
        //    if (createdNote.Status == NotesStatus.NOT_TO_BE_PRINTED)
        //    {
        //        var message = $"A new Note is marked as NOT_TO_BE_PRINTED for Tour {TourDetails.Tour}.";
        //        await _notificationService.SendNotificationAsync("AGENCY", message);
        //        await _notificationService.SendNotificationAsync("CONSULTANT", message);
        //    }

        //    return _mapper.Map<NoteDto>(createdNote);
        //}

        public async Task<NoteDto> AddNotesAsync(NoteDto noteDto)
        {
           
            var noteEntity = _mapper.Map<Notes>(noteDto);

            
            var createdNote = await _noteRepository.AddNotesAsync(noteEntity);

           
            var tourDetails = await _noteRepository.GetNotesByIdAsync(createdNote.Id);

           
            if (createdNote.Status == NotesStatus.NOT_TO_BE_PRINTED)
            {
                string tourName = tourDetails?.Tour?.TourName ?? "Unknown Tour";
                string message = $" A new note has been marked as NOT_TO_BE_PRINTED for Tour: {tourName}.";

                await _notificationService.SendNotificationAsync("AGENCY", message);
                await _notificationService.SendNotificationAsync("CONSULTANT", message);

                Console.WriteLine($"Notification sent for tour '{tourName}' to AGENCY and CONSULTANT roles.");
            }

           
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
