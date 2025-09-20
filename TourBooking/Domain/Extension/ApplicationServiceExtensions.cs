using Domain.Models;
using Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Services.Participant.Interface;
using Domain.Services.Participant;
using Domain.Services.TourBooking.DTO;
using Domain.Services.TourBooking.Interface;
using Domain.Services.TourBooking;

namespace Domain.Extension
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TourBookingDbContext>(options =>
               options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
            services.AddScoped<IParticipantService, ParticipantService>();
           services. AddScoped<ITourBookingRepository, TourBookingRepository>();
           services.AddScoped<ITourBookingService, TourBookingService>();

            return services;
        }
    }
}
