using Domain.Enums;

using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class TourBookingForm
{
    public Guid Id { get; set; }

    public Guid TourId { get; set; }
    public Guid? UserId { get; set; }
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Citizenship { get; set; }

    public string? PassportNumber { get; set; }

    public DateOnly? IssueDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string? PlaceOfBirth { get; set; }

    public bool? LeadPassenger { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ParticipantType ParticipantType { get; set; }

    public BookStatus Status { get; set; }
    public EditStatus EditStatusCheck { get; set; }
    public bool IsEditAllowed { get; set; } = false;

    public virtual ICollection<ParticipantInformation> ParticipantInformations { get; set; } = new List<ParticipantInformation>();

    public virtual Tourss Tour { get; set; }
    public virtual AuthUser? User { get; set; }
}
