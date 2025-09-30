﻿using AutoMapper;
using Domain.Services.TourNote.DTO;
using TourBooking.API.Notes.RequestObjects;

public class NoteProfile : Profile
{
    public NoteProfile()
    {
        CreateMap<AddNoteRequest, NoteDto>();
        CreateMap<UpdateNoteRequest, NoteDto>();
    }
}
