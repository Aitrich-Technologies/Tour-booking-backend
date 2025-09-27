using Domain.Enums;

public partial class Notes
{
    public Guid Id { get; set; }
    public Guid TourId { get; set; }

    public string? TourNotes { get; set; }
    public NotesStatus Status { get; set; }

    public virtual Tourss Tour { get; set; } = null!;
}
