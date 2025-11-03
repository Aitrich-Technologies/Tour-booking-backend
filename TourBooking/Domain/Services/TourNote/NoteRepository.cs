using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Services.TourNote.Interface;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace Domain.Services.TourNote
{
    public class NoteRepository: INoteRepository
    {
        private readonly TourBookingDbContext _context;
       string role="";

        public NoteRepository(TourBookingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notes>> GetNotesByTourIdAsync(Guid tourId, string role)
        {
           

            if (role == "CUSTOMER")
            {
                return await _context.Notes
                .Where(n => n.TourId == tourId).Where(n=>n.Status==0)
                .ToListAsync();
            }

            return await _context.Notes
                .Where(n => n.TourId == tourId)
                .ToListAsync();
        }
        public async Task<Notes> AddNotesAsync(Notes note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<Notes?> GetNotesByIdAsync(Guid id)
        {
            return await _context.Notes
            .Include(n => n.Tour)
            .FirstOrDefaultAsync(n => n.Id == id);

        }

        public async Task<Notes> UpdateNotesAsync(Notes note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<bool> DeleteNotesAsync(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
