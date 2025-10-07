using AutoMapper;
using Domain.Services.TourNote.DTO;
using Domain.Services.TourNote.Interface;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IMapper _mapper;

        public NotesController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpGet("tour/{tourId}")]
        public async Task<IActionResult> GetNotesByTourIdAsync([FromRoute] Guid tourId)
        {
            var notes = await _noteService.GetNotesByTourIdAsync(tourId);
            return Ok(notes);
        }

        [Authorize(Roles = "AGENCY,CONSULTANT")]
        [HttpPost]
        public async Task<IActionResult> NotesAdd([FromBody] AddNoteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Map request object → DTO
            var noteDto = _mapper.Map<NoteDto>(request);
            noteDto.Id = Guid.NewGuid();

            var createdNote = await _noteService.AddNotesAsync(noteDto);
            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }

        [Authorize(Roles = "AGENCY,CONSULTANT,CUSTOMER")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var note = await _noteService.GetNotesByIdAsync(id);
            if (note == null)
                return NotFound();

            return Ok(note);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateNoteRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest("ID in URL and request body do not match.");

            var noteDto = _mapper.Map<NoteDto>(request);

            var updatedNote = await _noteService.UpdateNotesAsync(noteDto);
            if (updatedNote == null)
                return NotFound();

            return Ok(updatedNote);
        }

        [Authorize(Roles = "AGENCY,CUSTOMER,CONSULTANT")]
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
