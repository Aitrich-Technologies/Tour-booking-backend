using Domain.Helper;
using Domain.Models;
using Domain.Services.Destinations;
using Domain.Services.Destinations.Interface;
using Domain.Services.Participant;
using Domain.Services.Participant.Interface;
using Domain.Services.Terms;
using Domain.Services.Terms.Interface;
using Domain.Services.TourNote;
using Domain.Services.TourNote.Interface;
using Domain.Services.Tours.Interface;
using Domain.Services.User;
using Domain.Services.User.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourBooking.Services.Tours;
using TourBooking.Services.Tours.Interface;

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
           

            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteService, NoteService>();


            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IDestinationRepository, DestinationRepository>();

            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ITourService, TourService>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();





            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();


            services.AddScoped<ITermsAndConditionRepository, TermsRepository>();
            services.AddScoped<ITermsAndConditionService, TermsService>();



            return services;
        }
    }
}
