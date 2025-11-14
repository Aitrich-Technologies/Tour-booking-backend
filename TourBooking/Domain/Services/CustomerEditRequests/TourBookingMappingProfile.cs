using AutoMapper;
using Domain.Models;
using Domain.Services.CustomerEditRequests.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests
{
    public class TourBookingMappingProfile : Profile
    {
        public TourBookingMappingProfile()
        {
           

            CreateMap<TourBookingEditRequest, TourBookingEditRequestDto>()
                .ForMember(dest => dest.Status,
                           opt => opt.MapFrom(src => src.Status.ToString()));  // ENUM → STRING
            CreateMap<TourBookingForm, TourBookingFormDto>()
    .ForMember(dest => dest.CustomerName,
               opt => opt.MapFrom(src => src.User != null
                   ? src.User.FirstName + " " + src.User.LastName
                   : ""))
    .ForMember(dest => dest.TourName,
               opt => opt.MapFrom(src => src.Tour != null
                   ? src.Tour.TourName   // <-- Change property based on your Tourss model
                   : ""));

        }
    }

}
