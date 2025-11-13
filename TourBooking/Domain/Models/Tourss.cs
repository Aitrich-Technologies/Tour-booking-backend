using Domain.Enums;
using Domain.Models;

public partial class Tourss
{
    public Guid Id { get; set; }

    public string TourName { get; set; } = null!;
    public string? TourDescription { get; set; }

    public Guid DestinationId { get; set; }
    public int? NoOfNights { get; set; }
    public int Price { get; set; }
    public DateOnly? DepartureDate { get; set; }
    public DateOnly? ArrivalDate { get; set; }

    // Foreign keys for AuthUser
    public Guid? CustomerId { get; set; }
    public Guid ConsultantId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public TourStatus Status { get; set; }

    // Navigation properties
    public virtual Destination Destination { get; set; } = null!;
    public virtual AuthUser? Customer { get; set; } 
    public virtual AuthUser Consultant { get; set; } = null!;

    public virtual ICollection<TermsAndCondition> TermsAndConditions { get; set; } = new List<TermsAndCondition>();
    public virtual ICollection<Notes> Notes { get; set; } = new List<Notes>();
    public virtual ICollection<TourBookingForm> TourBookingForms { get; set; } = new List<TourBookingForm>();
}
