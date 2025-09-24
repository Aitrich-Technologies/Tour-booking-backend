using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Terms.DTO
{
    public class TermsDto
    {
        public Guid Id { get; set; }

        public Guid TourId { get; set; }

        public string? Terms { get; set; }

    }
}
