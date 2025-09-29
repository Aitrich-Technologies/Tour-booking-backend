using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public partial class AuthUser
{
    //public Guid Id { get; set; }

    //public string FirstName { get; set; } = null!;

    //public string LastName { get; set; } = null!;

    //public string? Gender { get; set; }

    //public DateOnly? Dob { get; set; }

    //public UserRole Role { get; set; }

    //public string UserName { get; set; } = null!;

    //public string Password { get; set; } = null!;

    //public string? EmailId { get; set; }

    //public string? TelephoneNo { get; set; }
    [Key]
    public Guid Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [MaxLength(10)]
    public string Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [Required]
    public UserRole? Role { get; set; } = UserRole.CUSTOMER;

    [Required, MaxLength(50)]
    public string UserName { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(20)]
    public string TelephoneNo { get; set; }

    [Required, MaxLength(200)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}


