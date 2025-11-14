using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.CustomerEditRequests.DTO
{
    public class TourBookingFormDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string TourName { get; set; }
     
    }

}
