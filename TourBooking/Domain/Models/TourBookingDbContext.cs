using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Domain.Models;

public partial class TourBookingDbContext : DbContext
{
    public TourBookingDbContext()
    {
    }

    public TourBookingDbContext(DbContextOptions<TourBookingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<Destination> Destinations { get; set; }

    public virtual DbSet<ParticipantInformation> ParticipantInformations { get; set; }

    public virtual DbSet<TermsAndCondition> TermsAndConditions { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourBookingForm> TourBookingForms { get; set; }

   
    
    
}
