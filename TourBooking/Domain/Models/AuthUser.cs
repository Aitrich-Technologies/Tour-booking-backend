using Domain.Enum;
using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class AuthUser
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

<<<<<<< HEAD
    public DateOnly? DateOfBirth { get; set; }

    [Required]
    public UserRole? Role { get; set; }
=======
    public DateOnly? Dob { get; set; }

    public UserRole Role { get; set; }
>>>>>>> 13c5b6126e0674dde3e9af550c03a6f6092bade8

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? EmailId { get; set; }

<<<<<<< HEAD
    [Required, MaxLength(200)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



=======
    public string? TelephoneNo { get; set; }
>>>>>>> 13c5b6126e0674dde3e9af550c03a6f6092bade8
}


