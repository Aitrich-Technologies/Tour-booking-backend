using Domain.Models;
using Domain.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Domain.Services.Participant.Interface;
using Domain.Services.Participant;
using Domain.Services.TourBooking.Interface;
using Domain.Services.TourBooking;
using Domain.Services.Destinations.Interface;
using Domain.Services.Destinations;
using Domain.Services.Users.Interface;
using Domain.Services.Users;
using Domain.Services.Terms.Interface;
using Domain.Services.Terms;
using Domain.Services.TourNote.Interface;
using Domain.Services.TourNote;
using Domain.Services.Tour.Interface;
using Domain.Services.Tour;
using Domain.Services.Tour.Services;
using Domain.Services.CustomerEditRequests;
using Domain.Services.ParticipantsEditRequests;



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


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();





            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ITourService, TourService>();


            services.AddScoped<ITermsAndConditionRepository, TermsRepository>();
            services.AddScoped<ITermsAndConditionService, TermsService>();
           
            services.AddScoped< ITourBookingEditRequestRepository,TourBookingEditRequestRepository>();
            services.AddScoped<ITourBookingEditRequestService, TourBookingEditRequestService>();
            services.AddScoped<IParticipantEditRequestRepository, ParticipantEditRequestRepository>();

            return services;
        }
    }
}
