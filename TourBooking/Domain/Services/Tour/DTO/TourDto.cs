using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Services.Tour.DTO
{
    public class TourDto
    {
        public Guid Id { get; set; }
        public string TourName { get; set; } = string.Empty;
        public string? TourDescription { get; set; }
        public Guid DestinationId { get; set; }
        public string? DestinationName { get; set; }
        public string? ImageUrl { get; set; }
        public int? NoOfNights { get; set; }
        public int Price { get; set; }
        public DateOnly? DepartureDate { get; set; }
        public DateOnly? ArrivalDate { get; set; }
        public Guid? CustomerId { get; set; }
        [JsonIgnore]
        public Guid ConsultantId { get; set; }
        public string Status { get; set; } 
    }
}
