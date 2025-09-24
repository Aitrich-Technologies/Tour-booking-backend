using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Destinations.DTO
{
    public class DestinationPatchDto
    {
        public string? Name { get; set; }      // nullable
        public string? City { get; set; }      // nullable
        public IFormFile? ImageFile { get; set; }  // nullable
    }
}
