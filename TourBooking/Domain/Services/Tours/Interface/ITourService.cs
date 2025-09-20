//using Domain.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TourBooking.Services.Tours.DTO;

//namespace TourBooking.Services.Tours.Interface
//{

//        public interface ITourService
//        {
//            Task<Tour> CreateTourAsync(TourRegisterDto dto);

//        }
//    }

using Domain.Models;
using Domain.Services.Tours.DTO;
using TourBooking.Services.Tours.DTO;

namespace TourBooking.Services.Tours.Interface
{
    public interface ITourService
    {
        Task<Tour> CreateTourAsync(TourRegisterDto dto);
        Task<Tour?> GetTourByIdAsync(Guid id);
        Task<IEnumerable<Tour>> GetAllToursAsync();
        Task<Tour?> PutTourAsync(Guid id, TourPutDto dto);
        Task<bool> DeleteTourAsync(Guid id);
        Task<Tour?> PatchTourAsync(Guid id, UpdateTourDto dto);


      

    }
}
