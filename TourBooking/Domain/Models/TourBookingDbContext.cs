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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Tour → Customer (AuthUser)
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Customer)
            .WithMany()
            .HasForeignKey(t => t.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Tour → Consultant (AuthUser)
        modelBuilder.Entity<Tour>()
            .HasOne(t => t.Consultant)
            .WithMany()
            .HasForeignKey(t => t.ConsultantId)
            .OnDelete(DeleteBehavior.Restrict);
       

        // Tour → TermsAndConditions
        modelBuilder.Entity<TermsAndCondition>()
            .HasOne(tc => tc.Tour)
            .WithMany(t => t.TermsAndConditions)
            .HasForeignKey(tc => tc.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        // Tour → Notes
        modelBuilder.Entity<Notes>()
            .HasOne(n => n.Tour)
            .WithMany(t => t.Notes)
            .HasForeignKey(n => n.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        // TourBookingForm → Tour
        modelBuilder.Entity<TourBookingForm>()
            .HasOne(tbf => tbf.Tour)
            .WithMany(t => t.TourBookingForms)
            .HasForeignKey(tbf => tbf.TourId)
            .OnDelete(DeleteBehavior.Restrict);

        // ParticipantInformation → TourBookingForm
        modelBuilder.Entity<ParticipantInformation>()
            .HasOne(pi => pi.Lead)
            .WithMany(tbf => tbf.ParticipantInformations)
            .HasForeignKey(pi => pi.LeadId)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<ParticipantInformation>(entity =>
        {
            entity.HasKey(e => e.Id);

            // Make database generate the Id
            entity.Property(e => e.Id)
                  .HasDefaultValueSql("NEWSEQUENTIALID()");
        });
    }
}


