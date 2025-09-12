using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class Destination
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    // New property for storing image binary
    public byte[]? ImageData { get; set; }

    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
