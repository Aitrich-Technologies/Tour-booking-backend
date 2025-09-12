using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Tour
{
    public Guid Id { get; set; }

    public string TourName { get; set; } = null!;

    public string? TourDescription { get; set; }

    public Guid DestinationId { get; set; }

    public int? NoOfNights { get; set; }

    public int Price { get; set; }

    public DateOnly? DepartureDate { get; set; }

    public DateOnly? ArrivalDate { get; set; }

    public AuthUser Customer { get; set; }

    public TourStatus Status { get; set; }

    public AuthUser Consultant { get; set; }

    public Guid TermsId { get; set; }

    public virtual Destination Destination { get; set; } = null!;

    public virtual ICollection<TermsAndCondition> TermsAndConditions { get; set; } = new List<TermsAndCondition>();
    public virtual ICollection<TourBookingForm> Notes { get; set; } = new List<TourBookingForm>();

    public virtual ICollection<TourBookingForm> TourBookingForms { get; set; } = new List<TourBookingForm>();
}
