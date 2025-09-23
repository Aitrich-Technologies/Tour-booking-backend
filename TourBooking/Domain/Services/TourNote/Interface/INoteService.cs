using Domain.Services.TourNote.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.TourNote.Interface
{
    public interface INoteService
    {
        Task<NoteDto> AddNotesAsync(NoteDto noteDto);
        Task<NoteDto?> GetNotesByIdAsync(Guid id);
        Task<NoteDto?> UpdateNotesAsync(NoteDto noteDto);
        Task<bool> DeleteNotesAsync(Guid id);
    }
}
