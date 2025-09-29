using Domain.Services.TourNote.DTO;
using Domain.Services.TourNote.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Notes.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.Notes
{ 
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotesController : BaseApiController<NotesController>
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

     
        [HttpPost]
        public async Task<IActionResult> NotesAdd([FromBody] AddNoteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var noteDto = new NoteDto
            {
                Id = Guid.NewGuid(),
                TourId = request.TourId,
                TourNotes = request.TourNotes,
                Status = request.Status
            };

            var createdNote = await _noteService.AddNotesAsync(noteDto);
            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var note = await _noteService.GetNotesByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest("ID in URL and request body do not match.");

            var noteDto = new NoteDto
            {
                Id = request.Id,
                TourId = request.TourId,
                TourNotes = request.TourNotes,
                Status = request.Status
            };

            var updatedNote = await _noteService.UpdateNotesAsync(noteDto);
            if (updatedNote == null)
                return NotFound();

            return Ok(updatedNote);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _noteService.DeleteNotesAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
