using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Tour
{
    public Guid Id { get; set; }

    public string TourName { get; set; } = null!;

    public string? TourDescription { get; set; }

    public Guid DestinationId { get; set; }

    public string? NoOfNights { get; set; }

    public DateOnly? DepartureDate { get; set; }

    public DateOnly? ArrivalDate { get; set; }

    public string? Customer { get; set; }

    public string? Status { get; set; }

    public string? Consultant { get; set; }

    public int? TermsId { get; set; }

    public virtual Destination Destination { get; set; } = null!;

    public virtual ICollection<TermsAndCondition> TermsAndConditions { get; set; } = new List<TermsAndCondition>();

    public virtual ICollection<TourBookingForm> TourBookingForms { get; set; } = new List<TourBookingForm>();
}
