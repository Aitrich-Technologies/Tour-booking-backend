using Domain.Services.TourNote.DTO;
using Domain.Services.TourNote.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourBooking.API.Notes.RequestObjects;
using TourBooking.Controllers;

namespace TourBooking.API.Notes
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseApiController<NotesController>
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // POST: api/Notes
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
                Status = request.Status.ToString()
            };

            var createdNote = await _noteService.AddNotesAsync(noteDto);
            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }

        // GET: api/Notes/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var note = await _noteService.GetNotesByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        // PUT: api/Notes/{id}
        [HttpPut("{id:guid}")]
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
                Status = request.Status.ToString()
            };

            var updatedNote = await _noteService.UpdateNotesAsync(noteDto);
            if (updatedNote == null)
                return NotFound();

            return Ok(updatedNote);
        }

        // DELETE: api/Notes/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _noteService.DeleteNotesAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
