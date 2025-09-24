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

    public DateOnly? Dob { get; set; }

    public UserRole Role { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? EmailId { get; set; }

    public string? TelephoneNo { get; set; }
}
