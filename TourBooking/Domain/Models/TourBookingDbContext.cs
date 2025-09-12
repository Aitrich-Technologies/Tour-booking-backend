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

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=AFRA;Initial Catalog=TourBookingDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuthUser__3214EC07AE09EFE5");

            entity.ToTable("AuthUser");

            entity.HasIndex(e => e.UserName, "UQ__AuthUser__C9F2845666AE3E67").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.EmailId)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TelephoneNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Destination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Destinat__3214EC07454BA130");

            entity.ToTable("Destination");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ParticipantInformation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Particip__3214EC072D423132");

            entity.ToTable("ParticipantInformation");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Citizenship)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Lead).WithMany(p => p.ParticipantInformations)
                .HasForeignKey(d => d.LeadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Participant_Booking");
        });

        modelBuilder.Entity<TermsAndCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TermsAnd__3214EC0783330847");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Terms)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.Tour).WithMany(p => p.TermsAndConditions)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Terms_Tour");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tour__3214EC07B46AE151");

            entity.ToTable("Tour");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Consultant)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Customer)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.NoOfNights)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TourDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.TourName)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Destination).WithMany(p => p.Tours)
                .HasForeignKey(d => d.DestinationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tour_Destination");
        });

        modelBuilder.Entity<TourBookingForm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TourBook__3214EC07A33E2255");

            entity.ToTable("TourBookingForm");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Citizenship)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ParticipantType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlaceOfBirth)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Tour).WithMany(p => p.TourBookingForms)
                .HasForeignKey(d => d.TourId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TourBooking_Tour");
        });

        modelBuilder.Entity<TsCompanyMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TS_Compa__3213E83F007862AB");

            entity.ToTable("TS_Company_Master");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Remarks)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("remarks");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
