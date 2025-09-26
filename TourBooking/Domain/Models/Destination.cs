using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Models;

//public partial class Destination
//{
//    public Guid Id { get; set; }

//    public string? Name { get; set; } 

//    public string? City { get; set; } 

//    // New property for storing image binary
//    public byte[]? ImageData { get; set; }

//    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
//}using System.Text.Json.Serialization;

public partial class Destination
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? City { get; set; }
    public byte[]? ImageData { get; set; }

    [JsonIgnore]  // 👈 prevent cycles
    public virtual ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
