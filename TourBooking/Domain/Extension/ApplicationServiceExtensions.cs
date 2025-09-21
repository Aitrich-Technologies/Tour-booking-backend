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
<<<<<<< HEAD
using Domain.Services.Destinations.Interface;
using Domain.Services.Destinations;
=======
using Domain.Services.User.Interface;
using Domain.Services.User;
>>>>>>> df226a5ffc4e5b1e939f88eeb15eb131fdeb9629

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
<<<<<<< HEAD
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IDestinationRepository, DestinationRepository>();


=======
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
>>>>>>> df226a5ffc4e5b1e939f88eeb15eb131fdeb9629
            return services;
        }
    }
}
