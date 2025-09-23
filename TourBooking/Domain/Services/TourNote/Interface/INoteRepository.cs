using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
;

namespace Domain.Services.TourNote.Interface
{
    public interface INoteRepository

    {
        Task<Notes> AddNotesAsync(Notes note);
        Task<Notes?> GetNotesByIdAsync(Guid id);
        Task<Notes?> UpdateNotesAsync(Notes note);
        Task<bool> DeleteNotesAsync(Guid id);

    }
}

