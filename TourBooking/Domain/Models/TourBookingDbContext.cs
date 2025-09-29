using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class TourBookingDbContext : DbContext
    {
        public TourBookingDbContext(DbContextOptions<TourBookingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuthUser> AuthUsers { get; set; }
        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<ParticipantInformation> ParticipantInformations { get; set; }
        public virtual DbSet<TermsAndCondition> TermsAndConditions { get; set; }
        public virtual DbSet<Tourss> Tours { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<TourBookingForm> TourBookingForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto-generate GUIDs for Tourss
            modelBuilder.Entity<Tourss>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();

            // Relations
            modelBuilder.Entity<Tourss>()
                .HasOne(t => t.Customer)
                .WithMany()
                .HasForeignKey(t => t.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tourss>()
                .HasOne(t => t.Consultant)
                .WithMany()
                .HasForeignKey(t => t.ConsultantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TermsAndCondition>()
                .HasOne(tc => tc.Tour)
                .WithMany(t => t.TermsAndConditions)
                .HasForeignKey(tc => tc.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notes>()
                .HasOne(n => n.Tour)
                .WithMany(t => t.Notes)
                .HasForeignKey(n => n.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TourBookingForm>()
                .HasOne(tbf => tbf.Tour)
                .WithMany(t => t.TourBookingForms)
                .HasForeignKey(tbf => tbf.TourId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParticipantInformation>()
                .HasOne(pi => pi.Lead)
                .WithMany(tbf => tbf.ParticipantInformations)
                .HasForeignKey(pi => pi.LeadId)
                .OnDelete(DeleteBehavior.Restrict);

            // ParticipantInformation Id auto-generation
            modelBuilder.Entity<ParticipantInformation>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}
